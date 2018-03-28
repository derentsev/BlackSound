using BlackSoundDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSoundDAL.Services
{
    public class AuthenticationService
    {
        public static User LoggedUser { get; private set; }

        public static void AuthenticateUser(string username, string password)
        {
            UserRepository userRepo = new UserRepository("Server=.\\SQLEXPRESS; Database=BlackSound; Integrated Security = True");
            AuthenticationService.LoggedUser = userRepo.GetUserByNameAndPass(username, password);
            bool isAdmin = LoggedUser.IsAdmin;
        }
    }
}
