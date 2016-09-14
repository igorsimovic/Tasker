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
        public IEnumerable<ListDTO> Lists { get; private set; }
        public bool Starred { get; set; }

        public string Color { get; set; }

        public BoardDTO()
        {

        }

        public BoardDTO(string id, string boardName, bool Starred, string color)
        {
            this.Id = id;
            this.BoardName = boardName;
            this.Starred = Starred;
            this.Color = color;
        }

        public BoardDTO(string id, string boardName, bool stared, IEnumerable<ListDTO> lists)
        {
            this.Id = id;
            this.BoardName = boardName;
            this.Starred = stared;
            this.Lists = lists;
        }

    }
}
