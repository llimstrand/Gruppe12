using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.DataAccess;
using Microsoft.AspNetCore.Mvc;
using bacit_dotnet.MVC.Models.Teams;

namespace bacit_dotnet.MVC.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ILogger<TeamsController> _logger;
        private readonly ISqlConnector sqlConnector;

        public TeamsController(ILogger<TeamsController> logger, ISqlConnector sqlConnector)
        {
            _logger = logger;
            this.sqlConnector = sqlConnector;
        }
        
        public IActionResult AllTeam()
        {
            return View();
        }
        public IActionResult AddTeam()
        { 
            var data = sqlConnector.FetchEmp();
            var model = new UsersModel();
            model.Users = data;
            return View(model);
        }

        
        [HttpGet]
        public IActionResult Save(int id) 
        {   
            var data = sqlConnector.ViewTeams(id); // Henter ut data med id
            var model = new TeamsModel();
            model.Teams = data;
          
            return View("ViewTeam", model); 
        }

        
        
        
        
        
        [HttpPost]
        public IActionResult Save(TeamsViewModel model) 
        {   
            Console.WriteLine(model.Team_ID);

            sqlConnector.SetTeam(model);
            var data = sqlConnector.ViewTeams(model.Team_ID);
            var models = new TeamsModel();
            models.Teams = data;
            return View("ViewTeam",models); 
        }

        [HttpGet]
        public IActionResult ViewTeam()
        {

            var data = sqlConnector.FetchTeam();
            var model = new TeamsModel();
            model.Teams = data;

            return View("AllTeam",model);

        }

           [HttpPost]
        public IActionResult ViewTeam(TeamsViewModel model) 
        {
            sqlConnector.SetTeam(model);
            return View(model); 
        }

                
        [HttpGet]

        public IActionResult Delete(int id)
        {
            Console.WriteLine(id);
            sqlConnector.DeleteTeam(id); // Denne metoden sletter data
            return View("DeleteTeam");

         }

          [HttpGet]

        public IActionResult Edit(int id)
        {
            Console.WriteLine(id);
            var data = sqlConnector.UpdateTeam(id); // Denne metoden henter ut data
            var model = new TeamsModel();
            model.Teams = data;
            return View("EditTeam",model);
    }

     [HttpPost]
        public IActionResult Update(TeamsViewModel model){
            Console.WriteLine("Update");
            sqlConnector.SetUpTeam(model);
            Console.WriteLine("Model");
            Console.WriteLine(model.Team_ID);
            int id = model.Team_ID;
            Console.WriteLine(id);
            var data = sqlConnector.UpdateTeam(id);
            var result = new TeamsModel();
            result.Teams = data;
            return View("ViewTeam", result); 
    }
 }
 }
        
        
