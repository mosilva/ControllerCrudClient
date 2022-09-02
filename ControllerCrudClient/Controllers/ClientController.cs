using ControllerCrudClient.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ControllerCrudClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]


    public class ClientController : ControllerBase
    {
        public List<Client> clients { get; set; }

        private ClientRepository _clientRepository;

        public ClientController(IConfiguration configuration)
        {
            _clientRepository = new ClientRepository(configuration);
        }

        #region CreateClientsWhithoutDB
        //public ClientController()
        //{
        //    clients = Enumerable.Range(0, 4).Select(index => new Client
        //    {
        //        cpf = String.Concat(Enumerable.Repeat(index + 1, 9)),
        //        name = names[index] + " " + lastnames[index],
        //        birthDate = DateTime.Now.AddYears(-(index + 1) * 5)

        //    })
        //    .ToList();
        //}
        #endregion

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Client> Create(Client client)
        {
            if (_clientRepository.CreateClient(client))
            {
                return BadRequest();
            };
            return CreatedAtAction(nameof(Create), client);
        }

        #region TestCreateClient
        //{
        //    "cpf":"25478415420",
        //    "name": "Arthur Santos Alves",
        //    "birthDate": "2008-08-28T20:03:51.139Z"
        //}        
        #endregion

        [HttpGet("/clients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Client>> Read()
        {
            var clientsList = _clientRepository.GetClients();
            
            if(clientsList == null) { 
                return NotFound();
            }

            return Ok(clientsList);
        }

        [HttpPut("/{novoNome}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(string nome, string novoNome)
        {
            if(_clientRepository.UpdateClient(nome, novoNome))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("/{index}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Client>> Delete(int index)
        {
            if (!_clientRepository.DeleteClient(index))
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}