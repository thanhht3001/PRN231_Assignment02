using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
            Users = new HashSet<User>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PubId { get; set; }
        public string PublisherName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
