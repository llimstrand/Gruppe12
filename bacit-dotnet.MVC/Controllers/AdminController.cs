using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.DataAccess;
using Microsoft.AspNetCore.Mvc;
using bacit_dotnet.MVC.Models;
using System.Dynamic;

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
        public IActionResult AdmProfile()
        
        {
            return View();
        }


        [HttpGet]
        public IActionResult AdmAllSug()
        {

            var data = sqlConnector.FetchSug();
            var model = new SuggestionsModel();
            model.suggestions = data;

            return View(model);

        }
         [HttpGet]
         public IActionResult Save(int id) 
        {
            Console.WriteLine(id);
            var data = sqlConnector.SaveSug(id); // Henter ut data med id
            dynamic mymodel = new ExpandoObject();
            mymodel.suggestions = sqlConnector.SaveSug(id);
            mymodel.Proposers = sqlConnector.FetchProByID(id);
            mymodel.Users = sqlConnector.FetchExByID(id);
            
            return View("AdmViewSug", mymodel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Console.WriteLine(id);
            sqlConnector.DeleteSug(id); // Denne metoden sletter data
            return View("Delete");

        }

        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Console.WriteLine(id);
             // Denne metoden henter ut data
            dynamic mymodel = new ExpandoObject();
            mymodel.suggestions = sqlConnector.UpdateSug(id); ;
            mymodel.Users = sqlConnector.FetchEmp();
            
            return View(mymodel);

        }

        [HttpPost]
        public IActionResult Update(SuggestionViewModel model){
            Console.WriteLine("Update");
            sqlConnector.SetUpSug(model);
            Console.WriteLine("Model");
            int id =  model.Sug_ID;
            
            var data = sqlConnector.UpdateSug(id); //hvis forslaget har en id skal forslaget vises
            dynamic models = new ExpandoObject();
            models.suggestions = sqlConnector.SaveSug(id);
            models.Proposers = sqlConnector.FetchProByID(id);
            models.Users = sqlConnector.FetchExByID(id);
            return View("ViewSug", models);
        }

        
        
        
    }
}