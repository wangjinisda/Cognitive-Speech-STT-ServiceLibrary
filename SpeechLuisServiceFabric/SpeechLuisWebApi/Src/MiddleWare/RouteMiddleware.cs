
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SpeechLuisOwin.Src.MiddleWare
{
    public class RouteMiddleware {     
        private readonly ILogger _logger;

    private readonly RequestDelegate _next;

    public RouteMiddleware(RequestDelegate next, ILogger logger) 
        {
            this._logger = logger;
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //this._logger.Write("Inside the 'Invoke' method of the '{0}' middleware.", GetType().Name);

            await _next(context);
        }
    }
}