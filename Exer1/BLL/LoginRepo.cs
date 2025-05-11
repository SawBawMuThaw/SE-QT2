using BLL.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoginRepo : ILoginInterface
    {
        private readonly DALservice service;

        public LoginRepo()
        {
            service = new DALservice();
        }

        public bool authenticateEmail(string Email, string Password)
        {
            var user = service.Users.FirstOrDefault(u => u.Email == Email);

            if(user != null)
            {
                if(user.Password == Password)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool authenticateUsername(string Username, string Password)
        {
            var user = service.Users.FirstOrDefault(u => u.UserName == Username);

            if (user != null)
            {
                if (user.Password == Password)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool isEmail(string input)
        {
            if (input.Contains("@"))
            {
                return true;
            }
            return false;
        }
    }
}
