using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSoundDAL.Entities;
using BlackSoundDAL.Repositories;

namespace BlackSound
{
    public class DisplaySong
    {
        private static readonly string conString = "Server=.\\SQLEXPRESS; Database=BlackSound; Integrated Security = True";
        SongRepository songRepo = new SongRepository(conString);
        Song songInfo = new Song();

        public void AddSong()
        {
            Console.WriteLine("...................................");
            Console.WriteLine("Adding new song...");
            Console.Write("Name: ");
            songInfo.Name = Console.ReadLine();
            Console.Write("Year: ");
            songInfo.Year = Convert.ToInt32(Console.ReadLine());
            Console.Write("Artist: ");
            songInfo.ArtistName = Console.ReadLine();
            songRepo.Insert(songInfo);
        }

        public void UpdateSong()
        {
            Console.WriteLine("...................................");
            Console.WriteLine("Updating new song...");
            Console.WriteLine("Insert song`s ID: ");
            songInfo.ID = Convert.ToInt32(Console.ReadLine());
            Console.Write("New name: ");
            songInfo.Name = Console.ReadLine();
            Console.Write("New year: ");
            songInfo.Year = Convert.ToInt32(Console.ReadLine());
            Console.Write("New artist: ");
            songInfo.ArtistName = Console.ReadLine();
            songRepo.Update(songInfo);
        }
         
        public void GetAllSongs()
        {
            List<Song> allSongs = songRepo.GetAll();

            foreach (var item in allSongs)
            {
                Console.WriteLine("...........................................................................................");
                Console.WriteLine("Song ID: " + item.ID + " Song's name: " + item.Name + "   Song's year: " + item.Year);
                Console.WriteLine("...........................................................................................");
            }

        }

        public void DisplaySongByID()
        {
            Console.WriteLine("..................................."); ;
            Console.Write("Insert song`s ID: ");
            int songID = Convert.ToInt32(Console.ReadLine());
            songInfo = songRepo.GetByID(songID);
            Console.WriteLine("ID: " + songInfo.ID + "    " + songInfo.ArtistName + "  -  " + songInfo.Name);
            Console.WriteLine("...................................");
        }

        public void DeleteSong()
        {
            Console.WriteLine("...................................");
            Console.WriteLine("Deleting song..");
            Console.WriteLine("Song`s ID to be deleted: ");
            songRepo.Delete(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine("The song has been deleted! ");
        }

        public void PrintSongMenu()
        {
            Console.WriteLine(".............SONG MENU.............");
            Console.WriteLine("Press 1 to add new song");
            Console.WriteLine("Press 2 to update song");
            Console.WriteLine("Press 3 to get all songs");
            Console.WriteLine("Press 4 to get song by ID");
            Console.WriteLine("Press 5 to delete a song");
            Console.WriteLine("Press any other key to exit");
            Console.WriteLine("...................................");

            int caseSwitch = Convert.ToInt32(Console.ReadLine());

            switch (caseSwitch)
            {
                case 1:
                    AddSong();
                    break;
                case 2:
                    UpdateSong();
                    break;
                case 3:
                    GetAllSongs();
                    break;
                case 4:
                    DisplaySongByID();
                    break;
                case 5:
                    DeleteSong();
                    break;

                default: 
                    break;
            }

        }
    }
}
