using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.DataAccess;
using Microsoft.AspNetCore.Mvc;
using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.Controllers
{
    public class SuggestionsController : Controller
    {
        private readonly ILogger<SuggestionsController> _logger;
        private readonly ISqlConnector sqlConnector;

        public SuggestionsController(ILogger<SuggestionsController> logger, ISqlConnector sqlConnector)
        {
            _logger = logger;
            this.sqlConnector = sqlConnector;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(SuggestionViewModel model) 
        {
            sqlConnector.SetSug(model);
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewSug()
        {

            var data = sqlConnector.FetchSug();
            var model = new SuggestionsModel();
            model.suggestions = data;

            return View(model);

        }
    }
}
