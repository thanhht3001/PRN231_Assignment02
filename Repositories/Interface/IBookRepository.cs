using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IBookRepository
    {

        void SaveBook(Book p);
        Book GetBookById(int id);
        Book FindBookByTitle(string _title);
        List<Book> Search(string keyword);
        void UpdateBook(Book p);
        void DeleteBook(Book p);
        List<Book> GetBooks();
    }
}
