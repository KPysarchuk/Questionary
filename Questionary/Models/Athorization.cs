
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionary.Models
{
    public static class Athorization
    {
        public static bool AuthorizationCheck()
        {

            var httpContext = System.Web.HttpContext.Current;
            if (httpContext.Request.Cookies["questionary"] != null)
            {


                return true;

            }
            else
            {
                return false;

            }

        }
    }
}