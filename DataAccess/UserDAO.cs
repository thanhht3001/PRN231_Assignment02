using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAO
    {
        public static List<User> GetUser()
        {
            var listMembers = new List<User>();
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    listMembers = context.Users.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listMembers;
        }
        public static User FindUserById(int memberId)
        {
            var member = new User();
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    member = context.Users.SingleOrDefault(c => c.UserId == memberId);
                    if (member == null)
                    {
                        throw new Exception("User does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }
        public static void SaveUser(User member)
        {
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    context.Users.Add(member);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateUser(User member)
        {
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    context.Entry(member).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteUser(User member)
        {
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    var customerToDelete = context
                        .Users
                        .SingleOrDefault(c => c.UserId == member.UserId);
                    context.Users.Remove(customerToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
