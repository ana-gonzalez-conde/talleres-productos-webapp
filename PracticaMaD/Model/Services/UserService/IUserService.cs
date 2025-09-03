using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Services.UserService;
using System;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Model.Services.OrderService;
using System.Collections.Generic;

namespace Model.Services
{
    public interface IUserService
    {

        [Transactional]
        long RegisterUser(String loginName, String clearPassword,
            UserDetails userProfileDetails);

        [Transactional]
        LoginResult Login(String loginName, String password,
            Boolean passwordIsEncrypted);

        [Transactional]
        PreOrderData DefaultInfo(Int64 userId);

        [Transactional]
        void UpdateUser(Int64 userId, UserDetails userDetails, string login = null);

        [Transactional]
        void ChangePassword(Int64 userId, string oldClearPassword, string newClearPassword);

        [Transactional]
        long AddCard(Int64 userId, CardDetails cardDetails);

        [Transactional]
        void UpdateCardDetails(Int64 userId, Int64 cardId, CardDetails cardDetails);

        [Transactional]
        UserDetails UserDetails(Int64 userId);

        [Transactional]
        List<CardDetails> CardsDetails(Int64 userId);
        
        [Transactional]
        CardDetails CardDetails(Int64 cardId);

        [Transactional]
        void DeactivateCard(Int64 cardId);
    }
}
