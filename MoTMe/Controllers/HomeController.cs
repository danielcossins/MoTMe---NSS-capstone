using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoTMe.Models;

namespace MoTMe.Controllers
{
    public class HomeController : Controller
    {
        MoTMeRepository repo = new MoTMeRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.List = new string[] { "1", "2", "3" };

            ViewBag.Thing = repo.GetMessageById(1).Body;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}