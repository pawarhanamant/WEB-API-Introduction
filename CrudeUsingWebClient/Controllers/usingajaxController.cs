using Microsoft.AspNetCore.Mvc;

namespace CrudeUsingWebClient.Controllers
{
    public class usingajaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       public IActionResult Create()
        {
            return View();
        }
        public IActionResult Deatils(int id)
        {
            return View();
        }
    }
}
