using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.DataAccess;
using Microsoft.AspNetCore.Mvc;
using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ISqlConnector sqlConnector;

        public AdminController(ILogger<AdminController> logger, ISqlConnector sqlConnector)
        {
            _logger = logger;
            this.sqlConnector = sqlConnector;
        }


        [HttpGet]
        public IActionResult AdmViewSug()
        {

            var data = sqlConnector.FetchSug();
            var model = new SuggestionsModel();
            model.suggestions = data;

            return View("AdmAllSug",model);

        }

        public IActionResult AdmProfile()
        
        {
            return View();
        }

        
        
    }
}