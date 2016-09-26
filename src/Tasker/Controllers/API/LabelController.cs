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
    [Route("api/v1/labels")]
    public class LabelController : Controller
    {
        private readonly ILabelRepository label_repo_;

        public LabelController(ILabelRepository label_repo)
        {
            label_repo_ = label_repo;
        }

        // PUT api/labels/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]LabelDTO label)
        {
            try
            {
                label_repo_.UpdateLabel(label);
                return this.Ok(label);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // POST api/labels
        [HttpPost("")]
        public ActionResult Post([FromBody] LabelDTO label)
        {
            try
            {
                var result = label_repo_.CreateLabel(label);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        // DELETE api/labels/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var result = label_repo_.DeleteLabel(id);

                return this.Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
