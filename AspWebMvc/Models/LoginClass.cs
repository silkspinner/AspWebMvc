using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspWebMvc.Models
{
    public class LoginClass
    {
        public LoginClass() { }
        public LoginClass(string user, string password)
        {
            Username = user;
            Password = password;
        }
        public string Username { set; get; }
        public string Password { set; get; }
    }
}