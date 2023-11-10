using BusinessObjects.Models;
using DataAccess;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public void DeleteUser(User member)
        => UserDAO.DeleteUser(member);

        public List<User> GetUser()
      => UserDAO.GetUser();

        public User GetUserById(int id)
      => UserDAO.FindUserById(id);

        public void SaveUser(User member)
       => UserDAO.SaveUser(member);

        public void UpdateUser(User member)
       => UserDAO.UpdateUser(member);
    }
}
