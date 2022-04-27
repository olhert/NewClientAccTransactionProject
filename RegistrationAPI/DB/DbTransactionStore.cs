using System.Collections.Generic;
using System.Linq;
using Dapper;
using Npgsql;

namespace RegistrationAPI
{
    public class DbTransactionStore
    {
        private readonly string connectionString =
            "Server=34.159.94.190;Database=ClientBase;Port=5432;User Id= postgres;Password=jtcboPaN7cHJiprd;Ssl Mode=Require;Trust Server Certificate=true;";

        public DbTransactionStore(string conn)
        {
            connectionString = conn;
        }
        public IEnumerable<Transaction> GetAllTransactions()
        {
            using var connection = new NpgsqlConnection(connectionString);

            return connection.Query<Transaction>("select * from transactions").ToList();
        }
    }
}