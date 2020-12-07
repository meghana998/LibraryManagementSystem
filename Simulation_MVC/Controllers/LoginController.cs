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
    public class LoginController : Controller
    {
        [Route("Login/Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44396/");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync<LoginViewModel>("api/Token", user);
                if (postTask.IsSuccessStatusCode)
                {
                    var token = await postTask.Content.ReadAsStringAsync();
                    TempData["token"] = token;
                    return RedirectToAction("Display", "DisplayBooks");
                }
            }

            return RedirectToAction("Login", "Login");
        }

    }
}
