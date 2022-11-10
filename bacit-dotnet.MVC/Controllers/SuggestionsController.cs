using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.DataAccess;
using Microsoft.AspNetCore.Mvc;
using bacit_dotnet.MVC.Models;
using System.Dynamic;

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
            var data = sqlConnector.FetchEmp();
            var model = new UsersModel();
            model.Users = data;
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewSug()
        {

            var data = sqlConnector.FetchSug(); //henter alle forslag og sier at de skal vises på AlleFor
            var model = new SuggestionsModel();
            model.suggestions = data;

            return View("AlleFor",model);

        }

        [HttpPost]
        public IActionResult Save(SuggestionViewModel model) 
        {   
            Console.WriteLine(model.Emp_Nr);
            Console.WriteLine(model.Executor_Nr);
            sqlConnector.SetSug(model);
            sqlConnector.SetProposer(model);
            sqlConnector.SetExecutor(model);
            var data = sqlConnector.FetchSug(); //henter alle forslag og sier at de skal vises på AlleFor
            var models = new SuggestionsModel();
            models.suggestions = data;

            return View("AlleFor",models);
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
            
            return View("Save", mymodel);
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
            var result = new SuggestionsModel();
            result.suggestions = data;
            return View("AlleFor", result);
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
