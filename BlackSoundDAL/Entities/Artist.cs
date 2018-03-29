using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSoundDAL.Entities
{
    class Artist : BaseEntity
    {
        public string Country { get; set; }
        public string Name { get; set; }
    }
       
}
