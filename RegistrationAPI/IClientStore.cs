using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace RegistrationAPI
{
    public interface IRegisterClientStore
    {
       void RegisterClient (ClientRequest clientRequest);
       IEnumerable<IClient> GetAllClients();

    }

    public class RegisterClientStore : IRegisterClientStore
    {
        private readonly Dictionary<string, IClient> _clients = new ();
        private readonly IConfiguration _configuration;

        public IEnumerable<IClient> GetAllClients()
        {
            return _clients.Values;
        }
        public void RegisterClient(ClientRequest clientRequest)
        {
            var Client = new ClientModel
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientName = clientRequest.Name,
                Email = clientRequest.Email,
                Password = clientRequest.Password.GetHashCode().ToString()
            };
            
            _clients.Add(Client.ClientId, Client);
        }

        public string Authenticate(AuthRequest authRequest)
        {
            var client = _clients.Values.FirstOrDefault(x =>
                     x.Email == authRequest.Email && x.Password == authRequest.Password.GetHashCode().ToString());
            
                 if (client == null)
                     return null;

                 var token = _configuration.GenerateJwtToken(client);

                 return token;
        }
    }
}