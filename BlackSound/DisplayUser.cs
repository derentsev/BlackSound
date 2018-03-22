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
        User userInfo = new User();
        UserRepository user = new UserRepository(conString);

        public void AddUser()
        {
            Console.WriteLine("Adding new user...");
            Console.Write("Name: ");
            userInfo.Name = Console.ReadLine();
            Console.Write("Email: ");
            userInfo.Email = Console.ReadLine();
            Console.Write("Password: ");
            userInfo.Password = Console.ReadLine();
            Console.Write("Is admin? true/false: ");
            userInfo.IsAdmin = Convert.ToBoolean(Console.ReadLine());
            bool userExists = user.CheckIfUserExists(userInfo.Name, userInfo.Password);

            if (userExists == true)
            {
                Console.WriteLine(Environment.NewLine + "Name or password already used. User not added." + Environment.NewLine);
                return;
            }
            else user.Insert(userInfo);            
        }

        public void UpdateUser()
        {
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
            user.Update(userInfo);
        }

        public void DeleteUser()
        {
            Console.WriteLine("Deleting user..");
            Console.WriteLine("User`s ID to be deleted: ");
            user.Delete(Convert.ToInt32(Console.ReadLine()));
        }

        //Finish 
        public void DisplayAllUsers()
        {
            List<User> allUsers = user.GetAll();
            Console.WriteLine("...........................................................");

            foreach (var item in allUsers)
            {
                Console.WriteLine("Users ID: " + item.ID + "|   User`s Name: " + item.Name + "|   User`s Email: " + item.Email + Environment.NewLine);
            }
        }

        public void DisplayUserByID()
        {
            Console.WriteLine("...................................");
            Console.Write("Insert user`s ID: ");
            int userID = Convert.ToInt32(Console.ReadLine());
            
            userInfo = user.GetByID(userID);

            Console.WriteLine("...........................................................");
            Console.WriteLine("Users ID: " + userInfo.ID + "|   User`s Name: " + userInfo.Name + "|   User`s Email: " + userInfo.Email + Environment.NewLine);
        }

        //public void Register()
        //{
        //    Console.WriteLine("Input 1 to register new user.");
        //    Console.WriteLine("Input 2 to log in.");
        //    Console.WriteLine("Input 3 to exit." + Environment.NewLine);
        //    int caseSwitch = Convert.ToInt32(Console.ReadLine());

        //    switch (caseSwitch)
        //    {
        //        case 1:
        //            AddUser();
        //            break;
        //        case 2:
        //            UserLogin();
        //            break;
        //        case 3:
        //            break;
        //        default:
        //            break;
        //    }
        //}
        public void UserLogin()
        {
            bool logged = false;

            while (logged == false)
            {
                Console.WriteLine(".............LOG IN.............");
                Console.Write("Username: ");
                string userName = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                logged = user.LogIn(userName, password);

                if (logged == true)
                {
                    Console.WriteLine("...................................");
                    Console.WriteLine("       Login succesfull! ");
                    Console.WriteLine("...................................");
                }
                else
                {
                    Console.WriteLine("...................................");
                    Console.WriteLine(Environment.NewLine + "Wrong username or password! Please try again! ");
                    Console.WriteLine("...................................");
                }
            }
        }

        public void PrintUserMenu()
        {
            Console.WriteLine(".............USER MENU.............");
            Console.WriteLine("Press 1 to add new user");
            Console.WriteLine("Press 2 to update  user");
            Console.WriteLine("Press 3 to get all users");
            Console.WriteLine("Press 4 to get user by ID");
            Console.WriteLine("Press 5 to delete a user");
            Console.WriteLine("Press any other key to exit");
            Console.WriteLine("...................................");

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
                    DisplayAllUsers();                    
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
