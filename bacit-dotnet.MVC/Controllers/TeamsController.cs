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

         [HttpPost]
        public IActionResult Save(TeamsViewModel model) 
        {   
            Console.WriteLine(model.Team_ID);
           
            sqlConnector.SetTeams(model);
            return View("ViewTeam",model); 
        }

        [HttpGet]
        public IActionResult ViewTeam()
        {

            var data = sqlConnector.FetchTeam();
            var model = new TeamsModel();
            model.Teams = data;

            return View("AllTeam",model);

        }
        
    }
}
