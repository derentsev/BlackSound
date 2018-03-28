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
    public enum SongOperation
    {
        Add = 1,
        Delete = 2,
        DisplayByID = 3,
        GetAll = 4,
        Update = 5
    }

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

        public void DeleteSong()
        {
            Console.WriteLine("...................................");
            Console.WriteLine("Deleting song..");
            Console.WriteLine("Song`s ID to be deleted: ");
            songRepo.Delete(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine("The song has been deleted! ");
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

        public void PrintSongMenu()
        {
            int caseMainMenu = -1;

            while (caseMainMenu != 0)
            {
                if (AuthenticationService.LoggedUser.IsAdmin == true)
                {
                    Console.WriteLine(".............SONG MENU FOR ADMIN.............");
                    Console.WriteLine("1 - Add new song");
                    Console.WriteLine("2 - Update song");
                    Console.WriteLine("3 - Get all song");
                    Console.WriteLine("4 - Get song by ID");
                    Console.WriteLine("5 - Delete song");
                    Console.WriteLine("Press any other key to exit");
                    Console.WriteLine(".............................................");

                    int operationInt = Convert.ToInt32(Console.ReadLine());
                    SongOperation operation = (SongOperation)operationInt;

                    switch (operation)
                    {
                        case SongOperation.Add:
                            AddSong();
                            break;
                        case SongOperation.Update:
                            UpdateSong();
                            break;
                        case SongOperation.GetAll:
                            GetAllSongs();
                            break;
                        case SongOperation.DisplayByID:
                            DisplaySongByID();
                            break;
                        case SongOperation.Delete:
                            DeleteSong();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(".............SONG MENU FOR USER.............");
                    Console.WriteLine("1 - Get all songs");
                    Console.WriteLine("2 - Get song by ID");
                    Console.WriteLine("Press any other key to exit");
                    Console.WriteLine("............................................");

                    int operationInt = Convert.ToInt32(Console.ReadLine());
                    SongOperation operation = (SongOperation)operationInt;

                    switch (operation)
                    {
                        case SongOperation.GetAll:
                            GetAllSongs();
                            break;
                        case SongOperation.DisplayByID:
                            DisplaySongByID();
                            break;
                        default:
                            break;
                    }
                }

                Console.WriteLine("0 - Go back to main menu");
                Console.WriteLine("1 - Stay in song menu");
                caseMainMenu = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
