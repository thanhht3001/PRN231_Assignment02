using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.Interface;
using Repositories;
using BusinessObjects.Models;

namespace ODataBookStore.Controllers
{
    public class PublisherController : ODataController
    {
        private readonly IPublisherRepository repository = new PublisherRepository();
        [EnableQuery]
        public IActionResult Get() => Ok(repository.GetPublishers());

        [EnableQuery]
        public ActionResult<PublisherRequest> Get([FromRoute] int key)
        {
            var item = repository.GetPublisherById(key);

            if (item == null) return NotFound();

            return Ok(item);
        }
        public ActionResult Post([FromBody] PublisherRequest _pub)
        {


            Publisher pub = new Publisher
            {
                PublisherName = _pub.PublisherName,
                City = _pub.City,
                State = _pub.State,
                Country = _pub.Country,
              
            };

            repository.SavePublisher(pub);

            return Created(pub);
        }
        public IActionResult Put([FromRoute] int key, [FromBody] PublisherRequest _pub)
        {
            var pub = repository.GetPublisherById(key);

            if (pub == null)
            {
                return NotFound();
            }
            pub.PublisherName = _pub.PublisherName;
            pub.City = _pub.City;
            pub.State = _pub.State;
            pub.Country = _pub.Country;
            repository.UpdatePublisher(pub);

            return Updated(pub);
        }

        public ActionResult Delete([FromRoute] int key)
        {
            var _pub = repository.GetPublisherById(key);

            if (_pub == null)
            {
                return NotFound();
            }

            repository.DeletePublisher(_pub);
            return NoContent();
        }
    }
}
