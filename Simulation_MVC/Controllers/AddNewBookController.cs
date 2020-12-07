using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simulation_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Simulation_MVC.Controllers
{
    public class AddNewBookController : Controller
    {
        [Route("AddNewBook/Create")]
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            BookViewModel book = new BookViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/");
                //HTTP POST
                var postTask = client.PostAsJsonAsync("api/AddNewBookDetails/AddNewBook/", book);

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(book);
        }

        
    }
}
