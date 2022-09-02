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

        public bool CreateClient(Client client)
        {
            var query = $"INSERT INTO Clientes VALUES(@cpf, @name, @dataNascimento, @idade)";

            var parameters = new DynamicParameters();
            parameters.Add("cpf", client.cpf);
            parameters.Add("name",client.nome);
            parameters.Add("dataNascimento", client.dataNascimento);
            parameters.Add("idade", CalculateAgeClient(client));

            using var conn = new SqlConnection(_configuration
                .GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) > 0;
        }

        public int CalculateAgeClient(Client client)
        {
            return client.idade != null && client.idade > -1 ? client.idade :
                (DateTime.Now.DayOfYear < client.dataNascimento.DayOfYear ?
            (DateTime.Now.Year - client.dataNascimento.Year) - 1 : (DateTime.Now.Year - client.dataNascimento.Year));              
        }


        public bool DeleteClient(long id)
        {
            var query = "DELETE FROM Clientes WHERE id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            using var conn = new SqlConnection(_configuration
                .GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;

        }

        public bool UpdateClient(string nome, string novoNome)
        {
            var query = "UPDATE Clientes SET nome = @novoNome WHERE nome = @nome";

            var parameters = new DynamicParameters();
            parameters.Add("nome", nome);
            parameters.Add("novoNome", novoNome);

            using var conn = new SqlConnection(_configuration
                .GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;


        }

    }
}
