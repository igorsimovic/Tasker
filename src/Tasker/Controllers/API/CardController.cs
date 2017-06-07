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


        [HttpGet("{id}/checkLists")]
        [Authorize(Policy = "TaskerUser")]
        public ActionResult GetCheckLists(string id)
        {
            try
            {
                var result = card_repo_.GetCheckListsByCardID(id);
                return this.Ok(result);
            }
            catch (Exception)
            {

                throw;
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

        [HttpPost("{id}/addCheckList/{name}")]
        [Authorize(Policy = "TaskerUser")]
        public ActionResult PostCheckList(string id, string name)
        {
            try
            {
                var result = card_repo_.AddCheckList(id, name);
                return this.Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("chekList/{checkListId}")]
        [Authorize(Policy = "TaskerUser")]
        public ActionResult PostCheckListItem(string checkListId, [FromBody] CheckItemDTO model)
        {
            try
            {
                var result = card_repo_.AddCheckListItem(checkListId, model);
                return this.Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }



        [HttpPut("{id}/name")]
        [Authorize(Policy = "TaskerUser")]
        public ActionResult PutName(string id, [FromBody] CardDTO card)
        {
            try
            {
                card_repo_.UpdateCardName(id, card.Name);

                return this.Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}/description")]
        [Authorize(Policy = "TaskerUser")]
        public ActionResult PutDescription(string id, [FromBody] CardDTO card)
        {
            try
            {
                card_repo_.UpdateCardDescription(id, card.Description);

                return this.Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}/comments")]
        [Authorize(Policy = "TaskerUser")]
        public ActionResult InsertComment(string id, [FromBody] CommentDTO comment)
        {
            try
            {
                var result = card_repo_.InsertComment(id, comment.UserId, comment.Text);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}/addLabels")]
        [Authorize(Policy = "TaskerUser")]
        public ActionResult InsertLabels(string id, [FromBody] List<string> labels)
        {
            try
            {
                var result = card_repo_.InsertLabels(id, labels);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}/removeLabel")]
        [Authorize(Policy = "TaskerUser")]
        public ActionResult RemoveLabel(string id, [FromBody] LabelDTO label)
        {
            try
            {
                var result = card_repo_.RemoveLabel(id, label.Id);

                return this.Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // PUT api/lists/5
        [HttpPut("order")]
        public ActionResult Put([FromBody]IEnumerable<UpdateOrderModel> model)
        {
            try
            {
                var result = card_repo_.UpdateOrder(model);

                return this.Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("{id}/move")]
        public ActionResult Put(string id, [FromBody]MoveModel model)
        {
            try
            {
                var result = card_repo_.MoveCard(id, model);

                return this.Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("checkItem")]
        [Authorize(Policy = "TaskerUser")]

        public ActionResult CheckItem([FromBody] CheckItemDTO model)
        {
            try
            {

                return this.BadRequest();
                card_repo_.CheckItem(model);
                return this.Ok();
            }
            catch (Exception)
            {

                throw;
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
