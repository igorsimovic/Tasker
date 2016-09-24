using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DomainModel.Repositories;
using DomainModel.Entities;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Tasker.Controllers.API
{
    [Route("api/v1/boards", Name ="Boards")]
    public class BoardController : Controller
    {
        private readonly IBoardRepository board_repo_;

        public BoardController(IBoardRepository board_repo)
        {
            board_repo_ = board_repo;
        }

        // GET: api/boards
        [HttpGet("")]
        public IEnumerable<BoardDTO> Get()
        {
            return board_repo_.GetAll().OrderBy(b=>b.OrderNo);
        }

        // GET api/boards/57cf0a9636fc06fa4628c3c5
        [HttpGet("{id}", Name = "GetById")]
        public BoardDTO Get(string id)
        {
            return board_repo_.GetById(id);
        }
        [HttpGet]
        [Route("userID/{id}")]
        public List<BoardDTO> GetByUserID(string id)
        {
            return board_repo_.GetBoardsByUserID(id);
        }

        // POST api/values
        [HttpPost("")]

        public BoardDTO Post([FromBody] BoardDTO board)
        {
            try
            {
                var result = board_repo_.CreateBoard(board);
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
