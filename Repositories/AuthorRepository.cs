using BusinessObjects.Models;
using DataAccess;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public void DeleteAuthor(Author _author)
        => AuthorDAO.DeleteAuthor(_author);

        public Author GetAuthorById(int id)
      => AuthorDAO.FindAuthorById(id);

        public List<Author> GetAuthors()
       => AuthorDAO.GetAuthor();

        public void SaveAuthor(Author _author)
      => AuthorDAO.SaveAuthor(_author);

        public void UpdateAuthor(Author _author)
     => AuthorDAO.UpdateAuthor(_author);
    }
}
