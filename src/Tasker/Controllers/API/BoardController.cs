using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DomainModel.Repositories;
using DomainModel.Entities;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Tasker.Controllers.API
{
    [Route("api/v1/boards", Name = "Boards")]
    public class BoardController : Controller
    {
        private readonly IBoardRepository board_repo_;

        public BoardController(IBoardRepository board_repo)
        {
            board_repo_ = board_repo;
        }

        // GET: api/boards
        [Route("userID/{userId}")]
        [Authorize(Policy = "TaskerUser")]
        public ActionResult Get(string userId)
        {
            var result = board_repo_.GetAll(userId).OrderBy(b => b.OrderNo);
            return this.Ok(result);
        }

        // GET api/boards/57cf0a9636fc06fa4628c3c5
        [HttpGet("{id}", Name = "GetById")]
        [Authorize(Policy = "TaskerUser")]
        public BoardDTO GetById(string id)
        {
            return board_repo_.GetById(id);
        }

        [HttpGet("{id}/collaborators")]
        [Authorize(Policy = "TaskerUser")]
        public IEnumerable<UserDTO> GetBoardCollaborators(string id)
        {
            try
            {
                return board_repo_.GetBoardCollaborators(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // POST api/values
        [HttpPost("")]
        [Authorize(Policy = "TaskerUser")]
        public ActionResult Post([FromBody] BoardDTO board)
        {
            try
            {
                var result = board_repo_.CreateBoard(board);

                return this.CreatedAtRoute("GetById", new { id = result.Id }, result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("{id}/invite/{user}")]
        [Authorize(Policy = "TaskerUser")]
        public ActionResult InviteToBoard(string id, string user)
        {
            try
            {
                board_repo_.InviteUser(id, user);

                return this.Ok();
            }
            catch (Exception ex)
            {

                return this.BadRequest();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize(Policy = "TaskerUser")]
        public void Put(int id, [FromBody]BoardDTO board)
        {
            try
            {
                board_repo_.update(board);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // PUT api/values/5
        [HttpPut("{id}/name")]
        [Authorize(Policy = "TaskerUser")]
        public void Put(string id, [FromBody]UpdateNameModel model)
        {
            try
            {
                board_repo_.UpdateName(id, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "TaskerUser")]
        public void Delete(int id)
        {
        }
    }
}
