//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlogwritingApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BlogTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BlogTable()
        {
            this.ImageTables = new HashSet<ImageTable>();
        }
    
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public System.DateTime TravelDate { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
    
        public virtual UserTable UserTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImageTable> ImageTables { get; set; }
    }
}
