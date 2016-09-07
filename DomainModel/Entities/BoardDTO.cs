using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class BoardDTO
    {

        public string Id { get; set; }
        public string BoardName { get; set; }
        public string BoardStuff { get; set; }

        public BoardDTO(string id, string boardName, string boardStuff)
        {
            this.Id = id;
            this.BoardName = boardName;
            this.BoardStuff = boardStuff;
        }

    }
}
