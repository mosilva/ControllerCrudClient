using Microsoft.AspNetCore.Mvc;


namespace ControllerCrudClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        string[] names = new[] { "Marcelo", "Juliana", "Nathan", "Thayssa" };
        string[] lastnames = new[] { "Oliveira", "da Silva", "Lubawski", "Souza" };

        public List<Client> clients { get; set; }
        public ClientController()
        {
            clients = Enumerable.Range(0, 4).Select(index => new Client
            {
                cpf = String.Concat(Enumerable.Repeat(index + 1, 9)),
                name = names[index] + " " + lastnames[index],
                birthDate = DateTime.Now.AddYears(-(index + 1) * 5)
            })
            .ToList();
        }

        [HttpPost]
        public Client Create(Client client)
        {
            clients.Add(client);
            return client;
        }
        /* TEST CREATE CLIENT
        {
            "cpf":"25478415420",
            "name": "Arthur Santos Alves",
            "birthDate": "2008-08-28T20:03:51.139Z"
        }
        */

        [HttpGet]
        public IEnumerable<Client> Read()
        {
            return clients;
        }





    }
}