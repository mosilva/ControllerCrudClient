
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControllerCrudClient.Filters
{
    public class ResourceFilterShowStopWatch : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Clock.EndClock();
            Clock.StopWatchProcess(context);
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {

        }
    }
}
