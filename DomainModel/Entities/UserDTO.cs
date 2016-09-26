using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
   public class UserDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Initials { get; set; }
        public string Bio { get; set; }
        public string Picture { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public string RepeatPassword { get; set; }
        public List<BoardDTO> Boards { get; set; }

        public string Email { get; set; }

        public UserDTO()
        {

        }

        

        public UserDTO(string id, string fn, string un, string initials, string bio, string pic)
        {
            this.Id = id;
            this.FullName = fn;
            this.UserName = un;
            this.Initials = initials;
            this.Bio = bio;
            this.Picture = pic;
        }


    }
}
