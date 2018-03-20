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
    class PlaylistRepository
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
                command.CommandText = "SELECT * FROM playlistTable";
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

        //id fix
        public Playlist GetByID(int ID)
        {
            Playlist playlist = new Playlist();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM playlistTable WHERE ID = @ID";

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

        //Finish Select from 
        public List<Song> GetPlaylistInfo(int playlistID)
        {
            List<Song> resultset = new List<Song>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT song.Name, song.ID FROM playlist_song ps JOIN songsTable song ON (ps.songID = song.ID) WHERE ps.playlistID = 1";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        resultset.Add(new Song
                        {
                            ID = (int)reader["ID"],
                            Name = (string)reader["Name"]
                        });
                    }
                }
            }

            finally
            {
                connection.Close();
            }

            return resultset;
        }


        public void Insert(Playlist playlist)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO playlistTable (Name, isPublic, userID)VALUES (@Name, @isPublic, @userID)";

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
            command.CommandText = "UPDATE playlistTable SET Name = @Name, isPublic = @isPublic, userID = @userID WHERE ID = @ID";

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

        public void Delete(Playlist playlist)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM playlistTable WHERE ID=@ID";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@ID";
            parameter.Value = playlist.ID;
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
