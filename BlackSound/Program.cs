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
            //CreateDatabase.CreateDB();
            UserRepository user = new UserRepository(conString);
            DisplayUser dispUser = new DisplayUser();
            SongRepository song = new SongRepository(conString);
            DisplaySong dispSong = new DisplaySong();
            PlaylistRepository playlist = new PlaylistRepository(conString);
            DisplayPlaylist dispPlaylist = new DisplayPlaylist();
            DisplayMainMenu menu = new DisplayMainMenu();

            dispUser.UserLogin();
            menu.DisplayMenu();            
        }
    }
}
