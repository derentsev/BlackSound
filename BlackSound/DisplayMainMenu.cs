using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound
{
    public enum MenuOperation
    {
        User = 1,
        Song = 2,
        Playlist = 3
    }

    class DisplayMainMenu
    {
        public void DisplayMenu()
        {
            DisplayUser dispUser = new DisplayUser();
            DisplaySong dispSong = new DisplaySong();
            DisplayPlaylist dispPlaylist = new DisplayPlaylist();
            int operationInt = 0;

            while (operationInt != 4)
            {
                Console.WriteLine("...................................");
                Console.WriteLine("Press 1 for user options");
                Console.WriteLine("Press 2 for song options");
                Console.WriteLine("Press 3 for playist options");
                Console.WriteLine("Press any other key to exit" + Environment.NewLine);

                operationInt = Convert.ToInt32(Console.ReadLine());
                MenuOperation operation = (MenuOperation)operationInt;

                switch (operation)
                {
                    case MenuOperation.User:
                        dispUser.PrintUserMenu();
                        break;
                    case MenuOperation.Song:
                        dispSong.PrintSongMenu();
                        break;
                    case MenuOperation.Playlist:
                        dispPlaylist.PrintPlaylistMenu();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
