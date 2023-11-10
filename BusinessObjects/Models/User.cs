using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Source { get; set; } = null!;
        public string FistName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int RoleId { get; set; }
        public int PubId { get; set; }
        public DateTime HireDate { get; set; }

        public virtual Publisher Pub { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
