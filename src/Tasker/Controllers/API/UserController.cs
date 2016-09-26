using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DomainModel.Entities;
using DomainModel.Repositories;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Tasker.Models.ManageViewModels;

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
        [Authorize(Policy = "TaskerUser")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("forSearch")]
        [Authorize(Policy = "TaskerUser")]
        public ActionResult GetUserListForSearch()
        {
            try
            {
                var result = repo_.GetUsers();
                return this.Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [Authorize(Policy = "TaskerUser")]
        public UserDTO Get(string id)
        {
            return repo_.getUser(id);
        }

        // POST: api/User
        [HttpPost]
        [Authorize(Policy = "TaskerUser")]
        public void Post([FromBody]string value)
        {
        }
      

        [HttpPut]
        [Route("{id}/changePassword")]
        [Authorize(Policy = "TaskerUser")]
        public HttpResponseMessage ChangePassword(string id, [FromBody] ChangePasswordModel model)
        {
            try
            {
                var dto = new UserDTO
                {
                    Id = model.Id,
                    NewPassword = model.NewPass,
                    OldPassword = model.OldPass
                };
                repo_.ChangePassword(dto);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        // PUT: api/User/5
        [HttpPut]
        [Route("{id}")]
        [Authorize(Policy = "TaskerUser")]
        public HttpResponseMessage Put(string id, [FromBody]UserDTO model)
        {
            try
            {
                repo_.UpdateUser(model);
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
