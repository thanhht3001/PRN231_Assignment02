using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IUserRepository
    {
        void SaveUser(User member);
        User GetUserById(int id);
        List<User> GetUser();
        void UpdateUser(User member);
        void DeleteUser(User member);
    }
}
