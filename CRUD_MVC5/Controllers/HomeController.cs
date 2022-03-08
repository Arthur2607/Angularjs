using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_MVC5.Models;
using CRUD_MVC5.Controllers;

namespace CRUD_MVC5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
                                     
           return View();
                           
        }  

        public ActionResult login()
        {
            
            return View();
        }


       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}