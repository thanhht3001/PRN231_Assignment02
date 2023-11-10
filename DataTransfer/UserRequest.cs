using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class UserRequest
    {
        public UserRequest()
        {
        }

        public UserRequest(int userId, string emailAddress, string source, string fistName, string middleName, string lastName, DateTime hireDate, int roleId, int pubId, string password)
        {
            UserId = userId;
            EmailAddress = emailAddress;
            Source = source;
            FistName = fistName;
            MiddleName = middleName;
            LastName = lastName;
            HireDate1 = hireDate;
            RoleId = roleId;
            PubId = pubId;
            Password = password;
        }

        public int UserId { get; set; }
       
        public string EmailAddress { get; set; }
      
        public string Source { get; set; }
        
        public string FistName { get; set; }
        
        public string MiddleName { get; set; }
       
        public string LastName { get; set; }
       
        public DateTime HireDate { get; set; }
        public int RoleId { get; set; }
        public int PubId { get; set; }

        public string? Password { get; set; }
        public DateTime HireDate1 { get; }
    }
}
