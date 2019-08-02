using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordCount.Models;
using WordCount.Dal.Interfaces;

namespace WordCount.Controllers
{
    public class HomeController : Controller
    {
        IOutputDal outputDal;

        public HomeController(IOutputDal outputDal)
        {
            this.outputDal = outputDal;
        }

        public IActionResult Index()
        {
            ViewBag.WordsAndSentences = outputDal.GetWordsAndSentences();
            ViewBag.WordCount = outputDal.GetCountByWord();
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
