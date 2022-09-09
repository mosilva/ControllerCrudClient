using Microsoft.AspNetCore.Mvc.Filters;

namespace ControllerCrudClient
{
    public static class Clock
    {
        static public DateTime start, end;

        static public void StartClock()
        {
            start = DateTime.Now;
        }
        static public void EndClock()
        {
            end = DateTime.Now;
        }

        static public void StopWatchProcess(ResourceExecutedContext context)
        {
            TimeSpan StopWatchProcess = (end - start);

            Console.WriteLine("O tempo do processo foi {0} segundos - Codigo da reqisição: {1}",
                (StopWatchProcess.TotalMilliseconds/1000), context.HttpContext.Request.Headers["Code"]);
        }
    }
}
