using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound
{
    class DisplayMainMenu
    {
        public void DisplayMenu()
        {
            DisplayUser dispUser = new DisplayUser();
            DisplaySong dispSong = new DisplaySong();
            DisplayPlaylist dispPlaylist = new DisplayPlaylist();
            int caseSwitch = 0;

            while (caseSwitch != 4)
            {
            Console.WriteLine("Press 1 for user options");
            Console.WriteLine("Press 2 for song options");
            Console.WriteLine("Press 3 for playist options");
            Console.WriteLine("Press any other key to exit" + Environment.NewLine);
            caseSwitch = Convert.ToInt32(Console.ReadLine());

            
                switch (caseSwitch)
                {
                    case 1:
                        dispUser.PrintUserMenu();
                        break;
                    case 2:
                        dispSong.PrintSongMenu();
                        break;
                    case 3:
                        dispPlaylist.PrintPlaylistMenu();
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
