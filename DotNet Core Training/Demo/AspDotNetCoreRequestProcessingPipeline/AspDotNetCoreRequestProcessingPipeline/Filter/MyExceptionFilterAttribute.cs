using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AspDotNetCoreRequestProcessingPipeline.Filter
{
    public class MyExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                Debug.WriteLine(context.Exception.Message);
            }
        }
    }
}
