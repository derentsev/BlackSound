using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSoundDAL.Entities
{
    public class Song
    {
        public int ID { get; set; }
        public int ArtistID { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
    }
}
