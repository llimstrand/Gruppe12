using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.DataAccess;
using Microsoft.AspNetCore.Mvc;
using bacit_dotnet.MVC.Models.Users;

namespace bacit_dotnet.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ISqlConnector sqlConnector;

        public UsersController(ILogger<UsersController> logger, ISqlConnector sqlConnector)
        {
            _logger = logger;
            this.sqlConnector = sqlConnector;
        }
        [HttpGet]
        public IActionResult Index(){
             return View();
        }

        [HttpGet]
        public IActionResult AddEmp()
        {
            return View();
        }
        public IActionResult Save(){
           
            var data = sqlConnector.GetEmployee();
            var model = new UsersModel();
            model.Users = data;

            return View("ViewEmp",model);
        }
        [HttpPost]
        public IActionResult ViewEmp(UsersViewModel model)
       {   
           Console.WriteLine("test");

            return View("AllEmp");
        }
    }
}
