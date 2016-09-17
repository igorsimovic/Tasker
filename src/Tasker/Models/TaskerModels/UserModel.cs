using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasker.Models.TaskerModels
{
    public class UserModel
    {

        public string ID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Initials { get; set; }
        public string Bio { get; set; }
        public string Picture { get; set; }
        public List<BoardDTO> Boards { get; set; }
    }
}
