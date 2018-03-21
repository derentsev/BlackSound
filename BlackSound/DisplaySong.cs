using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSoundDAL.Entities;

namespace BlackSound
{
    public class DisplaySong
    {
        public Song AddSong()
        {
            Song songInfo = new Song();

            Console.WriteLine("Adding new song...");
            Console.Write("Name: ");
            songInfo.Name = Console.ReadLine();
            Console.Write("Year: ");
            songInfo.Year = Convert.ToInt32(Console.ReadLine());
            Console.Write("Artist: ");
            songInfo.ArtistName = Console.ReadLine();
            
            return songInfo;
        }

        public Song UpdateSong()
        {
            Song songInfo = new Song();
            Console.WriteLine("Updating new song...");
            Console.WriteLine("Insert song`s ID: ");
            songInfo.ID = Convert.ToInt32(Console.ReadLine());
            Console.Write("New ame: ");
            songInfo.Name = Console.ReadLine();
            Console.Write("New year: ");
            songInfo.Year = Convert.ToInt32(Console.ReadLine());
            Console.Write("New artist: ");
            songInfo.ArtistName = Console.ReadLine();
            return songInfo;
        }
    }
}
