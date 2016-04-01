using Newtonsoft.Json;
using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.Cookies["questionary"] != null)
            {
                

                ViewBag.Login = "success";
                ViewBag.UserName = JsonConvert.DeserializeObject<UserInfo>(Request.Cookies["questionary"]["userinfo"]).Name;

            }
            else
            {
                ViewBag.Login = "fail";

            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [MyAuthorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}