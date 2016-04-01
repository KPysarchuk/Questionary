using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionaryDataModel;
using Questionary.Models;
using Newtonsoft.Json;

namespace Questionary.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpPost]
        public ActionResult LogIn(string login, string password)
        {
            //string login = "test";
            //string password = "test";

            using (var db = new QuestionaryEntities())
            {
                bool anyUser = db.Users.Any(x => x.Login == login && x.Password == password);
                var con = System.Web.HttpContext.Current;

                if (anyUser)
                {
                    UserInfo ui = db.Users.Where(x => x.Login == login && x.Password == password).Select(x => new UserInfo
                    {
                        Id = x.id,
                        Login = x.Login,
                        Name = x.Name,
                        Surname = x.Surname
                    }).FirstOrDefault();
                    string cookie = JsonConvert.SerializeObject(ui);
                    HttpCookie myCookie = new HttpCookie("questionary");
                    myCookie["userinfo"] = cookie;
                    myCookie.Expires = DateTime.Now.AddDays(7d);
                    Response.Cookies.Add(myCookie);
                    return Redirect(con.Request.UrlReferrer.AbsoluteUri);
                }
                else
                {
                    return Redirect(con.Request.UrlReferrer.AbsoluteUri);

                }
            }
        }

        public ActionResult LogOut()
        {
            Response.Cookies["questionary"].Expires = DateTime.Now.AddDays(-7);
            return RedirectToAction("Index", "Home");
        }
    }
}