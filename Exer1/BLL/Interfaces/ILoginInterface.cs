using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface ILoginInterface
    {
        bool authenticateUsername(string Username, string Password);
        bool authenticateEmail(string Email, string Password);
        bool isEmail(string input);
    }
}
