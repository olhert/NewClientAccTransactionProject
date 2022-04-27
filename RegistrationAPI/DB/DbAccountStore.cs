using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;

namespace RegistrationAPI
{
    public class DbAccountStore
    {
        private readonly string connectionString =
            "Server=34.159.94.190;Database=ClientBase;Port=5432;User Id= postgres;Password=jtcboPaN7cHJiprd;Ssl Mode=Require;Trust Server Certificate=true;";

        public DbAccountStore(string connectionStr)
        {
            connectionString = connectionStr;
        }

        public void RegisterAcc(AccountRequest accountRequest)
        {
            using var connection = new NpgsqlConnection(connectionString);
            
            var Account = new Account
            {
                AccountId = Guid.NewGuid().ToString(),
                ClientId = accountRequest.ClientId,
                Currency = accountRequest.Currency,
                Balance = 0
            };
            
            connection.Execute("insert into accounts (accountid, clientid, currency, balance) values (@AccountId,  @ClientId, @Currency, @Balance)", Account);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            using var connection = new NpgsqlConnection(connectionString);
            
            return connection.Query<Account>("select * from accounts").ToList();
        }

        public IEnumerable<Account> GetAccountById(string id)
        {
            using var connection = new NpgsqlConnection(connectionString);
            
            return connection.Query<Account>("select * from accounts where accountid = @id", new{id});
        }

        public void Deposit(string accountId, double sumOfDeposit)
        {
            using var connection = new NpgsqlConnection(connectionString);
            
            var account = connection.QueryFirstOrDefault<Account>("select * from accounts where accountid = @accountId", new{accountId});

            if (account == null)
            {
                return;
            }
            
            connection.Execute("select deposit (@accountId, @sumOfDeposit);", new{accountId, sumOfDeposit});
        }

        public void Withdrawal(string accountId, double sumOfWithdrawal)
        {
            using var connection = new NpgsqlConnection(connectionString);
            
            var account = connection.QueryFirstOrDefault<Account>("select * from accounts where accountid = @accountId", new{accountId});

            if (account == null)
            {
                return;
            }
            
            connection.Execute("select withdrawal (@accountId, @sumOfWithdrawal);", new{accountId, sumOfWithdrawal});
        }

        public void Transaction(string idOfSender, double sumOfTransaction, string idOfRecipient)
        {
            using var connection = new NpgsqlConnection(connectionString);

            var senderAccount = connection.QueryFirstOrDefault<Account>("select * from accounts where accountid = @idOfSender", new{idOfSender});

            if (senderAccount == null)
            {
                return;
            }
            
            var recipientAccount = connection.QueryFirstOrDefault<Account>("select * from accounts where accountid = @idOfRecipient", new{idOfRecipient});

            if (recipientAccount == null)
            {
                return;
            }
            
            var Transaction = new Transaction
            {
                DateTime = DateTime.Now,
                SenderId = idOfSender,
                RecipientId = idOfRecipient,
                SumOfTransaction = sumOfTransaction
            };
            
            connection.Execute("select transaction (@idOfSender, @idOfRecipient, @sumOfTransaction);", new{idOfSender, idOfRecipient, sumOfTransaction});
            connection.Execute("insert into transactions (senderid, recipientid, sumoftransaction, datetime) values (@SenderId,  @RecipientId, @SumOfTransaction, @DateTime)", Transaction);
        }

        
    }
}