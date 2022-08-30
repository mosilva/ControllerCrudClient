using Microsoft.AspNetCore.Mvc;

namespace ControllerCrudClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Client> Create(Client client)
        {
            clients.Add(client);
            return CreatedAtAction(nameof(Create), client);
        }
        /* TEST CREATE CLIENT
        {
            "cpf":"25478415420",
            "name": "Arthur Santos Alves",
            "birthDate": "2008-08-28T20:03:51.139Z"
        }
        */

        [HttpGet("/clients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Client>> Read()
        {
            return Ok(clients);
        }

        [HttpPut("/{index}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int index, Client client)
        {
            if (index < 0 || index > 4)
            {
                return BadRequest();
            }
            clients[index] = client;
            return NoContent();
        }

        [HttpDelete("/{index}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Client>> Delete(int index)
        {
            if ((index < 0 || index > 4))
            {
                return BadRequest();
            }
            clients.RemoveAt(index);
            return Ok(clients);
            //return StatusCode (200, clients);

        }


    }
}