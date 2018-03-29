using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound
{
    public class CreateDatabase
    {
        //private readonly string connectionString;

        //public CreateDatabase(string connectionString)
        //{
        //    this.connectionString = connectionString;
        //}

        public static void CreateDB()
        {
            IDbConnection connection = new SqlConnection("Server=.\\SQLEXPRESS; Database=BlackSound; Integrated Security = True");

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
            @"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name='BlackSound')
            CREATE DATABASE [BlackSound]
            GO

            USE [BlackSoundTestDB]

            IF NOT EXISTS(SELECT * FROM sys.databases WHERE name='BlackSound')
            CREATE DATABASE [BlackSound]
            GO

            USE [BlackSoundTestDB]

            CREATE TABLE [Songs]

            (
	
            [ID] int IDENTITY(1,1) PRIMARY KEY,
            [Name] NVARCHAR(50) NOT NULL,
            [ArtistName] NVARCHAR(50) NOT NULL,
            [YearCreated] int NOT NULL

            )

            CREATE TABLE [Users]

            (
	
            [ID] int IDENTITY(1,1) PRIMARY KEY,
            [Email] NVARCHAR(50) UNIQUE NOT NULL,
            [Password] NVARCHAR(50) NOT NULL,
            [Name] NVARCHAR(50) NOT NULL,
            [isAdmin] bit NOT NULL
            )

            CREATE TABLE [Playlists]

            (

            [ID] int IDENTITY(1,1) PRIMARY KEY,
            [Name] nvarchar(50) NOT NULL,
            [isPublic] bit NOT NULL,
            [userID] int NOT NULL FOREIGN KEY REFERENCES [Users] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
            )

            CREATE TABLE [playlist_song]

            (
            [songID] int FOREIGN KEY REFERENCES [Songs]([ID])  ON DELETE CASCADE ON UPDATE CASCADE,
            [playlistID] int FOREIGN KEY REFERENCES [Playlists] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
            CONSTRAINT PK_playlist_song PRIMARY KEY (songID,playlistID)
            )

            CREATE TABLE [user_playlist]

            (
            [userID] int FOREIGN KEY REFERENCES [Users]([ID]) ON DELETE NO ACTION  ON UPDATE NO ACTION,
            [playlistID] int FOREIGN KEY REFERENCES [Playlists] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
            CONSTRAINT PK_user_playlist PRIMARY KEY (userID,playlistID)
            )

            GO

            INSERT INTO Users (Email, Password, Name, IsAdmin)
            VALUES ('admin@abv.bg', 'admin', 'admin', 1)

            INSERT INTO Playlists(Name, IsPublic, UserId)
            VALUES ('All Songs', 1, 1)";

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

