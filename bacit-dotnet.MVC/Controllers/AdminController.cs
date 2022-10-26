<<<<<<< HEAD
using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.DataAccess;
using Microsoft.AspNetCore.Mvc;
using bacit_dotnet.MVC.Models;

=======
using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
>>>>>>> c254d1ef0d9792f6d5f19d1acd5161e93589e286
namespace bacit_dotnet.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ISqlConnector sqlConnector;
<<<<<<< HEAD

=======
>>>>>>> c254d1ef0d9792f6d5f19d1acd5161e93589e286
        public AdminController(ILogger<AdminController> logger, ISqlConnector sqlConnector)
        {
            _logger = logger;
            this.sqlConnector = sqlConnector;
        }
<<<<<<< HEAD


        [HttpGet]
        public IActionResult AdmViewSug()
        {

            var data = sqlConnector.FetchSug();
            var model = new SuggestionsModel();
            model.suggestions = data;

            return View("AdmAllSug",model);

        }

        public IActionResult AdmProfile()
=======
        public IActionResult Home()
>>>>>>> c254d1ef0d9792f6d5f19d1acd5161e93589e286
        
        {
            return View();
        }
<<<<<<< HEAD

        
        
    }
}
=======
        
       
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
    }
}
>>>>>>> c254d1ef0d9792f6d5f19d1acd5161e93589e286
