using BusinessObjects.Models;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories;
using Repositories.Interface;

namespace ODataBookStore.Controllers
{
    public class AuthorController : ODataController
    {
        private readonly IAuthorRepository repository = new AuthorRepository();
        [EnableQuery]
        public IActionResult Get() => Ok(repository.GetAuthors());
        
        [EnableQuery]
        public ActionResult<AuthorRequest> Get([FromRoute] int key)
        {
            var item = repository.GetAuthorById(key);

            if (item == null) return NotFound();

            return Ok(item);
        }

        [EnableQuery]
        public ActionResult<AuthorRequest> Gets([FromRoute] String search)
        {
            var item = repository.GetAuthors();

            return Ok(item[0]);
        }


        public ActionResult Post([FromBody] AuthorRequest _auth)
        {
            Author auth = new Author
            {
                EmailAddress = _auth.EmailAddress,
                Address = _auth.Address,
                LastName = _auth.LastName,
                FistName = _auth.FistName,
                Phone = _auth.Phone,
                City = _auth.City,
                Zip = _auth.Zip,

            };

            repository.SaveAuthor(auth);

            return Created(auth);
        }
        public IActionResult Put([FromRoute] int key, [FromBody] AuthorRequest _auth)
        {
            var auth = repository.GetAuthorById(key);

            if (auth == null)
            {
                return NotFound();
            }
            auth.EmailAddress = _auth.EmailAddress;
            auth.Address = _auth.Address;
            auth.LastName = _auth.LastName;
            auth.FistName = _auth.FistName;
            auth.Phone = _auth.Phone;
            auth.City = _auth.City;
            auth.Zip = _auth.Zip;
            repository.UpdateAuthor(auth);

            return Updated(auth);
        }

        public ActionResult Delete([FromRoute] int key)
        {
            var _auth = repository.GetAuthorById(key);

            if (_auth == null)
            {
                return NotFound();
            }

            repository.DeleteAuthor(_auth);
            return NoContent();
        }
    }
}
