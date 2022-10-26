using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace bacit_dotnet.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ISqlConnector sqlConnector;
        public AdminController(ILogger<AdminController> logger, ISqlConnector sqlConnector)
        {
            _logger = logger;
            this.sqlConnector = sqlConnector;
        }
        public IActionResult Home()
        
        {
            return View();
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
    }
}