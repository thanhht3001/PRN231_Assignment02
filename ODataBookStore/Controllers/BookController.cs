using BusinessObjects.Models;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories;
using Repositories.Interface;
using System;

namespace ODataBookStore.Controllers
{
    public class BookController : ODataController
    {
        private readonly IBookRepository repository = new BookRepository();
        [EnableQuery]
        public IActionResult Get() => Ok(repository.GetBooks());

        [EnableQuery]
        public ActionResult<BookRequest> Get([FromRoute] int key)
        {
            var item = repository.GetBookById(key);

            if (item == null) return NotFound();

            return Ok(item);
        }
        public ActionResult Post([FromBody] BookRequest book)
        {
            var tempCpnPrj = repository.FindBookByTitle(book.Title);

            if (tempCpnPrj != null)
            {
                return BadRequest("Project name already exists.");
            }

            Book _book = new Book
            {
                Title = book.Title,
                Type = book.Type,
                PubId = book.PubId,
                Advance = book.Advance,
                Price = book.Price,
                Royalty = book.Royalty,
                YtdSales = book.YtdSales,
                Notes = book.Notes,
                PublishedDate = book.PublishedDate
            };

            repository.SaveBook(_book);

            return Created(_book);
        }
        public IActionResult Put([FromRoute] int key, [FromBody] BookRequest _book)
        {
            var book = repository.GetBookById(key);

            if (book == null)
            {
                return NotFound();
            }

            
            book.Title = _book.Title;
            book.Type = _book.Type;
            book.PubId = _book.PubId;
            book.Advance = _book.Advance;
            book.Price = _book.Price;
            book.Royalty = _book.Royalty;
            book.YtdSales = _book.YtdSales;
            book.Notes = _book.Notes;
            book.PublishedDate = _book.PublishedDate;

            repository.UpdateBook(book);

            return Updated(book);
        }

        public ActionResult Delete([FromRoute] int key)
        {
            var _book = repository.GetBookById(key);

            if (_book == null)
            {
                return NotFound();
            }

            repository.DeleteBook(_book);
            return NoContent();
        }
    }
}
