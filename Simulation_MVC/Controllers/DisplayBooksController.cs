using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simulation_MVC.Models;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Policy;
using Newtonsoft.Json;

namespace Simulation_MVC.Controllers
{
    public class DisplayBooksController : Controller
    {
        [Route("DisplayBooks/Display")]
        public ActionResult DisplayView()
        {
            return View();
        }
        // GET: CatalogController
        public async Task<IActionResult> Display()
        {
            IEnumerable<BookViewModel> books = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44321/api/DisplayDetails/");
                //HTTP GET
                var responseTask = await client.GetAsync("GetAllBooks");
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<IList<BookViewModel>>();
                    books = readTask.Result;
                }
            }
            return View(books);
        }
        // GET: CatalogController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            BookViewModel book = new BookViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44321/api/DisplayDetails/");
                //HTTP GET
                HttpResponseMessage responseTask = await client.GetAsync("DetailsById?id=" + id.ToString());
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsStringAsync().Result;
                    book = JsonConvert.DeserializeObject<BookViewModel>(readTask);
                }
            }
            return View(book);
        }
    }
}
