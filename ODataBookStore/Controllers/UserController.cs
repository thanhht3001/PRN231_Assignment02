using BusinessObjects.Models;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories;
using Repositories.Interface;

namespace ODataBookStore.Controllers
{
    public class UserController : ODataController
    {
        private readonly IUserRepository repository = new UserRepository();
        [EnableQuery]
        public IActionResult Get() => Ok(repository.GetUser());
        [EnableQuery]
        public ActionResult<User> Get([FromRoute] int key)
        {
            var item = repository.GetUserById(key);

            if (item == null) return NotFound();

            return Ok(item);
        }
        public ActionResult Post([FromBody] UserRequest userReq)
        {
            User employee = new User
            {
                EmailAddress = userReq.EmailAddress,
                Source = userReq.Source,
                FistName = userReq.FistName,
                MiddleName = userReq.MiddleName,
                LastName = userReq.LastName,
                Password = userReq.Password,
                RoleId = userReq.RoleId,
                PubId = userReq.PubId,
                HireDate = userReq.HireDate 
            };

            repository.SaveUser(employee);

            return Created(employee);
        }
        public IActionResult Put([FromRoute] int key, [FromBody] UserRequest userReq)
        {
            var employee = repository.GetUserById(key);

            if (employee == null)
            {
                return NotFound();
            }

            employee.EmailAddress = userReq.EmailAddress;
            employee.Source = userReq.Source;
            employee.FistName = userReq.FistName;
            employee.MiddleName = userReq.MiddleName;
            employee.LastName = userReq.LastName;
            employee.RoleId = userReq.RoleId;
            employee.PubId = userReq.PubId;
            employee.HireDate = userReq.HireDate;

            if (userReq.Password != null)
                employee.Password = userReq.Password;

            repository.UpdateUser(employee);

            return Updated(employee);
        }

        public ActionResult Delete([FromRoute] int key)
        {
            var employee = repository.GetUserById(key);

            if (employee == null)
            {
                return NotFound();
            }
            repository.DeleteUser(employee);
            return NoContent();
        }
    }
   
}
