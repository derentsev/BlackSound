//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlackSoundDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class playlistTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public playlistTable()
        {
            this.songTable = new HashSet<songTable>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isPublic { get; set; }
        public Nullable<int> userID { get; set; }
    
        public virtual Entities.User userTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<songTable> songTable { get; set; }
    }
}
