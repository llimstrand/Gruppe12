using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.DataAccess;
using Microsoft.AspNetCore.Mvc;
using bacit_dotnet.MVC.Models.Users;

namespace bacit_dotnet.MVC.Controllers
{
    public class StatController : Controller
    {
        private readonly ILogger<StatController> _logger;
        private readonly ISqlConnector sqlConnector;

        public StatController(ILogger<StatController> logger, ISqlConnector sqlConnector)
        {
            _logger = logger;
            this.sqlConnector = sqlConnector;
        }
        public IActionResult StatAllEmpEx()
        {
            return View();
        }

         public IActionResult StatAllEmpPr()
        {
            return View();
        }

         [HttpGet]
        public IActionResult ViewStatEmpEx()
        {
            Console.WriteLine();
            var data = sqlConnector.FetchStatEmpEx();
            var model = new UsersModel();
            model.Users = data;
            return View("StatAllEmpEx",model);

        }

        [HttpGet]
        public IActionResult ViewStatEmpPr()
        {
            Console.WriteLine();
            var data = sqlConnector.FetchStatEmpPr();
            var model = new UsersModel();
            model.Users = data;
            return View("StatAllEmpPr",model);
        }

        public IActionResult StatAllTeamEx()
        {
            return View();
        }

        public IActionResult StatAllTeamPr()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewStatTeamEx()
        {
            Console.WriteLine();
            var data = sqlConnector.FetchStatTeamEx();
            var model = new TeamsModel();
            model.Teams = data;
            return View("StatAllTeamEx",model);
        }

        [HttpGet]
        public IActionResult ViewStatTeamPr()
        {
            Console.WriteLine();
            var data = sqlConnector.FetchStatTeamPr();
            var model = new TeamsModel();
            model.Teams = data;
            return View("StatAllTeamPr",model);
        }
    }
}
