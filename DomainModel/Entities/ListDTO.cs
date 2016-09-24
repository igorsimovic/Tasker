using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class ListDTO
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string Description{ get; set; }
        public IEnumerable<CardDTO> Cards { get; set; }
        public string BoardId { get; set; }

        public ListDTO(string id, string name, int order, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Order = order;
            this.Description = description;
            this.Cards = new List<CardDTO>();
        }

    }
}
