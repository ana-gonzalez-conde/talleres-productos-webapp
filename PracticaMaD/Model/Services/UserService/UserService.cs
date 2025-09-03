using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Daos.BankCardDao;
using Model.Daos.UserDao;
using Model.Services.Exceptions;
using Model.Services.OrderService;
using Model.Services.UserService.Exceptions;
using Model.Services.UserService.Util;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Services.UserService
{
    public class UserService : IUserService
    {
        [Inject]
        public IUserDao UserDao { private get; set; }
        [Inject]
        public IBankCardDao CardDao { private get; set; }

        #region IUserService Members

        /// <summary>
        /// Allows to register a new user on the plataform
        /// </summary>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long RegisterUser(string loginName, string clearPassword,
            UserDetails userDetails)
        {
            try
            {
                UserDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(User).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                User user = new User();

                user.login = loginName;
                user.password = encryptedPassword;
                user.name = userDetails.FirstName;
                user.surnames = userDetails.Surnames;
                user.address = userDetails.Address;
                user.email = userDetails.Email;
                user.language = userDetails.Language;
                user.country = userDetails.Country;
                // suponemos que cualquier registro será sin privilegios de administrador.
                // si se quiere crear o cambiar un usuario a tener privilegions de administrador, tendrá
                // que ponerse en contacto con el equipo de desarrollo
                user.isAdmin = false;

                UserDao.Create(user);

                return user.userId;
            }
        }

        /// <summary>
        /// Allows to login with your credentials in the plataform
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        public LoginResult Login(string loginName, string password, bool passwordIsEncrypted)
        {
            User user =
                UserDao.FindByLoginName(loginName);

           String storedPassword = user.password;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }

            return new LoginResult(user.userId, user.name, user.login,
                storedPassword, user.language, user.country, user.isAdmin);
        }

        /// <summary>
        /// Allows to get the data of an order by user id
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public PreOrderData DefaultInfo(Int64 userId)
        {
            // la dirección de envío del usuario y su tarjeta bancaria por defecto.
            var user = UserDao.Find(userId);
            try
            {
                var cards = CardDao.FindCardsByUserId(userId);
                //var defaultCard = CardDao.FindDefaultCardByUserId(userId);
                List<CardDTO> cardsList = cards.Select(card => new CardDTO(
                card.cardId,
                card.number,
                card.isDefault
                )).ToList();

                // Crear y devolver PreOrderData
                return new PreOrderData(user.address, cardsList);
            }
            catch (InstanceNotFoundException)
            {
                return new PreOrderData(user.address, null);
            }
        }

        /// <summary>
        /// Allows to update user details
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public void UpdateUser(Int64 userId, UserDetails userDetails, string login = null)
        {
            User user = UserDao.Find(userId);

            // Si el loginName se quiere cambiar y ya está en uso por otro usuario
            if (!string.IsNullOrWhiteSpace(login) && !user.login.Equals(login))
            {
                if (UserDao.ExistsByLoginName(login))
                {
                    throw new DuplicateInstanceException(login, typeof(User).FullName);
                }
                user.login = login;
            }

            user.name = userDetails.FirstName;
            user.surnames = userDetails.Surnames;
            user.address = userDetails.Address;
            user.email = userDetails.Email;
            user.language = userDetails.Language;
            user.country = userDetails.Country;

            UserDao.Update(user);
        }

        /// <summary>
        /// Adds to add a new bank card
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long AddCard(Int64 userId, CardDetails cardDetails)
        {
            long CardId;
            ValidateUser(userId);
            var existingCards = CardDao.FindCardsByUserId(userId);
            //Si no hay tarjetas la que se añade debe ser la tarjeta por defecto
            if (!existingCards.Any()) cardDetails.IsDefault = true;
            
            var matchingCard = FindMatchingCard(existingCards, cardDetails);

            if (matchingCard != null)
            {
                CardId = HandleExistingCard(matchingCard);
            }
            else
            {
                CardId = AddNewCard(userId, cardDetails, existingCards);
            }
            return CardId;
        }

        /// <summary>
        /// Allows to update bank card details by userId and cardId
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        [Transactional]
        public void UpdateCardDetails(Int64 userId, Int64 cardId, CardDetails cardDetails)
        {
            // Encuentra la tarjeta por ID y verifica que pertenece al usuario
            var cardToUpdate = CardDao.Find(cardId);
            if (cardToUpdate.userId != userId)
            {
                throw new UnauthorizedAccessException("La tarjeta no pertenece al usuario.");
            }

            // Si se solicita establecer como tarjeta por defecto y no es la actual por defecto
            if (cardDetails.IsDefault && !cardToUpdate.isDefault)
            {
                // Busca y desactiva la actual tarjeta por defecto
                CardDao.DeactivateDefaultCard(userId);
                // Establece la tarjeta actual como por defecto
                cardToUpdate.isDefault = true;
            }

            // Actualiza los detalles de la tarjeta
            cardToUpdate.type = cardDetails.Type;
            cardToUpdate.number = cardDetails.Number;
            cardToUpdate.cvv = cardDetails.Cvv;
            cardToUpdate.expirationDate = cardDetails.ExpirationData;

            CardDao.Update(cardToUpdate);
        }

        /// <summary>
        /// Allows to get user data by its id
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public UserDetails UserDetails(long userId)
        {
            var user = UserDao.Find(userId);

            UserDetails userDetails =
                new UserDetails(user.login, user.name, user.surnames, user.email, user.address, user.language, user.country, user.isAdmin);


            return userDetails;
        }

        /// <summary>
        /// Allows to get card details by its id
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public CardDetails CardDetails(long cardId)
        {
            var card = CardDao.Find(cardId);

            CardDetails cardDetails =
                new CardDetails(card.cardId, card.type, card.number, card.cvv, card.expirationDate, card.isDefault);


            return cardDetails;
        }

        /// <summary>
        /// Allows to get all bank cards of an user by its id
        /// </summary>
        [Transactional]
        public List<CardDetails> CardsDetails(long userId)
        {
            var cards = CardDao.FindCardsByUserId(userId);

            List<CardDetails> cardsList = cards.Select(card => new CardDetails(
                card.cardId,
                card.type,
                card.number,
                card.cvv,
                card.expirationDate,
                card.isDefault
             )).ToList();

            return cardsList;
        }

        /// <summary>
        /// Allows to deactivate a bank card by its id
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void DeactivateCard(Int64 cardId)
        {
            var deactiveCard = CardDao.Find(cardId);
            if (deactiveCard.isDefault == true)
            {
                AssignNewDefaultCard(deactiveCard);
                deactiveCard.isDefault = false;
            }
            deactiveCard.isActive = false;
            CardDao.Update(deactiveCard);

        }
        //Asigna cualquier otra tarjeta a la pasada por parametro como tarjeta por defecto. Si es la única registrada por el user no se hace nada
        private void AssignNewDefaultCard(BankCard deactiveCard)
        {
            // Encuentra todas las tarjetas activas del mismo usuario, excluyendo la que se va a desactivar
            var userCards = CardDao.FindCardsByUserId(deactiveCard.userId)
                                    .Where(card => card.cardId != deactiveCard.cardId)
                                    .ToList();

            if (userCards.Any())
            {
                // Si hay otras tarjetas activas, establece la primera como la nueva por defecto
                var newDefaultCard = userCards.First();
                newDefaultCard.isDefault = true;
                CardDao.Update(newDefaultCard);
            }
        } 

        //Recupera la tarjeta de la BD con el mismo número que la que se quiere añadir. El objetivo seria activar una tarjeta desactivada
        private BankCard FindMatchingCard(IEnumerable<BankCard> existingCards, CardDetails cardDetails)
        {
            return existingCards.FirstOrDefault(c => c.number == cardDetails.Number);
        }

        private long HandleExistingCard(BankCard matchingCard)
        {
            if (matchingCard.isActive)
            {
                throw new DuplicateInstanceException(matchingCard.number, typeof(BankCard).FullName);
            }
            else
            {
                // La tarjeta existe pero está inactiva, se activa y actualiza
                matchingCard.isActive = true;
                CardDao.Update(matchingCard);
                return matchingCard.cardId;
            }
        }

        private long AddNewCard(Int64 userId, CardDetails cardDetails, IEnumerable<BankCard> existingCards)
        {
            bool isDefault = cardDetails.IsDefault; 
            // Si la nueva tarjeta va a ser la tarjeta por defecto, desactivar la actual
            if (isDefault && existingCards.Any(c => c.isDefault))
            {
                CardDao.DeactivateDefaultCard(userId);
            }

            BankCard newCard = new BankCard()
            {
                userId = userId,
                type = cardDetails.Type,
                number = cardDetails.Number,
                cvv = cardDetails.Cvv,
                expirationDate = cardDetails.ExpirationData,
                isDefault = cardDetails.IsDefault,
                isActive = true
            };

            CardDao.Create(newCard);
            return newCard.cardId;
        }

        // Verifica que el usuario existe y está autenticado.
        private void ValidateUser(Int64 userId)
        {
            if (!UserDao.Exists(userId))
            {
                throw new UserNotAuthenticatedException("El usuario no está autenticado o no existe.");
            }
        }

        public void ChangePassword(long userId, string oldClearPassword, string newClearPassword)
        {
            User user = UserDao.Find(userId);
            String storedPassword = user.password;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                 storedPassword))
            {
                throw new IncorrectPasswordException(user.login);
            }

            user.password =
            PasswordEncrypter.Crypt(newClearPassword);

            UserDao.Update(user);
        }
        #endregion IUserService Members
    }
}

        
