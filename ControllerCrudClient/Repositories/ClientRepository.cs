using Dapper;
using System.Data.SqlClient;


namespace ControllerCrudClient.Repositories
{
    public class ClientRepository
    {
        private readonly IConfiguration _configuration;

        public ClientRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Client> GetClients()
        {
            var query = "SELECT * FROM Clientes";

            using var conn = new SqlConnection(_configuration
                .GetConnectionString("DefaultConnection"));

            var selectAll = conn.Query<Client>(query).ToList(); 

            return selectAll;

        }


    }
}
