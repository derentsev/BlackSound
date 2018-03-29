using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSoundDAL.Entities;
using BlackSoundDAL.Services;

namespace BlackSoundDAL.Repositories
{
    public class PlaylistRepository 
    {
        private readonly string connectionString;

        public PlaylistRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Playlist> GetAll()
        {
            List<Playlist> resultSet = new List<Playlist>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Playlists WHERE isPublic = 'TRUE'";
                IDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        resultSet.Add(new Playlist
                        {
                            ID = (int)reader["ID"],
                            Name = (string)reader["Name"],
                            isPublic = (bool)reader["isPublic"],
                            userID = (int)reader["userID"]
                        });
                    }
                }
            }

            finally
            {
                connection.Close();
            }

            return resultSet;
        }

        public List<Playlist> GetSharedPlaylists(int userID)
        {
            List<Playlist> resultSet = new List<Playlist>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT DISTINCT Playlists.Name AS Playlist_Name, Playlists.userID AS User_ID, user_playlist.playlistID AS Playlist_ID FROM user_playlist 
                                        INNER JOIN Playlists  ON user_playlist.playlistID = playlistID WHERE user_playlist.userID = @userID 
                                        AND Playlists.ID = playlistID";

                IDataParameter parameter = command.CreateParameter();
                parameter = command.CreateParameter();
                parameter.ParameterName = "@userID";
                parameter.Value = userID;
                command.Parameters.Add(parameter);
                IDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        resultSet.Add(new Playlist
                        {
                            ID = (int)reader["Playlist_ID"],
                            Name = (string)reader["Playlist_Name"],
                            userID = (int)reader["User_ID"]
                        });
                    }
                }
            }

            finally
            {
                connection.Close();
            }

            return resultSet;
        }

        public List<Playlist> GetAllByUser(int userID)
        {
            List<Playlist> resultSet = new List<Playlist>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Playlists WHERE userID = @userID";
                IDataParameter parameter = command.CreateParameter();
                parameter = command.CreateParameter();
                parameter.ParameterName = "@userID";
                parameter.Value = userID;
                command.Parameters.Add(parameter);
                IDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        resultSet.Add(new Playlist
                        {
                            ID = (int)reader["ID"],
                            Name = (string)reader["Name"],
                            isPublic = (bool)reader["isPublic"],
                            userID = (int)reader["userID"]
                        });
                    }
                }
            }

            finally
            {
                connection.Close();
            }

            return resultSet;
        }

        public Tuple<Playlist, string> GetByID(int ID)
        {
            Playlist playlist = new Playlist();
            IDbConnection connection = new SqlConnection(connectionString);
            string userName = string.Empty;

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT plt.Name AS Playlist_Name, plt.ID AS Playlist_ID, ut.Name AS User_Name FROM Playlists AS plt INNER JOIN Users AS ut ON plt.userID = ut.ID WHERE plt.ID = @ID";

                IDataParameter parameter = command.CreateParameter();
                parameter = command.CreateParameter();
                parameter.ParameterName = "@ID";
                parameter.Value = ID;
                command.Parameters.Add(parameter);

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        playlist.Name = (string)reader["Playlist_Name"];
                        playlist.ID = (int)reader["Playlist_ID"];
                        userName = (string)reader["User_Name"];
                    }                    
                }
            }

            finally
            {
                connection.Close();
            }

            return Tuple.Create<Playlist, string>(playlist, userName);
        }

        public bool UserOwnsPlaylist(int playlistID)
        {
            int userID = 0;
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT userID FROM Playlists WHERE ID = @ID";

                IDataParameter parameter = command.CreateParameter();
                parameter = command.CreateParameter();
                parameter.ParameterName = "@ID";
                parameter.Value = playlistID;
                command.Parameters.Add(parameter);

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        userID = (int)reader["userID"];
                    }
                }

                if (userID == AuthenticationService.LoggedUser.ID)
                {
                    return true;
                }
                else return false;
            }

            finally
            {
                connection.Close();
            }
        }

        public bool CheckIfExists(int playlistID, int songID)
        {
            //check
            //check
            IDbConnection connection = new SqlConnection(connectionString);
            int rowsAffected = 0;

            try
            {
                connection.Open();                
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(playlistID) FROM playlist_song WHERE playlistID = @playlistID AND songID = @songID";

                IDataParameter parameter = command.CreateParameter();
                parameter = command.CreateParameter();
                parameter.ParameterName = "@playlistID";
                parameter.Value = playlistID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@songID";
                parameter.Value = songID;
                command.Parameters.Add(parameter);

                if((Int32)command.ExecuteScalar() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                connection.Close();
            }

            if (rowsAffected > 0)
            {
               
            }
            else return false;
            
        }

        public void DeleteSongFromPlaylist(int songID, int playlistID)
        {
            //Check why method is always true
            bool exists = CheckIfExists(playlistID, songID);

            if (exists == false)
            {
                Console.WriteLine("Entry doesnt exist! ");
                return;
            }

            IDbConnection connection = new SqlConnection(connectionString);
            IDbCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM playlist_song WHERE playlistID = @playlistID AND songID = @songID";

            IDataParameter parameter = command.CreateParameter();
            parameter = command.CreateParameter();
            parameter.ParameterName = "@PlaylistID";
            parameter.Value = playlistID;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@SongID";
            parameter.Value = songID;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void AddSongToPlaylist(int songID, int playlistID)
        {
            bool exists = CheckIfExists(playlistID, songID);

            if(CheckIfExists(playlistID, songID) == true)
            {
                Console.WriteLine("Entry already exists! ");
                return;
            }

            IDbConnection connection = new SqlConnection(connectionString);
            IDbCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO playlist_song (playlistID, songID) VALUES (@PlaylistID, @SongID)";

            IDataParameter parameter = command.CreateParameter();
            parameter = command.CreateParameter();
            parameter.ParameterName = "@PlaylistID";
            parameter.Value = playlistID;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@SongID";
            parameter.Value = songID;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public Tuple<List<Song>, string> GetSongsInPlaylist(int playlistID)
        {
            List<Song> resultset = new List<Song>();
            IDbConnection connection = new SqlConnection(connectionString);
            string playlistName = "";

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();

                command.CommandText = @"SELECT
                 plt.Name AS Playlist_Name, st.ID AS Song_Id, st.Name AS Song_Name, st.ArtistName AS Song_ArtistName, st.YearCreated AS Song_YearCreated
                 FROM playlist_song AS pls
                 INNER JOIN Songs AS st ON pls.songID = st.ID
                 INNER JOIN Playlists AS plt ON pls.playlistID = plt.ID
                 WHERE pls.playlistID = @PlaylistID";                

                IDataParameter parameter = command.CreateParameter();
                parameter = command.CreateParameter();
                parameter.ParameterName = "@PlaylistID";
                parameter.Value = playlistID;
                command.Parameters.Add(parameter);

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        resultset.Add(new Song
                        {
                            ID = (int)reader["Song_Id"],
                            Name = (string)reader["Song_Name"],
                            ArtistName = (string)reader["Song_ArtistName"],
                            Year = (int)reader["Song_YearCreated"]
                        });

                        playlistName = (string)reader["Playlist_Name"];
                    }
                }
            }

            finally
            {
                connection.Close();
            }

            return Tuple.Create<List<Song>, string>(resultset, playlistName);
        }

        public void Insert(Playlist playlist)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Playlists (Name, isPublic, userID) VALUES (@Name, @isPublic, @userID)";

            IDataParameter parameter = command.CreateParameter();
            parameter = command.CreateParameter();
            parameter.ParameterName = "@Name";
            parameter.Value = playlist.Name;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@isPublic";
            parameter.Value = playlist.isPublic;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@userID";
            parameter.Value = playlist.userID;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(Playlist playlist)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Playlists SET Name = @Name, isPublic = @isPublic, userID = @userID WHERE ID = @ID";

            IDataParameter parameter = command.CreateParameter();
            parameter = command.CreateParameter();
            parameter.ParameterName = "@Name";
            parameter.Value = playlist.Name;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@ID";
            parameter.Value = playlist.ID;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@isPublic";
            parameter.Value = playlist.isPublic;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@userID";
            parameter.Value = playlist.userID;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Delete(int playlistID)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Playlists WHERE ID=@ID";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@ID";
            parameter.Value = playlistID;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Share(int userToShareWithID, int playlistID)
        {
            if(UserOwnsPlaylist(playlistID))
            {
                IDbConnection connection = new SqlConnection(connectionString);
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO user_playlist (playlistID, userID) VALUES (@PlaylistID, @UserID)";

                IDataParameter parameter = command.CreateParameter();
                parameter = command.CreateParameter();
                parameter.ParameterName = "@PlaylistID";
                parameter.Value = playlistID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UserID";
                parameter.Value = userToShareWithID;
                command.Parameters.Add(parameter);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
