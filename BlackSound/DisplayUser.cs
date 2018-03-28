using BlackSoundDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSoundDAL.Entities;
using BlackSoundDAL.Repositories;
using BlackSoundDAL.Services;

namespace BlackSound
{
    public enum UserOperation
    {
        Add = 1,
        Delete = 2,
        DisplayByID = 3,
        GetAll = 4,
        Update = 5
    }

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
            Console.WriteLine(Environment.NewLine);
            bool userExists = user.CheckIfUserExists(userInfo.Name, userInfo.Password);

            if (userExists == true)
            {
                Console.WriteLine(Environment.NewLine + "Name or password already used. User not added." + Environment.NewLine);
                return;
            }
            else user.Insert(userInfo);            
        }

        public void DeleteUser()
        {
            Console.WriteLine("Deleting user..");
            Console.WriteLine("User`s ID to be deleted: ");
            user.Delete(Convert.ToInt32(Console.ReadLine()));
        }

        public void DisplayAllUsers()
        {
            List<User> allUsers = user.GetAll();
            Console.WriteLine("...........................................................");

            foreach (var item in allUsers)
            {
                Console.WriteLine("Users ID: " + item.ID + "   User`s Name: " + item.Name + "   User`s Email: " + item.Email + Environment.NewLine);
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

        public void PrintUserMenu()
        {
            
            int caseMainMenu = -1;

            while (caseMainMenu != 0)
            {
                if (AuthenticationService.LoggedUser.IsAdmin == true)
                {
                    Console.WriteLine(".............ADMIN MENU.............");
                    Console.WriteLine("1 - Add new user");
                    Console.WriteLine("2 - Update  user");
                    Console.WriteLine("3 - Get all users");
                    Console.WriteLine("4 - Get user by ID");
                    Console.WriteLine("5 - Delete a user");
                    Console.WriteLine("Press any other key to exit");
                    Console.WriteLine("...................................");

                    int operationInt = Convert.ToInt32(Console.ReadLine());
                    UserOperation operation = (UserOperation)operationInt;

                    switch (operation)
                    {
                        case UserOperation.Add:
                            AddUser();
                            break;
                        case UserOperation.Update:
                            UpdateUser();
                            break;
                        case UserOperation.GetAll:
                            DisplayAllUsers();
                            break;
                        case UserOperation.DisplayByID:
                            DisplayUserByID();
                            break;
                        case UserOperation.Delete:
                            DeleteUser();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(".............USER MENU.............");
                    Console.WriteLine("1 - Update user");
                    Console.WriteLine("2 - Get all users");
                    Console.WriteLine("3 - Get user by ID");
                    Console.WriteLine("Press any other key to exit");
                    Console.WriteLine("...................................");

                    int operationInt = Convert.ToInt32(Console.ReadLine());
                    UserOperation operation = (UserOperation)operationInt;

                    switch (operation)
                    {

                        case UserOperation.Update:
                            UpdateUser();
                            break;
                        case UserOperation.GetAll:
                            DisplayAllUsers();
                            break;
                        case UserOperation.DisplayByID:
                            DisplayUserByID();
                            break;
                        default:
                            break;
                    }
                }

                Console.WriteLine("0 - Go back to main menu..");
                Console.WriteLine("1 - Stay in user menu..");
                caseMainMenu = Convert.ToInt32(Console.ReadLine());
            }
        }

        public void UserLogin()
        {
            bool logged = false;

            while (logged == false)
            {
                Console.WriteLine(".............LOG IN.............");
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                logged = user.LogIn(username, password);
                AuthenticationService.AuthenticateUser(username, password);

                if (logged == true)
                {
                    Console.WriteLine("...................................");
                    Console.WriteLine("       Login succesfull! ");
                }
                else
                {
                    Console.WriteLine("...................................");
                    Console.WriteLine(Environment.NewLine + "Wrong username or password! Please try again! ");
                    Console.WriteLine("...................................");
                }
            }
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
    }
}
