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
    public class BookRepository : IBookRepository
    {
        public void DeleteBook(Book p)
       => BookDAO.DeleteBook(p);

        public Book FindBookByTitle(string _title)
       => BookDAO.FindBookByTitle(_title);

        public Book GetBookById(int id)
       => BookDAO.FindBookById(id);

        public List<Book> GetBooks()
      => BookDAO.GetBooks();

        public void SaveBook(Book p)
      => BookDAO.SaveBook(p);

        public List<Book> Search(string keyword)
      => BookDAO.Search(keyword);

        public void UpdateBook(Book p)
      => BookDAO.UpdateBook(p);
    }
}
