using Common.Service.AuthorizationProvider;
using Common.Service.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SpeechLuisOwin.Controllers
{
    [Route("api/[controller]")]
    public class AzureController : Controller
    {
        private AADTokenProvider _tokenProvider;

        public AzureController(AADTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public async Task<ServiceAuthenticationResultModel> Get()
        {
            //var getter = new AADTokenProvider();
            return await _tokenProvider.GetAccessTokenFromAAD();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}