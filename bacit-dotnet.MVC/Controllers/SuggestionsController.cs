﻿using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.DataAccess;
using Microsoft.AspNetCore.Mvc;
using bacit_dotnet.MVC.Models;

namespace bacit_dotnet.MVC.Controllers
{
    public class SuggestionsController : Controller
    {
        private readonly ILogger<SuggestionsController> _logger;
        private readonly ISqlConnector sqlConnector;

        public SuggestionsController(ILogger<SuggestionsController> logger, ISqlConnector sqlConnector)
        {
            _logger = logger;
            this.sqlConnector = sqlConnector;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewSug()
        {

            var data = sqlConnector.FetchSug(); //henter alle forslag og sier at de skal vises på AlleFor
            var model = new SuggestionsModel();
            model.suggestions = data;

            return View("AlleFor",model);

        }

        [HttpPost]
        public IActionResult Save(SuggestionViewModel model) 
        {
            sqlConnector.SetSug(model);
            var data = sqlConnector.FetchSug(); // Denne metoden henter ut data
            var models = new SuggestionsModel();
            models.suggestions = data;
            return View(models);
        }
       
        [HttpGet]
         public IActionResult Save(int id) 
        {
            Console.WriteLine(id);
            var data = sqlConnector.SaveSug(id); // Henter ut data med id
            var model = new SuggestionsModel();
            model.suggestions = data;
            return View(model);
        }
       
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Console.WriteLine(id);
            var data = sqlConnector.UpdateSug(id); // Denne metoden henter ut data
            var model = new SuggestionsModel();
            model.suggestions = data;
            return View(model);

        }

        [HttpPost]
        public IActionResult Update(SuggestionViewModel model){
            Console.WriteLine("Update");
            sqlConnector.SetUpSug(model);
            Console.WriteLine("Model");
            int id = 0;
            string? stringid = model.Sug_ID;
            
               Boolean idcheck = string.IsNullOrEmpty(stringid); //hvis id er null/tom skal den returnere feilmelding
                if(!idcheck){
                id = Int32.Parse(stringid);
                Console.WriteLine(id);
                } else{
                    Console.WriteLine("Sug_Id is empty or Null");
                }
                
    
            var data = sqlConnector.UpdateSug(id); //hvis forslaget har en id skal forslaget vises
            var result = new SuggestionsModel();
            result.suggestions = data;
            return View("Save", result);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Console.WriteLine(id);
            sqlConnector.DeleteSug(id); // Denne metoden sletter data
            return View("Delete");

        }
        
    }
}
