using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CrudeUsingWebClient.Controllers
{
    public class AccountController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;


        public AccountController(HttpClient httpClient , IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(configuration["ApiBaseUrl"]);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest loginRequest) {

            if (ModelState.IsValid) { 
            
                string json =   JsonSerializer.Serialize(loginRequest);
                StringContent reuest = new StringContent(json,Encoding.UTF8,
                    "application/json");
                HttpResponseMessage responseMessage = await _httpClient.PutAsync("CCount", reuest);
                responseMessage.EnsureSuccessStatusCode();

                string responsetoen = responseMessage.Content.ReadAsStringAsync().Result;
                HttpContext.Session.SetString("apitoen", responsetoen);

                return RedirectToAction("Index", "Home");
               
            }
        
            return View(loginRequest);
        }
    }
}
