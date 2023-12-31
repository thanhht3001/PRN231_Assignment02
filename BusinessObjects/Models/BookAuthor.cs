﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class BookAuthor
    {
        [Key]
        public int? AuthorId { get; set; }
        public int? BookId { get; set; }
        public string AuthorOrder { get; set; } = null!;
        public string RoyalityPercentage { get; set; } = null!;

        public virtual Author? Author { get; set; }
        public virtual Book? Book { get; set; }
    }
}
