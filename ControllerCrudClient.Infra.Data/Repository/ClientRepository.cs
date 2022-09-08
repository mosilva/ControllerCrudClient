using ControllerCrudClient.Core;
using ControllerCrudClient.Core.Interface;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;


namespace ControllerCrudClient.Infra.Data.Repository
{
    public class ClientRepository : IClientRepository
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

        public bool DeleteClient(long id)
        {
            var query = "DELETE FROM Clientes WHERE id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            using var conn = new SqlConnection(_configuration
                .GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
           
            #region Possibilidade de fazer o tratamento de exceção com o Try/Catch

            //try
            //{
            //    var parameters = new DynamicParameters();
            //    parameters.Add("id", id);

            //    using var conn = new SqlConnection(_configuration
            //        .GetConnectionString("DefaultConnection"));

            //    return conn.Execute(query, parameters) == 1;
            //}

            //catch (SqlException ex)
            //{
            //    Console.WriteLine("Erro inesperado ao se comunicar com o banco de dados");

            //    throw;
            //}

            //catch (NullReferenceException ex)
            //{
            //    Console.WriteLine("Erro inesperado no sistema");

            //    return new StatusCodeResult(StatusCodes.Status417ExpectationFailed));
            //}


            //catch (Exception ex)
            //{
            //    var tipoDaExcecao = ex.GetType().Name;
            //    var messagem = ex.InnerException.Message;
            //    var caminho = ex.InnerException.StackTrace;

            //    Console.WriteLine(
            //        $"tipo da exceção:{tipoDaExcecao}\n" +
            //        $"mensagem:{messagem}\n" +
            //        $"caminho:{caminho}");

            //    return false;
            //}

            #endregion

        }

        public Client GetClientByCpf(string cpf)
        {
            var query = "SELECT * FROM Clientes WHERE cpf = @cpf";

            var parameters = new DynamicParameters();
            parameters.Add("cpf", cpf);

            using var conn = new SqlConnection(_configuration
                .GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<Client>(query, parameters);
        }

        public Client GetClientByNome(string nome)
        {
            var query = "SELECT * FROM Clientes WHERE nome = @nome";

            var parameters = new DynamicParameters();
            parameters.Add("nome", nome);

            using var conn = new SqlConnection(_configuration
                .GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<Client>(query, parameters);

        }
    }
}
