using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSoundDAL.Entities
{
    public class Playlist
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isPublic { get; set; }
        public int? userID { get; set; }
    }
}
