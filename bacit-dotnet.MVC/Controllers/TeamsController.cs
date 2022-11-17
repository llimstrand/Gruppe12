using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.DataAccess;
using Microsoft.AspNetCore.Mvc;
using bacit_dotnet.MVC.Models.Teams;
using System.Dynamic;

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
            Console.WriteLine(id);
            var data = sqlConnector.ViewTeams(id); // Henter ut data med id
            dynamic model = new ExpandoObject();
            model.Teams = sqlConnector.ViewTeams(id);
            model.Users = sqlConnector.ViewMembers(id);
            model.Teams = data;
          
            return View("ViewTeam", model); 

        }


        [HttpPost]
        public IActionResult Save(TeamsViewModel model) 
        {   
            sqlConnector.SetTeam(model);
            dynamic models = new ExpandoObject(); // dynamisk modell
            models.Teams = sqlConnector.ViewTeams(model.Team_ID);//Henter team 
            models.Users = sqlConnector.ViewMembers(model.Team_ID);//Henter Users 
            return View("ViewTeam",models); // sender modellen til ViewTeams
        }

        [HttpPost]
        public IActionResult Lagre(TeamsViewModel model){
            sqlConnector.SetMember(model); // Denne at jeg setter medlemmet
            dynamic models = new ExpandoObject(); // dynamisk modell
            models.Teams = sqlConnector.ViewTeams(model.Team_ID);//Henter team 
            models.Users = sqlConnector.ViewMembers(model.Team_ID);//Henter Users 
            return View("ViewTeam", models); // sender modellen til ViewTeams
        }

        public IActionResult AddMember(int id, TeamsViewModel model)
        { 
            dynamic mymodel = new ExpandoObject();
            mymodel.Teams = sqlConnector.ViewTeams(id); ;
            mymodel.Users = sqlConnector.FetchEmp();
            return View(mymodel);
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
        public IActionResult EditMemb(int id)
        {
            dynamic model = new ExpandoObject();
            model.Teams = sqlConnector.ViewTeams(id); ;
            model.Users = sqlConnector.FetchEmpByTeamID(id);
            return View(model);
        }


        [HttpGet]

        public IActionResult DeleteMemb(int id)
        {
            Console.WriteLine(id);
            sqlConnector.DeleteMember(id); // Denne metoden sletter data
            dynamic model = new ExpandoObject();
            model.Teams = sqlConnector.ViewTeams(id);
            model.Users = sqlConnector.ViewMembers(id);
            return View("ViewTeam", model);

         }

        
  
 }
 }
        
        
