using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace bacit_dotnet.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISqlConnector sqlConnector;

        public HomeController(ILogger<HomeController> logger, ISqlConnector sqlConnector)
        {
            _logger = logger;
            this.sqlConnector = sqlConnector;
        }
        public IActionResult Index()
        
        {
            return View();
        }
         public IActionResult MitTeam()
        {
           return View();
        }
          public IActionResult Stat()
        {
            return View();
        }
           public IActionResult MinProf()
        {
            return View();
        } 
    
    }

}
