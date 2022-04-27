using System;

namespace RegistrationAPI
{

    public interface IClient
    {
        string ClientId { get; set; }
        string Email { get; set; }
        string ClientName { get; set; }
        string Password { get; set; }
    }
    
    public class ClientModel : IClient
    {
        public string ClientId { get; set; }
        public string Email { get; set; }
        public string ClientName { get; set; }
        public string Password { get; set; }
    }

    public class ClientRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }

    public class AuthRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponse : IClient
    {
        public string ClientId { get; set; }
        public string Email { get; set; }
        public string ClientName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

    public class StatusModel
    {
        public string Status { get; set; }
    }

    public interface IAccount
    {
        string AccountId { get; set; }
        string ClientId { get; set; }
        string Currency { get; set; }
        double Balance { get; set; }
    }

    public class Account : IAccount
    {
        public string AccountId { get; set; }
        public string ClientId { get; set; }
        public string Currency { get; set; }
        public double Balance { get; set; }
    }

    public class AccountRequest
    {
        public string ClientId { get; set; }
        public string Currency { get; set; }
    }

    public interface ITransactionModel
    {
        string SenderId { get; set; }
        string RecipientId { get; set; }
        double SumOfTransaction { get; set; }
        DateTime DateTime { get; set; }
    }

    public class Transaction : ITransactionModel
    {
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public double SumOfTransaction { get; set; }
        public DateTime DateTime { get; set; }
    }
}