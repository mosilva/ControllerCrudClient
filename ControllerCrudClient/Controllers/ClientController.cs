using ControllerCrudClient.Core;
using ControllerCrudClient.Core.Interface;
using ControllerCrudClient.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ControllerCrudClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [TypeFilter(typeof(LogAuthorizationFilter))]
    [TypeFilter(typeof(ResourceFilterShowStopWatch))]
    public class ClientController : ControllerBase
    {
        private IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(ActionFilterValidationInserctionCpf))]
        public ActionResult<Client> Create(Client client)
        {
            if (!(_clientService.CreateClient(client)))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
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
            var clientsList = _clientService.GetClients();

            if (clientsList == null)
            {
                return NotFound();
            }

            return Ok(clientsList);
        }

        [HttpPut("/{novoNome}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(ActionFilterCheckUpdateNome))]
        public IActionResult Update(string nome, string novoNome)
        {
            if (!(_clientService.UpdateClient(nome, novoNome)))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpDelete("/{index}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Client>> Delete(int index)
        {
            if (!_clientService.DeleteClient(index))
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}