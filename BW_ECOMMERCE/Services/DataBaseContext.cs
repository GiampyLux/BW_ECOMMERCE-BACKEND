using System.Data;
using System.Data.SqlClient;

namespace BW_ECOMMERCE.Services
{
    //Questo servizio gestisce la connessione al database. 
    public class DatabaseContext
    {
        private readonly string? _connectionString;

        public DatabaseContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Appconn");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }

}
