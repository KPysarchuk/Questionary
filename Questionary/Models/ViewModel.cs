﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionary.Models
{
    public class ViewModel
    {
        public int id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public HttpPostedFileBase Photo { get; set; }
        public HttpPostedFileBase Song { get; set; }
    }
}