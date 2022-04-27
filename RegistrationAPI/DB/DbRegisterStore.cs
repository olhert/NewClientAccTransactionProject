using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Npgsql;

namespace RegistrationAPI
{
    public class DbRegisterStore
    {
        private readonly string connectionString =
            "Server=34.159.94.190;Database=ClientBase;Port=5432;User Id= postgres;Password=jtcboPaN7cHJiprd;Ssl Mode=Require;Trust Server Certificate=true;";

        public DbRegisterStore(string conn)
        {
            connectionString = conn;
        }

        public IEnumerable<ClientModel> GetAllClients()
        {
            using var connection = new NpgsqlConnection(connectionString);
            return connection.Query<ClientModel>("select * from clients ").ToList();
        }

        public IEnumerable<ClientModel> GetById(string id)
        {
            using var connection = new NpgsqlConnection(connectionString);
            return connection.Query<ClientModel>("select * from clients where clientId = @id), new{id}");
        }

        public void RegisterClient(ClientRequest clientRequest)
        {
            using var connection = new NpgsqlConnection(connectionString);
            
            var Client = new ClientModel
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientName = clientRequest.Name,
                Email = clientRequest.Email,
                Password = clientRequest.Password.GetHashCode().ToString()
            };
            
            connection.Execute("insert into clients (clientId, email, name, password) values (@ClientId,  @Email, @ClientName, @Password)", Client);
        }
    }
}