using System;
using System.Collections.Generic;
using System.Linq;

namespace RegistrationAPI
{
    public interface IAccountStore
    {
        void RegisterAcc(AccountRequest account);
        IEnumerable<IAccount> GetAllAccounts();
        IAccount GetAccountById(string id);
        void Deposit(string accountId, double sumOfDeposit);
        void Withdrawal(string accountId, double sumOfWithdrawal);
        void Transaction(string idOfSender, double sumOfTransaction, string idOfRecipient);
    }

    public class AccountStore : IAccountStore
    {
        private readonly List<Account> _accounts;

        public void RegisterAcc(AccountRequest account)
        {
            var Account = new Account
            {
                AccountId = Guid.NewGuid().ToString(),
                ClientId = account.ClientId,
                Currency = account.Currency,
                Balance = 0
            };
            
            _accounts.Add(Account);
        }

        public IEnumerable<IAccount> GetAllAccounts()
        {
            return _accounts;
        }

        public IAccount GetAccountById(string id)
        {
            return _accounts.FirstOrDefault(x => x.AccountId == id);
        }
        
        public void Deposit(string accountId, double sumOfDeposit)
        {
           var account = _accounts.FirstOrDefault(x => x.AccountId == accountId);
           
           account.Balance += sumOfDeposit;
        }

        public void Withdrawal(string accountId, double sumOfWithdrawal)
        {
            var account = _accounts.FirstOrDefault(x => x.AccountId == accountId);
            account.Balance -= sumOfWithdrawal;
        }

        public void Transaction(string idOfSender, double sumOfTransaction, string idOfRecipient)
        {
            var senderAccount = _accounts.FirstOrDefault(x => x.AccountId == idOfSender);
            var recipientAccount = _accounts.FirstOrDefault(x => x.AccountId == idOfSender);
            senderAccount.Balance -= sumOfTransaction;
            recipientAccount.Balance += sumOfTransaction;
        }
    }
}