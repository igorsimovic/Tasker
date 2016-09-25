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
    [Route("api/v1/cards")]
    public class CardController : Controller
    {
        private readonly ICardRepository card_repo_;

        public CardController(ICardRepository card_repo)
        {
            card_repo_ = card_repo;
        }

        // PUT api/cards/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]CardDTO card)
        {
            try
            {
                //card_repo_.update(card);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        // POST api/cards
        [HttpPost("")]
        [Authorize(Policy = "TaskerUser")]
        public ActionResult Post([FromBody] CardDTO card)
        {
            try
            {
                var result = card_repo_.CreateCard(card);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        // DELETE api/cards/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var result = card_repo_.DeleteCard(id);

                return this.Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
