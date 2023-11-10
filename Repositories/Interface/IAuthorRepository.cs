using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IAuthorRepository
    {
        void SaveAuthor(Author _author);
        Author GetAuthorById(int id);
        List<Author> GetAuthors();
        void UpdateAuthor(Author _author);
        void DeleteAuthor(Author _author);
    }
}
