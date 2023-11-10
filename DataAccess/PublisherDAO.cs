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
    public class PublisherDAO
    {
        public static List<Publisher> GetPublisher()
        {
            var listMembers = new List<Publisher>();
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    listMembers = context.Publishers.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listMembers;
        }
        public static Publisher FindPublisherById(int _pubId)
        {
            var pub = new Publisher();
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    pub = context.Publishers.SingleOrDefault(c => c.PubId == _pubId);
                    if (pub == null)
                    {
                        throw new Exception("Publisher does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pub;
        }
        public static void SavePublisher(Publisher pub)
        {
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    context.Publishers.Add(pub);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdatePublisher(Publisher pub)
        {
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    context.Entry(pub).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeletePublisher(Publisher pub)
        {
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    var customerToDelete = context
                        .Publishers
                        .SingleOrDefault(c => c.PubId == pub.PubId);
                    context.Publishers.Remove(customerToDelete);
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
