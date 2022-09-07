using ControllerCrudClient.Core;
using ControllerCrudClient.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControllerCrudClient.Filters
{
    public class ActionFilterCheckUpdateNome : ActionFilterAttribute
    {
        public IClientService _clientService;
        public ActionFilterCheckUpdateNome(IClientService clientService)
        {
            _clientService = clientService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string nome = (string) context.ActionArguments["nome"];           
           
            if (!(_clientService.CheckExistsNomeClient(nome))){

                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
