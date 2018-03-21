using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSoundDAL;
using System.Configuration;
using BlackSoundDAL.Entities;
using BlackSoundDAL.Repositories;

namespace BlackSound
{
    class Program
    {
        private static readonly string conString = "Server=.\\SQLEXPRESS; Database=BlackSound; Integrated Security = True";
            //"metadata=res://*/BlackSoundModel.csdl|res://*/BlackSoundModel.ssdl|res://*/BlackSoundModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=desktop-mkscoba\\sqlexpress;initial catalog=BlackSound;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;\" providerName=\"System.Data.EntityClient";
           
        static void Main(string[] args)
        {
            UserRepository user = new UserRepository(conString);
            SongRepository song = new SongRepository(conString);
            
            
            DisplayUser dispUser = new DisplayUser();
            DisplaySong dispSong = new DisplaySong();

            //AddUser
            //userTable userInfo = dispUser.AddUser();
            //user.Insert(userInfo);

            //UpdateUser
            //userTable userInfo = dispUser.UpdateUser();
            //user.Update(userInfo);

            //DeleteUser
            //user.Delete(dispUser.DeleteUser());

            //GetAllUsers
            //List<User> users = user.GetAll();
            //dispUser.DisplayAllUsers(users);

            //GetUserByID
            //Console.Write("Insert user`s ID: ");
            //int userID = Convert.ToInt32(Console.ReadLine());
            //dispUser.DisplayUserByID(user.GetByID(userID));

            //AddNewSong
            //Song songInfo = dispSong.AddSong();
            //song.Insert(songInfo);

            //UpdateSong
            //song.Update(dispSong.UpdateSong());   

            bool logged = false;

            while(logged == false)
            {
                Console.Write("Username: ");
                string userName = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                logged = user.LogIn(userName, password);
                dispUser.UserLogin(logged);
            }


            Console.WriteLine("Press 1 for user options");
            Console.WriteLine("Press 2 for song options");
            Console.WriteLine("Press 3 for playist options");
            Console.WriteLine("Press any other key to exit");
            int caseSwitch = Convert.ToInt32(Console.ReadLine());

            switch (caseSwitch)
            {
                case 1:
                    dispUser.PrintUserMenu();
                    
                    break;
                case 2:
                    
                    break;
                case 3:
                    ;
                    break;

                default:
                    
                    break;
            }


            Console.ReadLine();
        }
    }
}
