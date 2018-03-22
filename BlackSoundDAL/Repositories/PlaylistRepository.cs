using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSoundDAL.Entities;

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
                command.CommandText = "SELECT * FROM playlistsTable";
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
        
        public Playlist GetByID(int ID)
        {
            Playlist playlist = new Playlist();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM playlistsTable WHERE ID = @ID";

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

                        playlist.ID = (int)reader["ID"];
                        playlist.Name = (string)reader["Name"];
                        playlist.isPublic = (bool)reader["isPublic"];
                        playlist.userID = (int)reader["userID"];
                    }
                }
            }

            finally
            {
                connection.Close();
            }

            return playlist;
        }

        public bool CheckIfExists(int playlistID, int songID)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            int rowsAffected = 0;

            try
            {
                connection.Open();                
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM playlist_song WHERE playlistID = @playlistID AND songID = @songID";

                IDataParameter parameter = command.CreateParameter();
                parameter = command.CreateParameter();
                parameter.ParameterName = "@PlaylistID";
                parameter.Value = playlistID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SongID";
                parameter.Value = songID;
                command.Parameters.Add(parameter);
                IDataReader reader = command.ExecuteReader();
                rowsAffected = reader.FieldCount;
            }
            finally
            {
                connection.Close();
            }

            if (rowsAffected > 0)
            {
                return true;
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
                 INNER JOIN songsTable AS st ON pls.songID = st.ID
                 INNER JOIN playlistsTable AS plt ON pls.playlistID = plt.ID
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
            command.CommandText = "INSERT INTO playlistsTable (Name, isPublic, userID) VALUES (@Name, @isPublic, @userID)";

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
            command.CommandText = "UPDATE playlistsTable SET Name = @Name, isPublic = @isPublic, userID = @userID WHERE ID = @ID";

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
            command.CommandText = "DELETE FROM playlistsTable WHERE ID=@ID";

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
    }
}
