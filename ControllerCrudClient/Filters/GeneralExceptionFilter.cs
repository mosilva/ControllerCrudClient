using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.SqlClient;

namespace ControllerCrudClient.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            #region Fixar um único erro por filtro de exceção

            //var problem = new ProblemDetails
            //{
            //    Status = 500,
            //    Title = "Erro inesperado. Tente novamente",
            //    Detail = "Ocorreu um erro inesperado na solicitação",
            //    Type = context.Exception.GetType().Name
            //};

            //Console.WriteLine(problem.Title);

            #endregion 

            switch (context.Exception)
            {
                case SqlException:
                    Console.WriteLine("Erro inesperado ao se comunicar com o banco de dados");
                    context.Result = new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
                    break;
                case NullReferenceException:
                    Console.WriteLine("Erro inesperado no sistema");
                    context.Result = new StatusCodeResult(StatusCodes.Status417ExpectationFailed);
                    break;
                default:
                    Console.WriteLine("Erro inesperado. Tente novamente");
                    context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                    //context.Result = new ObjectResult(problem);  //retornar o objeto criado (e comentado) acima
                    break;
            }

        }




    }
}

