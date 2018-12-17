using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutNutriYA.Models;

namespace AutNutriYA.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        string cs = "Data Source=" + Environment.CurrentDirectory + "app.db";

        //using (SQLite)
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult InfoApp()
        {
            ViewData["Message"] = "Descarga la App";
            var myUri = new Uri("https://s101moyag8.blob.core.windows.net/app/NutriYA.apk");
            var uri = myUri.AbsolutePath;
            ViewData["Link"]= uri;
            //OnActionExecuted
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
