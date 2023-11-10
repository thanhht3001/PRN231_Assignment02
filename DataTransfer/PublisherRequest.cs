using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class PublisherRequest
    {
        public PublisherRequest()
        {
        }

        public PublisherRequest(int pubId, string publisherName, string city, string state, string country)
        {
            PubId = pubId;
            PublisherName = publisherName;
            City = city;
            State = state;
            Country = country;
        }

        public int PubId { get; set; }
        public string PublisherName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

    }
}
