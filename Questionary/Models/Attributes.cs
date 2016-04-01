using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionary.Models
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
        {
        public void MyAuthorizeAttribute(HttpContext filterContext)
        {
            // если пользователь не принадлежит роли admin, то он перенаправляется на Home/About
            bool auth = filterContext.HttpContext.Request.Cookies["questionary"] != null;
            if (auth)
            {
                filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary {
                { "controller", "Home" }, { "action", "About" }
                });
            }
        }




    }
    
}