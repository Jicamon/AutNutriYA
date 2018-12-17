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
            var Link= "https://s101moyag8.blob.core.windows.net/app/NutriYA.apk?st=2018-12-17T19%3A28%3A20Z&se=2019-02-01T06%3A59%3A00Z&sp=r&sv=2018-03-28&sr=b&sig=lXRjRJIQJ2%2FkI7FbzvXoIIhfW9l23siTZ%2B9sHPJ0Bus%3D";
            ViewData["Link"]= Link;
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
