﻿using System;
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
    [Route("api/v1/lists")]
    public class ListController : Controller
    {
        private readonly IListRepository list_repo_;

        public ListController(IListRepository list_repo)
        {
            list_repo_ = list_repo;
        }

        // PUT api/lists/5
        [HttpPut("order")]
        public ActionResult Put([FromBody]IEnumerable<UpdateOrderModel> model)
        {
            try
            {
                var result = list_repo_.UpdateOrder(model);

                return this.Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}/name")]
        public ActionResult Put(string id, [FromBody]UpdateNameModel model)
        {
            try
            {
                list_repo_.UpdateName(id, model);

                return this.Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/lists
        [HttpPost("")]
        public ActionResult Post([FromBody] ListDTO list)
        {
            try
            {
                var result = list_repo_.CreateList(list);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var result = list_repo_.DeleteList(id);

                return this.Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
