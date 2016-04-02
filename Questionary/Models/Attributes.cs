﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionary.Models
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
        {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {


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