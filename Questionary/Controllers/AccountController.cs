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

        [AuthorizeUser]

        public ActionResult LogOut()
        {
            Response.Cookies["questionary"].Expires = DateTime.Now.AddDays(-7);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            if (Request.Cookies["questionary"] != null)
            {
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationModel rm)
        {
            
            using (var db = new QuestionaryEntities())
            {

                Users user = new Users
                {
                    Login = rm.Login,
                    Password = rm.Password,
                    Name = rm.Name,
                    Surname = rm.Surname,
                    City = rm.City,
                    Country = rm.Country
                };
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login" , "Account" , new { login = rm.Login , password = rm.Password });
            }
        }

        [HttpGet]
        [AuthorizeUser]

        public ActionResult Edit()
        {
            var httpContext = System.Web.HttpContext.Current;
            if (httpContext.Request.Cookies["questionary"] == null)
            {
                return HttpNotFound();
            }
            int id = JsonConvert.DeserializeObject<UserInfo>(Request.Cookies["questionary"]["userinfo"]).Id;
            using (var db = new QuestionaryEntities())
            {
                ViewModel usr = db.Users.Select(x => new ViewModel
                {
                    id = x.id,
                    Login = x.Login,
                    Name = x.Name,
                    Surname = x.Surname,
                    Country = x.Country,
                    City = x.City,
                }).First(x => x.id == id);
            
                return View(usr);
            }

        }

        [HttpPost]
        [AuthorizeUser]

        public ActionResult Edit(ViewModel vm)
        {
            using (var db = new QuestionaryEntities())
            {

                Users user = db.Users.First(x => x.id == vm.id);
                user.Name = vm.Name;
                user.Surname = vm.Surname;
                user.Country = vm.Country;
                user.City = vm.City;
                if (vm.Photo != null)
                {
                    if (vm.Photo.ContentType.Contains("image"))
                    {
                        string fileName = System.IO.Path.GetFileName(vm.Photo.FileName);
                        if (!System.IO.Directory.Exists(Server.MapPath("~/MediaContent/" + vm.id)))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("~/MediaContent/" + vm.id));
                        }
                        vm.Photo.SaveAs((Server.MapPath("~/MediaContent/" + vm.id + "/" + fileName)));
                        user.Photo = "/MediaContent/" + vm.id + "/" + fileName;
                    }
                }
                if (vm.Song != null)
                {
                    if(vm.Song.ContentType.Contains("audio"))
                    {
                        string fileName = System.IO.Path.GetFileName(vm.Song.FileName);
                        if (!System.IO.Directory.Exists(Server.MapPath("~/MediaContent/" + vm.id)))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("~/MediaContent/" + vm.id));
                        }
                        vm.Song.SaveAs((Server.MapPath("~/MediaContent/" + vm.id + "/" + fileName)));
                        user.Song = "/MediaContent/" + vm.id + "/" + fileName;
                    }
                    
                }
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }

        [AuthorizeUser]
         public ActionResult Profile()
        {
            int id = JsonConvert.DeserializeObject<UserInfo>(Request.Cookies["questionary"]["userinfo"]).Id;
            using (var db = new QuestionaryEntities())
            {
                UserProfile usr = db.Users.Select(x => new UserProfile
                {
                    id = x.id,
                    Login = x.Login,
                    Name = x.Name,
                    Surname = x.Surname,
                    Country = x.Country,
                    City = x.City,
                    Photo = x.Photo,
                    Song = x.Song
                }).First(x => x.id == id);

                return View(usr);
            }
        }
    }
}
