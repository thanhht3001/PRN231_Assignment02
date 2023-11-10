using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class AuthorRequest
    {
        public AuthorRequest()
        {
        }

        public AuthorRequest(int authorId, string emailAddress, string address, string lastName, string fistName, string phone, string city, string zip)
        {
            AuthorId = authorId;
            EmailAddress = emailAddress;
            Address = address;
            LastName = lastName;
            FistName = fistName;
            Phone = phone;
            City = city;
            Zip = zip;
        }

        public int AuthorId { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string LastName { get; set; }
        public string FistName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }

    }
}
