using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int PubId { get; set; }
        public decimal Price { get; set; }
        public string Advance { get; set; } = null!;
        public string Royalty { get; set; } = null!;
        public string YtdSales { get; set; } = null!;
        public string Notes { get; set; } = null!;
        public DateTime PublishedDate { get; set; }

        public virtual Publisher Pub { get; set; } = null!;
    }
}
