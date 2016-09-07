using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DomainModel.Repositories;
using DomainModel.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Tasker.Controllers.API
{
    [Route("api/v1/boards")]
    public class BoardController : Controller
    {

        private readonly IBoardRepository board_repo_;

        public BoardController(IBoardRepository board_repo)
        {
            board_repo_ = board_repo;
        }

        // GET: api/values
        [HttpGet("")]
        public IEnumerable<BoardDTO> Get()
        {
            return board_repo_.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public BoardDTO Get(string id)
        {
            return board_repo_.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
