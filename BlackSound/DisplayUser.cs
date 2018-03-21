using BlackSoundDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSoundDAL.Entities;
using BlackSoundDAL.Repositories;

namespace BlackSound
{
    public class DisplayUser
    {
        private static readonly string conString = "Server=.\\SQLEXPRESS; Database=BlackSound; Integrated Security = True";

        public User AddUser()
        {
            User userInfo = new User();

            Console.WriteLine("Adding new user...");
            Console.Write("Name: ");
            userInfo.Name = Console.ReadLine();
            Console.Write("Email: ");
            userInfo.Email = Console.ReadLine();
            Console.Write("Password: ");
            userInfo.Password = Console.ReadLine();
            Console.Write("Is admin? true/false: ");
            userInfo.IsAdmin = Convert.ToBoolean(Console.ReadLine());
            return userInfo;
        }

        public User UpdateUser()
        {
            User userInfo = new User();

            Console.WriteLine("Updating user...");
            Console.Write("User`s ID: ");
            userInfo.ID = Convert.ToInt32(Console.ReadLine());
            Console.Write("New name: ");
            userInfo.Name = Console.ReadLine();
            Console.Write("New mail: ");
            userInfo.Email = Console.ReadLine();
            Console.Write("New password: ");
            userInfo.Password = Console.ReadLine();
            Console.Write("Is admin? true/false: ");
            userInfo.IsAdmin = Convert.ToBoolean(Console.ReadLine());
            return userInfo;
        }

        public int DeleteUser()
        {
            Console.WriteLine("Deleting user..");
            Console.WriteLine("User`s ID to be deleted: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        //Finish 
        public void DisplayAllUsers(List<User> users)
        {
            List<User> allUsers = allUsers.GetAll();

            foreach (var item in users)
            {
                Console.WriteLine("Users ID: " + item.ID + "|   User`s Name: " + item.Name + "|   User`s Email: " + item.Email);
            }
        }

        public void DisplayUserByID()
        {
            Console.Write("Insert user`s ID: ");
            int userID = Convert.ToInt32(Console.ReadLine());
            UserRepository userRep = new UserRepository(conString);
            User user = userRep.GetByID(userID);
            
                   
            Console.WriteLine("Users ID: " + user.ID + "|   User`s Name: " + user.Name + "|   User`s Email: " + user.Email);
        }

        public void UserLogin(bool logged)
        {
            if(logged == true)
            {
                Console.WriteLine("Login succesfull!" + Environment.NewLine);
            }
            else
            {
                Console.WriteLine("Wrong username or password!" + Environment.NewLine);
            }
            
        }

        public void PrintUserMenu()
        {
            Console.WriteLine(".............USER.............");
            Console.WriteLine("Press 1 to add new user");
            Console.WriteLine("Press 2 to update current user");
            Console.WriteLine("Press 3 to delete user");
            Console.WriteLine("Press 4 to get user by ID");
            Console.WriteLine("Press 5 to get all users");
            Console.WriteLine("Press any other key to exit");

            int caseSwitch = Convert.ToInt32(Console.ReadLine());

            switch (caseSwitch)
            {
                case 1:
                    AddUser();
                    break;
                case 2:
                    UpdateUser();
                    break;
                case 3:
                    DeleteUser();                    
                    break;
                case 4:
                    DisplayUserByID();
                    break;
                case 5:
                    DeleteUser();
                    break;

                default:

                    break;
            }

        }

    }
}
