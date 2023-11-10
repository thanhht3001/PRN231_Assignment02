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
    public class AuthorDAO
    {
        public static List<Author> GetAuthor()
        {
            var listMembers = new List<Author>();
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    listMembers = context.Authors.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listMembers;
        }
        public static Author FindAuthorById(int _authorId)
        {
            var author = new Author();
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    author = context.Authors.SingleOrDefault(c => c.AuthorId == _authorId);
                    if (author == null)
                    {
                        throw new Exception("Author does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return author;
        }
        public static void SaveAuthor(Author member)
        {
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    context.Authors.Add(member);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateAuthor(Author member)
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
        public static void DeleteAuthor(Author member)
        {
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    var customerToDelete = context
                        .Authors
                        .SingleOrDefault(c => c.AuthorId == member.AuthorId);
                    context.Authors.Remove(customerToDelete);
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

