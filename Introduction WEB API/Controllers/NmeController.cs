using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Introduction_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class NmeController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<string> Name() { 
        
            return new List<string>() { "pwr","xxx","yyy" };
        }

        [HttpGet]
        [Route("id")]
        public void GetName(int id) { 
        
        }
        [HttpPost]
        public void Create (string name) { 
        
        }
        [HttpPut]
        public void Update(string name) { 
        
        }
        [HttpDelete]
        public void Delete(string name) { 
        }


    }
}
