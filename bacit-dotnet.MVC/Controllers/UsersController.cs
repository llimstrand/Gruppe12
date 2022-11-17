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
        [HttpGet]
        public IActionResult Save(int id) 
        {

            Console.WriteLine(id);
            var data = sqlConnector.FetchEmpByID(id);
            var model = new UsersModel();
            model.Users = data;
            return View("ViewEmp",model); 
        }


         [HttpPost]
        public IActionResult Save(UsersViewModel model) 
        {
            sqlConnector.SetUsers(model);
            var data = sqlConnector.FetchEmpByID(model.Emp_Nr);
            var models = new UsersModel();
            models.Users = data;
            return View("ViewEmp",models); 
        }

        [HttpGet]
        public IActionResult ViewEmp()
        {
            Console.WriteLine();
            var data = sqlConnector.FetchEmp();
            var model = new UsersModel();
            model.Users = data;

            return View("AllEmp",model);

        }

         [HttpPost]
        public IActionResult ViewEmp(UsersViewModel model) 
        {
            sqlConnector.SetUsers(model);
            return View(model); 
        }

        public IActionResult Delete(int id)
       {
           Console.WriteLine(id);
           sqlConnector.DeleteEmp(id); // Denne metoden sletter data
           return View("DeleteEmp");
 
        }
        
        [HttpGet]

        public IActionResult Edit(int id)
        {
            Console.WriteLine(id);
            var data = sqlConnector.UpdateEmp(id); 
            var model = new UsersModel();
            model.Users = data;
            return View(model);
        }

    [HttpPost]
        public IActionResult Update(UsersViewModel model){
            Console.WriteLine("Updates");
            sqlConnector.SetUpEmp(model);
            Console.WriteLine("Model");
            Console.WriteLine(model.Emp_Nr);
            int id = model.Emp_Nr;
            Console.WriteLine(id);
            var data = sqlConnector.UpdateEmp(id);
            var result = new UsersModel();
            result.Users = data;
            return View("ViewEmp", result);}

 
    }
}
