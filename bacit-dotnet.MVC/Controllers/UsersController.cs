using Microsoft.AspNetCore.Mvc;

namespace bacit_dotnet.MVC.Controllers{
    public class UsersController : Controller{
        public IActionResult Index(){
            return View();
        }}
        [HttpPost]
        public IActionResult Save(UserViewModel model){ 
            return null;
        }
    
}