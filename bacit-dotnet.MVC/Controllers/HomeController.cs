using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace bacit_dotnet.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISqlConnector sqlConnector;

        public HomeController(ILogger<HomeController> logger, ISqlConnector sqlConnector)
        {
            _logger = logger;
            this.sqlConnector = sqlConnector;
        }

        public IActionResult Index()
        {
            return new ContentResult() { 
                Content = @"<html>
                            <head>
                            <title>BACIT</title>    
                            </head>
                            <body><h1>Alle forslag</h1>
                            </body> 
                            </html>", ContentType = "text/html; charset=UTF-8" };
        }
         public IActionResult MitTeam()
        {
            return new ContentResult() { 
                Content = @"<html>
                            <head>
                            <title>BACIT</title>    
                            </head>
                            <body><h1>Side for mitt team</h1>
                            </body> 
                            </html>", ContentType = "text/html; charset=UTF-8" };
        }
          public IActionResult Stat()
        {
            return new ContentResult() { 
                Content = @"<html>
                            <head>
                            <title>BACIT</title>    
                            </head>
                            <body><h1>Statistikk</h1>
                            </body> 
                            </html>", ContentType = "text/html; charset=UTF-8" };
        }
           public IActionResult MinProf()
        {
            return new ContentResult() { 
                Content = @"<html>
                            <head>
                            <title>BACIT</title>    
                            </head>
                            <body><h1>Min profil</h1>
                            </body> 
                            </html>", ContentType = "text/html; charset=UTF-8" };
        }
           public IActionResult Privacy()
        {
            return new ContentResult() { 
                Content = @"<html>
                            <head>
                            <title>BACIT</title>    
                            </head>
                            <body><h1>?</h1>
                            </body> 
                            </html>", ContentType = "text/html; charset=UTF-8" };
        }

      
        

        [HttpGet]
        public IActionResult UserData()
        {

            var data = sqlConnector.GetUsers();
            var model = new UsersModel();
            model.Users = data;
            return View("Users", model);

        }
        [HttpGet]
        public IActionResult UsingRazor()
        {
            var model = new RazorViewModel
            {
                Content = "En time til ørsta rådhus"
            };
            return View("UsingRazor", model);
        }
    }
}
