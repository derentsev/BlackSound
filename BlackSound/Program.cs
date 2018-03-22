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
           
        static void Main(string[] args)
        {
            UserRepository user = new UserRepository(conString);
            SongRepository song = new SongRepository(conString);
            PlaylistRepository playlist = new PlaylistRepository(conString);
            DisplayMainMenu menu = new DisplayMainMenu();
            DisplayUser dispUser = new DisplayUser();
            DisplaySong dispSong = new DisplaySong();
            DisplayPlaylist dispPlaylist = new DisplayPlaylist();

            bool isAdmin = false;


            dispUser.UserLogin();
            menu.DisplayMenu();            
        }
    }
}
