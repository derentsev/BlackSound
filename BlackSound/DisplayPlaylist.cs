using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSoundDAL;
using BlackSoundDAL.Entities;
using BlackSoundDAL.Repositories;
using BlackSoundDAL.Services;

namespace BlackSound
{

    public enum PlaylistOperation : byte
    {
        Create = 1,
        Update = 2,
        GetSongsFromPlaylist = 3,
        Delete = 4,
        ReadByID = 5,
        AddSongToPlaylist = 6,
        DeleteSongFromPlaylist = 7,
        ReadAll = 8,
        Share = 9,
        GetMyPlaylists = 10,
        SharedPlaylisys = 11
    }

    public class DisplayPlaylist
    {
        private static readonly string conString = "Server=.\\SQLEXPRESS; Database=BlackSound; Integrated Security = True";
        PlaylistRepository playlistRepo = new PlaylistRepository(conString);
        Playlist playlistInfo = new Playlist();
        UserRepository user = new UserRepository(conString);
        DisplayUser dispUser = new DisplayUser();
        List<Playlist> playlists = new List<Playlist>();
        DynamicRepository<Playlist> dynRepo = new DynamicRepository<Playlist>(conString);

        public void AddPlaylist()
        {
            Console.WriteLine("Adding new playlist...");
            Console.Write("Name: ");
            playlistInfo.Name = Console.ReadLine();
            playlistInfo.userID = AuthenticationService.LoggedUser.ID;
            Console.Write("Playlist is public (true/false): ");
            playlistInfo.isPublic = Convert.ToBoolean(Console.ReadLine());
            dynRepo.Insert(playlistInfo);
        }

        public void UpdatePlaylist()
        {
            Console.WriteLine("Updating playlist...");
            Console.WriteLine("Insert playlists's ID to be updated: ");
            playlistInfo.ID = Convert.ToInt32(Console.ReadLine());

            if (playlistRepo.UserOwnsPlaylist(playlistInfo.ID) || AuthenticationService.LoggedUser.ID == 1)
            {
                Console.Write("New name: ");
                playlistInfo.Name = Console.ReadLine();
                playlistInfo.userID = AuthenticationService.LoggedUser.ID;
                Console.Write("Playlist is public (true/false): ");
                playlistInfo.isPublic = Convert.ToBoolean(Console.ReadLine());
                dynRepo.Update(playlistInfo);
            }
            else
            {
                Console.WriteLine("You can not update the playlist, as you are not the creator! " + Environment.NewLine);
            }
            
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

                    if (playlistRepo.UserOwnsPlaylist(playlistInfo.ID) || AuthenticationService.LoggedUser.ID == 1)
                    {
                        playlistRepo.AddSongToPlaylist(songID, playlistID);
                    }
                    else
                    {
                        Console.WriteLine("You can not add song to the playlist, as you are not the owner! ");
                    }

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
            GetAllPlaylists();
            Console.WriteLine("Deleting song from playlist..");
            Console.Write("Playlist`s ID: ");
            int playlistID = Convert.ToInt32(Console.ReadLine());
            //Show songs from the playlist
            Console.Write("Song`s ID: ");
            int songID = Convert.ToInt32(Console.ReadLine());
            playlistRepo.DeleteSongFromPlaylist(songID, playlistID);
            Console.WriteLine("Song removed!" + Environment.NewLine);
        }

        public void GetPlaylistsByUser()
        {
            playlists = playlistRepo.GetAllByUser(AuthenticationService.LoggedUser.ID);
            playlists.AddRange(playlistRepo.GetSharedPlaylists(AuthenticationService.LoggedUser.ID));

            foreach (var item in playlists)
            {
                if (item.userID == AuthenticationService.LoggedUser.ID)
                {
                    {
                        Console.WriteLine("Playlist ID:" + item.ID + "      Playlist's name: " + item.Name + Environment.NewLine);
                    }
                }
            }
        }

