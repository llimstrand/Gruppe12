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
            sqlConnector.SetSaveSug(model);
            var data = sqlConnector.FetchSug(); // Denne metoden henter ut data
            var models = new SuggestionsModel();
            models.suggestions = data;
            return View(models);
        }
    

        [HttpGet]
        public IActionResult ViewSug()
        {

            var data = sqlConnector.FetchSug();
            var model = new SuggestionsModel();
            model.suggestions = data;

            return View("AlleFor",model);

        }
        [HttpGet]

        public IActionResult Edit(int id)
        {
            Console.WriteLine(id);
            var data = sqlConnector.UpdateSug(id); // Denne metoden henter ut data
            var model = new SuggestionsModel();
            model.suggestions = data;
            return View(model);

        }

        [HttpGet]
         public IActionResult Save(int id) 
        {
            Console.WriteLine(id);
            var data = sqlConnector.SaveSug(id); // Få den til å hente ut data med id
            var model = new SuggestionsModel();
            model.suggestions = data;
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(SuggestionViewModel model){
            Console.WriteLine("Update");
            sqlConnector.SetUpSug(model);
            Console.WriteLine("Model");
            int id = 0;
            string? stringid = model.Sug_ID;
            
               Boolean idcheck = string.IsNullOrEmpty(stringid);
                if(!idcheck){
                id = Int32.Parse(stringid);
                Console.WriteLine(id);
                } else{
                    Console.WriteLine("Sug_Id is empty or Null");
                }
                
    
            var data = sqlConnector.UpdateSug(id);
            var result = new SuggestionsModel();
            result.suggestions = data;
            return View("Save", result);
        }

        [HttpGet]

        public IActionResult Delete(int id)
        {
            Console.WriteLine(id);
            sqlConnector.DeleteSug(id); // Denne metoden sletter data
            return View("Delete");

    }
}
}
