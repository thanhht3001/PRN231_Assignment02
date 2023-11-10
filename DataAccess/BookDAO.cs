using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookDAO
    {
        public static List<Book> GetBooks()
        {
            var listProducts = new List<Book>();
            try
            {
                using (var context = new BookStoreAPIContext()
                )
                {
                    listProducts = context.Books.ToList();
                };
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return listProducts;
        }
        public static Book FindBookById(int proId)
        {
            Book p = null;

            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    p = context.Books.SingleOrDefault(x => x.BookId == proId);
                    if (p == null)
                    {
                        throw new Exception("Book does not exist"); // Ném ngoại lệ khi không tìm thấy sản phẩm
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return p;
        }
        public static Book FindBookByTitle(string _title)
        {
            var companyProject = new Book();
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    companyProject = context.Books
                        
                        .SingleOrDefault(c => c.Title.ToLower() == _title.ToLower());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return companyProject;
        }

        public static List<Book> Search(string keyword)
        {
            var listProduct = new List<Book>();
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    // Tìm tất cả sản phẩm có ProductName chứa keyword
                    listProduct = context.Books.Where(f => f.Title.Contains(keyword)).ToList();

                    // Tìm tất cả OrderDetails có UnitPrice tương tự với sản phẩm
                    //foreach (var product in listProduct)
                    //{
                    //    product.OrderDetails = context.OrderDetails.Where(od => od.UnitPrice == product.UnitPrice).ToList();
                    //}
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listProduct;
        }

        public static void SaveBook(Book p)
        {
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    context.Books.Add(p);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static void UpdateBook(Book p)
        {
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    context.Entry<Book>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static Book DeleteBook(Book p)
        {
            try
            {
                using (var context = new BookStoreAPIContext())
                {
                    var p1 = context.Books.SingleOrDefault(c => c.BookId == p.BookId);
                    context.Books.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return p;
        }
    }
}

