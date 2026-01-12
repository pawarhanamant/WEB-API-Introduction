using CRUDEFcore.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CrudeUsingWebClient.Controllers
{
    public class CtegoriesController : Controller
    {
        private readonly HttpClient _httpClient;

        public CtegoriesController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
        }


        #region hide
        //public IActionResult Index()
        //{
        //// https://localhost:7251/
        //// https://localhost:7251/api/Ctegories

        //    List<Ctegories> ctegories = new List<Ctegories>();
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("https://localhost:7251/api/Ctegories");

        //    HttpResponseMessage respose = client.GetAsync("Ctegories").Result;

        //    if (respose.IsSuccessStatusCode) { 

        //      var jsondata = respose.Content.ReadAsStringAsync().Result;
        //        ctegories = JsonSerializer.Deserialize<List<Ctegories>>(jsondata,
        //            new JsonSerializerOptions() {PropertyNameCaseInsensitive = true });
        //    }

        //    return View(ctegories);
        //}
        #endregion hide
        [HttpGet]

        public async Task<IActionResult> Index() {

            string apitoen = HttpContext.Session.GetString("apitoen");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", $"Bearer {apitoen}");
         HttpResponseMessage respones =    await _httpClient.GetAsync("Ctegories");
            string jsonresponse  = respones.Content.ReadAsStringAsync().Result;

            List<Ctegories> ctegories = JsonSerializer.Deserialize<List<Ctegories>>(jsonresponse, new JsonSerializerOptions
            {
 
                PropertyNameCaseInsensitive = true
            });

            return View(ctegories);
        }
        [HttpGet]
        public IActionResult Create() {
            
            return View();
        
        }
        public IActionResult Create( Ctegories ctegories) { 
        
          
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7251/api/Ctegories");

            string reuest = JsonSerializer.Serialize(ctegories);
            StringContent  reuestbody = new StringContent(reuest,Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessge  = client.PostAsync("ctegories", reuestbody).Result;

            if (responseMessge.IsSuccessStatusCode) { 
               
                return RedirectToAction("Index");
            }
            return View(ctegories);
            
        }
    }
}
