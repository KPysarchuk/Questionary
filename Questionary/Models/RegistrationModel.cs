using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionary.Models
{
    public class RegistrationModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

    }
}