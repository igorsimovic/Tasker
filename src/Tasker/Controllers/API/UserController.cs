using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DomainModel.Entities;
using DomainModel.Repositories;
using System.Net.Http;

namespace Tasker.Controllers.API
{
    [Produces("application/json")]
    [Route("api/v1/user")]
    public class UserController : Controller
    {
        private readonly IUserRepository repo_;

        public UserController(IUserRepository repo)
        {
            repo_ = repo;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public UserDTO Get(string id)
        {
            return repo_.getUser(id);
        }
        
        // POST: api/User
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/User/5
        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage Put(string id, [FromBody]UserDTO model)
        {
            try
            {
                repo_.updateUser(model);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
