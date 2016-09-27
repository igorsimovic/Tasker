using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class CommentDTO
    {
        public string Id;
        public string UserId;
        public string Text;

        public CommentDTO(string id, string userId, string text)
        {
            this.Id = id;
            this.UserId = userId;
            this.Text = text;
        }
    }
}
