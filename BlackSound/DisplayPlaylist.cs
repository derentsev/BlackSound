using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSoundDAL;
using BlackSoundDAL.Entities;
using BlackSoundDAL.Repositories;

namespace BlackSound
{
    public class DisplayPlaylist
    {
        private static readonly string conString = "Server=.\\SQLEXPRESS; Database=BlackSound; Integrated Security = True";
        PlaylistRepository playlistRepo = new PlaylistRepository(conString);
        Playlist playlistInfo = new Playlist();
        UserRepository user = new UserRepository(conString);

        public void AddPlaylist()
        {
            Console.WriteLine("Adding new playlist...");
            Console.Write("Name: ");
            playlistInfo.Name = Console.ReadLine();
            Console.Write("Creator`s ID: ");
            playlistInfo.userID = Convert.ToInt32(Console.ReadLine());
            Console.Write("Playlist is public (true/false): ");
            playlistInfo.isPublic = Convert.ToBoolean(Console.ReadLine());
            playlistRepo.Insert(playlistInfo);
        }

        public void UpdatePlaylist()
        {
            Console.WriteLine("Updating playlist...");
            Console.WriteLine("Insert playlists's ID to be updated: ");
            playlistInfo.ID = Convert.ToInt32(Console.ReadLine());
            Console.Write("New name: ");
            playlistInfo.Name = Console.ReadLine();
            Console.Write("Creator`s ID: ");
            playlistInfo.userID = Convert.ToInt32(Console.ReadLine());
            Console.Write("Playlist is public true/false");
            playlistInfo.isPublic = Convert.ToBoolean(Console.ReadLine());
            playlistRepo.Update(playlistInfo);
        }

        public void AddSongToPlaylist()
        {
            int exitNum = 0;

            while (exitNum == 1 || exitNum == 0)
            {

                if (exitNum == 0)
                {
                    Console.WriteLine(("Adding song to a playlist..."));
                    DisplaySong song = new DisplaySong();
                    song.GetAllSongs();
                    Console.WriteLine("..........................................");
                    GetAllPlaylists();
                    Console.WriteLine("..........................................");
                    Console.WriteLine("Song ID: ");
                    int songID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Playlist ID: ");
                    int playlistID = Convert.ToInt32(Console.ReadLine());

                    playlistRepo.AddSongToPlaylist(songID, playlistID);
                    Console.WriteLine(Environment.NewLine + "Input 0 to continue adding songs");
                    Console.WriteLine("Input 1 to go to main menu");
                    Console.WriteLine("Input any other number to exit");

                    exitNum = Convert.ToInt32(Console.ReadLine());
                }
                else if (exitNum == 1)
                {
                    DisplayMainMenu menu = new DisplayMainMenu();
                    menu.DisplayMenu();
                }
            }
        }

        public void DeleteSongFromPlaylist()
        {
            Console.WriteLine("Deleting song from playlist..");
            Console.Write("Playlist`s ID: ");
            int playlistID = Convert.ToInt32(Console.ReadLine());
            Console.Write("Song`s ID: ");
            int songID = Convert.ToInt32(Console.ReadLine());
            playlistRepo.DeleteSongFromPlaylist(songID, playlistID);
            Console.WriteLine("Song removed!" + Environment.NewLine);
        }

        public void GetAllPlaylists()
        {
            List<Playlist> allPlaylists = playlistRepo.GetAll();
            DisplayUser dispUser = new DisplayUser();

            foreach (var item in allPlaylists)
            {
                Console.WriteLine("Playlist ID:" + item.ID + "      Playlist's name: " + item.Name + Environment.NewLine);
            }
        }

        public void GetSongsFromPlaylist()
        {
            Console.WriteLine("Insert playlist`s ID  to retrieve songs from: ");
            int playlistID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(".....................................................");
            Tuple<List<Song>, string> getvalue = playlistRepo.GetSongsInPlaylist(playlistID);
            List<Song> songsFromPlaylist = getvalue.Item1;
            string playlistName = getvalue.Item2;
            //playlistRepo.GetSongsInPlaylist(playlistID);
            Console.WriteLine("Playlist name:" + playlistName + Environment.NewLine);            

            foreach (var item in songsFromPlaylist)
            {
                Console.WriteLine("Song`s artist:" + item.ArtistName + "      Song's name: " + item.Name + Environment.NewLine);
            }

            Console.WriteLine(".....................................................");
        }

        public void DeletePlaylist()
        {
            Console.WriteLine("Deleting playlist..");
            Console.WriteLine("Playlist`s ID to be deleted: " + Environment.NewLine);
            playlistRepo.Delete(Convert.ToInt32(Console.ReadLine()));
        }

        public void DisplayPlaylistByID()
        {
            Console.Write("Insert playlist`s ID:");
            int playlistID = Convert.ToInt32(Console.ReadLine());            
            playlistInfo = playlistRepo.GetByID(playlistID);
            Console.WriteLine("Playlist's ID: " +  playlistInfo.ID + " Playlist`s Name: " + playlistInfo.Name + Environment.NewLine);            
        }

        public void PrintPlaylistMenu()
        {
            Console.WriteLine(".............PLAYLIST.............");
            Console.WriteLine("Press 1 to add new playlist");
            Console.WriteLine("Press 2 to update current playlist");
            Console.WriteLine("Press 3 to get all playlists");
            Console.WriteLine("Press 4 to get playlist by ID");
            Console.WriteLine("Press 5 to delete a playlist");
            Console.WriteLine("Press 6 to add a song to a playlist");
            Console.WriteLine("Press 7 to get all songs from a playlist");
            Console.WriteLine("Press 8 to delete a song from a playlist");
            Console.WriteLine("Press any other key to exit");            
            Console.WriteLine("..................................");

            int caseSwitch = Convert.ToInt32(Console.ReadLine());

            switch (caseSwitch)
            {
                case 1:
                    AddPlaylist();
                    break;
                case 2:
                    UpdatePlaylist();
                    break;
                case 3:
                    GetAllPlaylists();
                    break;
                case 4:
                    DisplayPlaylistByID();
                    break;
                case 5:
                    DeletePlaylist();
                    break;
                case 6:
                    AddSongToPlaylist();
                    break;
                case 7:
                    GetSongsFromPlaylist();
                    break;
                case 8:
                    DeleteSongFromPlaylist();
                    break;
                    
                default:
                    break;
            }
        }
    }
}