        public void GetAllPlaylists()
        {
            playlists = dynRepo.GetAll();

            foreach (var item in playlists)
            {
                if (playlistInfo.isPublic || playlistInfo.userID == AuthenticationService.LoggedUser.ID || AuthenticationService.LoggedUser.IsAdmin)
                {
                    {
                        Console.WriteLine("Playlist ID:" + item.ID + "      Playlist's name: " + item.Name + Environment.NewLine);
                    }
                }
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
            dynRepo.DeleteByID(Convert.ToInt32(Console.ReadLine()));
        }

        public void DisplayPlaylistByID()
        {
            Console.Write("Insert playlist`s ID:");
            int playlistID = Convert.ToInt32(Console.ReadLine());
            Tuple<Playlist, string> getvalue = playlistRepo.GetByID(playlistID);
            playlistInfo = getvalue.Item1;
            if(playlistInfo.isPublic || playlistInfo.userID == AuthenticationService.LoggedUser.ID || AuthenticationService.LoggedUser.IsAdmin)
            {
                Console.WriteLine("Playlist's ID: " + playlistInfo.ID + "," + "  Playlist`s Name: " + playlistInfo.Name + "," + "  Creator`s name: " + getvalue.Item2 + Environment.NewLine);
            }
            else
            {
                Console.WriteLine("There is no public playlist with that ID." + Environment.NewLine);
            }
        }

        public void SharePlaylist()
        {
            Console.WriteLine("Sharing playlist..");
            GetPlaylistsByUser();
            Console.Write("Insert playlist`s ID: ");
            int playlistID = Convert.ToInt32(Console.ReadLine());
            dispUser.DisplayAllUsers();
            Console.Write("Insert user`s ID: ");
            int userID = Convert.ToInt32(Console.ReadLine());
            playlistRepo.Share(userID, playlistID);
        }

        public void GetAllSharedPlaylistsWithMe()
        {
            playlists = playlistRepo.GetSharedPlaylists(AuthenticationService.LoggedUser.ID);

            Console.WriteLine("Users have shared those playlists with you.....");
            Console.WriteLine("*************************************");

            foreach (var item in playlists)
            {
                if (item.userID == AuthenticationService.LoggedUser.ID)
                {
                    Console.WriteLine("Playlist ID:" + item.ID + Environment.NewLine
                        + "Playlist's name: " + item.Name + Environment.NewLine
                        + "Creator: " + user.GetByID((int)item.userID).Name + Environment.NewLine);
                }

                Console.WriteLine("*************************************");
            }
        }

        public void PrintPlaylistMenu()
        {
            Console.WriteLine(".............PLAYLIST.............");
            Console.WriteLine("1 - Add new playlist");
            Console.WriteLine("2 - Update playlist");
            Console.WriteLine("3 - Get all songs in a playlist");
            Console.WriteLine("4 - Delete playlsit");
            Console.WriteLine("5 - Get playlist");
            Console.WriteLine("6 - Add songs to playlist");
            Console.WriteLine("7 - Delete songs from playlist");
            Console.WriteLine("8 - Get all playlists");
            Console.WriteLine("9 - Share a playlist");
            Console.WriteLine("10 - Get all my playlists");
            Console.WriteLine("11 - Get all my shared playlists");
            Console.WriteLine("Press any other key to exit");            
            Console.WriteLine("..................................");

            int operationInt = Convert.ToInt32(Console.ReadLine());
            PlaylistOperation operation = (PlaylistOperation)operationInt;

            switch (operation)
            {
                case PlaylistOperation.Create:
                    AddPlaylist();
                    break;
                case PlaylistOperation.Update:
                    UpdatePlaylist();
                    break;
                case PlaylistOperation.ReadAll:
                    GetAllPlaylists();
                    break;
                case PlaylistOperation.ReadByID:
                    DisplayPlaylistByID();
                    break;
                case PlaylistOperation.Delete:
                    DeletePlaylist();
                    break;
                case PlaylistOperation.AddSongToPlaylist:
                    AddSongToPlaylist();
                    break;
                case PlaylistOperation.GetSongsFromPlaylist:
                    GetSongsFromPlaylist();
                    break;
                case PlaylistOperation.DeleteSongFromPlaylist:
                    DeleteSongFromPlaylist();
                    break;
                case PlaylistOperation.Share:
                    SharePlaylist();
                    break;
                case PlaylistOperation.GetMyPlaylists:
                    GetPlaylistsByUser();
                    break;
                case PlaylistOperation.SharedPlaylisys:
                    GetAllSharedPlaylistsWithMe();
                    break;
                default:
                    break;
            }
        }
    }
}
