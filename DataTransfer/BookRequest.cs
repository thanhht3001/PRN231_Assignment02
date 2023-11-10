using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class BookRequest
    {
        private DateTime publishedDate;

        public BookRequest()
        {
        }

        public BookRequest(int bookId, string title, string type, int pubId, string advance, decimal price, string royalty, string ytdSales, string notes, DateTime publishedDate)
        {
            BookId = bookId;
            Title = title;
            Type = type;
            PubId = pubId;
            Advance = advance;
            Price = price;
            Royalty = royalty;
            YtdSales = ytdSales;
            Notes = notes;
            PublishedDate = publishedDate;
        }

        public int BookId { get; set; }
        [Required, StringLength(40)]
        public string Title { get; set; }
        [Required, StringLength(40)]
        public string Type { get; set; }
        public int PubId { get; set; }
        [Required]
        public string Advance { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required, StringLength(40)]
        public string Royalty { get; set; }
        [Required, StringLength(40)]
        public string YtdSales { get; set; }
        [Required, StringLength(40)]
        public string Notes { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
    }
}
