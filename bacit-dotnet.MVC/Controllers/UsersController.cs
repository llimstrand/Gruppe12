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
        
        public IActionResult AllEmp()
        {
            return View();
        }
        public IActionResult AddEmp()
        {
            return View();
        }

         [HttpPost]
        public IActionResult Save(UsersViewModel model) 
        {
            sqlConnector.SetUsers(model);
            return View("ViewEmp",model); 
        }

        [HttpGet]
        public IActionResult ViewEmp()
        {

            var data = sqlConnector.FetchEmp();
            var model = new UsersModel();
            model.Users = data;

            return View("AllEmp",model);

        }
        
    }
}
