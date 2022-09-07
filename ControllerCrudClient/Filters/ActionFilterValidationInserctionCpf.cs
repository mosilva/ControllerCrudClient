using ControllerCrudClient.Core;
using ControllerCrudClient.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControllerCrudClient.Filters
{
    public class ActionFilterValidationInserctionCpf : ActionFilterAttribute
    {
        public IClientService _clientService;
        public ActionFilterValidationInserctionCpf(IClientService clientService)
        {
            _clientService = clientService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Client model = (Client) context.ActionArguments["Client"];           
           
            if (_clientService.CheckExistsCpfClient(model.cpf)){

                context.Result = new StatusCodeResult(StatusCodes.Status409Conflict);
            }


        }
    }
}
