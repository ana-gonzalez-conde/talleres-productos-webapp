using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Daos.UserDao;
using Model.Services;
using Model.Services.UserService;
using Model.Services.UserService.Exceptions;
using Model.Services.UserService.Util;
using Ninject;
using System;
using System.Linq;
using System.Transactions;

namespace Test.Services
{
        /// <summary>
        /// This is a test class for IUserServiceTest and is intended to contain all IUserServiceTest
        /// Unit Tests
        /// </summary>
        [TestClass]
        public class IUserServiceTest
        {
            // Variables used in several tests are initialized here
            private const string loginName = "loginNameTest";

            private const string clearPassword = "password";
            private const string firstName = "name";
            private const string surnames = "lastName anotherOne";
            private const string email = "user@udc.es";
            private const string address = "221B Baker Street";
            private const string language = "es";
            private const string country = "ES";
            private const long NON_EXISTENT_USER_ID = -1;
            private static IKernel kernel;
            private static IUserService userService;

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the
        /// current test run.
        /// </summary>
        private TransactionScope transactionScope;

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
               return testContextInstance;
            }
            set
            {
               testContextInstance = value;
            }
        }

        /// <summary>
        /// A test for RegisterUser
        /// </summary>
        [TestMethod]
            public void RegisterUserTest()
            {
                    // Register and find user
                    var userId =
                        userService.RegisterUser(loginName, clearPassword,
                            new UserDetails(loginName, firstName, surnames, email, address, language, country));

                    var user = userService.UserDetails(userId);

            // Check data
                    Assert.AreEqual(loginName, user.Login);
                    Assert.AreEqual(firstName, user.FirstName);
                    Assert.AreEqual(surnames, user.Surnames);
                    Assert.AreEqual(email, user.Email);
                    Assert.AreEqual(address, user.Address);
                    Assert.AreEqual(language, user.Language);
                    Assert.AreEqual(country, user.Country);

                    //Rollback is executed.
            }

        [TestMethod]
        public void UpdateUserDetailsTest()
        {
                // registramos un usuario
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, surnames, email, address, language, country));

                var updatedDetails = new UserDetails(loginName,"newName", "newLastNames", "newuser@udc.es",
                    "newAddress", "en", "GB");

                userService.UpdateUser(userId, updatedDetails);

                var obtained =
                    userService.Login(loginName,
                        PasswordEncrypter.Crypt(clearPassword), true);

                // Buscamos el usuario actualizado
                var updatedUser = userService.UserDetails(userId);

                // Verificamos que se actualizo
                Assert.AreEqual(loginName,updatedUser.Login);
                Assert.AreEqual(PasswordEncrypter.Crypt(clearPassword), (obtained.Password));
                Assert.AreEqual("newName", updatedUser.FirstName);
                Assert.AreEqual("newLastNames", updatedUser.Surnames);
                Assert.AreEqual("newuser@udc.es", updatedUser.Email);
                Assert.AreEqual("newAddress", updatedUser.Address);
                Assert.AreEqual("en", updatedUser.Language);
                Assert.AreEqual("GB", updatedUser.Country);

        }

        [TestMethod]
        public void UpdateUserWithLoginTest()
        {
                // Registramos usuario
                var userId = userService.RegisterUser(loginName + "2", clearPassword,
                    new UserDetails(loginName + "2", firstName, surnames, email, address, language, country));

                var new_login = "manolo";

                // Intentamos actualizar el segundo usuario para usar el loginName del primer usuario
                var updatedDetails = new UserDetails(new_login, firstName, surnames, email, address, language, country);

                userService.UpdateUser(userId: userId, userDetails: updatedDetails, login: new_login);

                var obtained =
                    userService.Login(new_login,
                        PasswordEncrypter.Crypt(clearPassword), true);

                Assert.AreEqual(new_login, obtained.Login);
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateNonExistingUserDetailsTest()
        {
            // Creamos detalles de usuario actualizados
            var updatedDetails = new UserDetails("newLogin", "newName", "newLastNames", "newuser@udc.es",
                "newAddress", "en", "GB");

            // Intentamos actualizar un usuario no existente
            userService.UpdateUser(NON_EXISTENT_USER_ID, updatedDetails);

            // Esperamos una InstanceNotFoundException
        }


        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void UpdateUserDetailsDuplicateLoginTest()
        {
                // Registramos dos usuarios
                var userId1 = userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, surnames, email, address, language, country));
                var userId2 = userService.RegisterUser(loginName + "2", clearPassword,
                    new UserDetails(loginName + "2", firstName, surnames, email, address, language, country));

                // Intentamos actualizar el segundo usuario para usar el loginName del primer usuario
                var updatedDetails = new UserDetails(loginName + "2", firstName, surnames, email, address, language, country);

                userService.UpdateUser(userId2, updatedDetails, loginName);

                // Esperamos una DuplicateInstanceException
        }


        /// <summary>
        /// A test for registering a user that already exists in the database
        /// </summary>
        [TestMethod]
            [ExpectedException(typeof(DuplicateInstanceException))]
            public void RegisterDuplicatedUserTest()
            {
                    // Register user
                    userService.RegisterUser(loginName, clearPassword,
                        new UserDetails(loginName, firstName, surnames, email, address, language, country));

                    // Register the same user
                    userService.RegisterUser(loginName, clearPassword,
                       new UserDetails(loginName, firstName, surnames, email, address, language, country));
            }

        ///// <summary>
        /////A test for Login with clear password
        /////</summary>
        [TestMethod]
        public void LoginClearPasswordTest()
        {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, surnames, email, address, language, country));

                var expected = new LoginResult(userId, firstName, loginName,
                    PasswordEncrypter.Crypt(clearPassword), language, country, false);

                // Login with clear password
                var actual =
                    userService.Login(loginName,
                        clearPassword, false);

                // Check data
                Assert.AreEqual(expected, actual);
        }

        ///// <summary>
        /////A test for Login with encrypted password
        /////</summary>
        [TestMethod]
        public void LoginEncryptedPasswordTest()
        {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, surnames, email, address, language, country));

                var expected = new LoginResult(userId, firstName, loginName,
                    PasswordEncrypter.Crypt(clearPassword), language, country, false);

                // Login with encrypted password
                var obtained =
                    userService.Login(loginName,
                        PasswordEncrypter.Crypt(clearPassword), true);

                // Check data
                Assert.AreEqual(expected, obtained);
        }

        [TestMethod]
        public void AddNewCardAndSetAsDefaultTest()
        {
                // Registramos un usuario
                var userId = userService.RegisterUser("userTest", "password", new UserDetails("userTest","Name", "Surname", "email@test.com", "Address", "es", "ES"));

                // Añadimos una primera tarjeta (por defecto implícito)
                userService.AddCard(userId, new CardDetails(1, "1111222233334444", "123", DateTime.Now.AddYears(3)));

                // Añadimos una segunda tarjeta y la establecemos como por defecto
                userService.AddCard(userId, new CardDetails(2, "5555666677778888", "456", DateTime.Now.AddYears(3), true));

                // Comprobamos que la segunda tarjeta es ahora la por defecto
                var defaultCard = userService.DefaultInfo(userId);
                Assert.AreEqual("5555666677778888", defaultCard.DefaultCard().Number);
        }

        [TestMethod]
        public void UpdateNonDefaultCardAndSetAsDefaultTest()
        {
            // Registramos un usuario
            var userId = userService.RegisterUser("userTest", "password", new UserDetails("userTest", "Name", "Surname", "email@test.com", "Address", "es", "ES"));

            // Añadimos una primera tarjeta (por defecto implícito)
            userService.AddCard(userId, new CardDetails(1, "1111222233334444", "123", DateTime.Now.AddYears(3), true));

            // Añadimos una segunda tarjeta y la establecemos como por defecto
            var secondCardId = userService.AddCard(userId, new CardDetails(2, "5555666677778888", "456", DateTime.Now.AddYears(3)));

            // Actualizar la segunda tarjeta y marcarla como por defecto
            userService.UpdateCardDetails(userId, secondCardId, new CardDetails(2, "5555666677778888", "457", DateTime.Now.AddYears(4), true));

            // Verificar que la segunda tarjeta es ahora la por defecto y sus detalles están actualizados
            var updatedCard = userService.CardDetails(secondCardId);
            var defaultData = userService.DefaultInfo(userId);
            Assert.IsTrue(defaultData.DefaultCard().IsDefault);
            Assert.AreEqual(updatedCard.Cvv, "457");
            Assert.AreEqual(updatedCard.Number, "5555666677778888");
        }

        //Test para comprobar que se recupera la información por defecto esperada al realizar un pedido. Tarjeta por defecto y dirección
        [TestMethod]
        public void DefaultInfoTest()
        {
            var userId = userService.RegisterUser("userTest", "password", new UserDetails("userTest", "Name", "Surname", "email@test.com", "Address", "es", "ES"));
            var cardId = userService.AddCard(userId, new CardDetails(1, "1111222233334444", "123", DateTime.Now.AddYears(3), true));
            userService.AddCard(userId, new CardDetails(2, "5555666677778888", "456", DateTime.Now.AddYears(3)));
            // Ejecución
            var preOrderData = userService.DefaultInfo(userId);
            
            // Verificación
            Assert.AreEqual("Address", preOrderData.ShippingAddress, "La dirección de envío no coincide con la esperada.");
            Assert.IsTrue(preOrderData.DefaultCard().IsDefault, "La tarjeta obtenida no está marcada como la tarjeta por defecto.");
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateNonExistingCardTest()
        {
            var userId = userService.RegisterUser("userTest", "password", new UserDetails("userTest", "Name", "Surname", "email@test.com", "Address", "es", "ES"));
            // Intentar actualizar una tarjeta que no existe
            userService.UpdateCardDetails(userId, -1, new CardDetails(1, "1111222233334444", "123", DateTime.Now.AddYears(3), false));

        }

        ///// <summary>
        /////A test for Login with incorrect password
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void LoginIncorrectPasswordTest()
        {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, surnames, email, address, language, country));

                // Login with incorrect (clear) password
                var actual =
                    userService.Login(loginName, clearPassword + "X", false);

                // transaction.Complete() is not called, so Rollback is executed.
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void UpdateCardBelongsToAnotherUserTest()
        {
            // Registro de dos usuarios y sus tarjetas
            long userId1 = userService.RegisterUser("user1", "password", new UserDetails("user1", "Name1", "Surname1", "email1@test.com", "Address1", "es", "ES"));
            long userId2 = userService.RegisterUser("user2", "password", new UserDetails("user2", "Name2", "Surname2", "email2@test.com", "Address2", "es", "ES"));

            // Añadir una tarjeta para el primer usuario
            var cardId = userService.AddCard(userId1, new CardDetails(1, "1111222233334444", "123", DateTime.Now.AddYears(3), true));
            

            // Intentar actualizar la tarjeta del primer usuario como el segundo usuario
            userService.UpdateCardDetails(userId2, cardId, new CardDetails(1, "1111222233334444", "123", DateTime.Now.AddYears(4), false));
        }

        [TestMethod]
        public void RetrieveUserCardsDetailsTest()
        {
            // Registramos un usuario y añadimos dos tarjetas
            var userId = userService.RegisterUser("userTest", "password", new UserDetails("userTest", "Name", "Surname", "email@test.com", "Address", "es", "ES"));
            userService.AddCard(userId, new CardDetails(1, "1111222233334444", "123", DateTime.Now.AddYears(3), true));
            userService.AddCard(userId, new CardDetails(2, "5555666677778888", "456", DateTime.Now.AddYears(3)));

            // Recuperamos las tarjetas del usuario
            var cardsDetails = userService.CardsDetails(userId);

            // Verificamos que se recuperan correctamente las tarjetas
            Assert.AreEqual(2, cardsDetails.Count);
            Assert.IsTrue(cardsDetails.Any(card => card.Number == "1111222233334444" && card.Cvv=="123"));
            Assert.IsTrue(cardsDetails.Any(card => card.Number == "5555666677778888" && card.Cvv == "456"));
        }

        [TestMethod]
        public void AddCardAsDefaultWhenNoOtherCardsExistTest()
        {
            // Registramos un usuario y añadimos una tarjeta
            var userId = userService.RegisterUser("userTest", "password", new UserDetails("userTest", "Name", "Surname", "email@test.com", "Address", "es", "ES"));
            var cardId = userService.AddCard(userId, new CardDetails(1, "1111222233334444", "123", DateTime.Now.AddYears(3)));

            // Verificamos que la tarjeta añadida es por defecto ya que es la única
            var defaultCard = userService.DefaultInfo(userId);
            Assert.IsNotNull(defaultCard.DefaultCard());
            Assert.IsTrue(defaultCard.DefaultCard().IsDefault);
        }

        [TestMethod]
        public void DeactivateDefaultCardAndAssignNewDefaultTest()
        {
            // Registramos un usuario y añadimos dos tarjetas, marcando la primera como por defecto.
            var userId = userService.RegisterUser("userTest", "password", new UserDetails("userTest", "Name", "Surname", "email@test.com", "Address", "es", "ES"));
            var defaultCardDetails = new CardDetails(1, "1111222233334444", "123", DateTime.Now.AddYears(3), true);
            var secondCardDetails = new CardDetails(2, "5555666677778888", "456", DateTime.Now.AddYears(3));

            var defaultCardId = userService.AddCard(userId, defaultCardDetails);
            var secondCardId = userService.AddCard(userId, secondCardDetails);

            // Act: Desactivamos la tarjeta predeterminada.
            userService.DeactivateCard(defaultCardId);

            // Assert: Verificamos que la tarjeta por defecto ya no está activa y que una nueva tarjeta es ahora la predeterminada.
            var cardsDetail = userService.CardsDetails(userId);

            // Assert: La tarjeta desactivada no debe estar en la lista.
            Assert.IsFalse(cardsDetail.Any(card => card.Equals(defaultCardDetails)));

            // Assert: Debe haber una y solo una tarjeta por defecto y que sea diferente a la desactivada.
            Assert.AreEqual(1, cardsDetail.Count(card => card.IsDefault));
            Assert.AreNotEqual(defaultCardDetails, cardsDetail.FirstOrDefault(card => card.IsDefault));
        }

        [TestMethod]
        public void DeactivateNonDefaultCardAndDefaultRemainsUnchangedTest()
        {
            // Registramos un usuario y añadimos dos tarjetas, marcando la primera como por defecto.
            var userId = userService.RegisterUser("userNonDefaultCard", "password", new UserDetails("userNonDefaultCard", "User", "NonDefaultCard", "user@nondefault.com", "Another Address", "es", "ES"));
            var defaultCardDetails = new CardDetails(1, "6666666666666666", "333", DateTime.Now.AddYears(3), true);
            var nonDefaultCardDetails = new CardDetails(2, "7777777777777777", "444", DateTime.Now.AddYears(3));

            var defaultCardId = userService.AddCard(userId, defaultCardDetails);
            var nonDefaultCardId = userService.AddCard(userId, nonDefaultCardDetails);

            // Act: Desactivamos la tarjeta que no es predeterminada.
            userService.DeactivateCard(nonDefaultCardId);

            // Assert: Verificamos que la tarjeta predeterminada sigue siendo la misma y que la tarjeta no predeterminada ha sido desactivada.
            var cardsDetail = userService.CardsDetails(userId);

            // Assert: La tarjeta no predeterminada no debe estar en la lista.
            Assert.IsFalse(cardsDetail.Any(card => card.Equals(nonDefaultCardDetails)));

            // Assert: La tarjeta predeterminada sigue siendo la misma.
            var defaultCard = cardsDetail.FirstOrDefault(card => card.IsDefault);
            Assert.IsNotNull(defaultCard);
            Assert.IsTrue(defaultCard.Equals(defaultCardDetails));

            int expectedActiveCards = 1; 
            Assert.AreEqual(expectedActiveCards, cardsDetail.Count());
        }

        ///// <summary>
        /////A test for Login with a non-existing user
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void LoginNonExistingUserTest()
        {
            // Login for a user that has not been registered
            var actual =
                userService.Login(loginName, clearPassword, false);
        }

        /// <summary>
        /// A test for ChangePassword
        /// </summary>
        [TestMethod]
        public void ChangePasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, surnames, email, address, language, country));

                // Change password
                var newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword, newClearPassword);

                // Try to login with the new password. If the login is correct, then the password
                // was successfully changed.
                userService.Login(loginName, newClearPassword, false);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for ChangePassword entering a wrong old password
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void ChangePasswordWithIncorrectPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, surnames, email, address, language, country));

                // Change password
                var newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword + "Y", newClearPassword);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for ChangePassword when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ChangePasswordForNonExistingUserTest()
        {
            userService.ChangePassword(NON_EXISTENT_USER_ID,
                clearPassword, clearPassword + "X");
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
            public static void MyClassInitialize(TestContext testContext)
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
            }

            //Use ClassCleanup to run code after all tests in a class have run
            [ClassCleanup]
            public static void MyClassCleanup()
            {
                TestManager.ClearNInjectKernel(kernel);
            }

            //Use TestInitialize to run code before running each test
            [TestInitialize]
            public void MyTestInitialize()
            {
                transactionScope = new TransactionScope();
            }

            //Use TestCleanup to run code after each test has run
            [TestCleanup]
            public void MyTestCleanup()
            {
                transactionScope.Dispose();
            }

            

        #endregion Additional test attributes
        }
    }
