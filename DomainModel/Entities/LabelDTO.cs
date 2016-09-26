using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class LabelDTO
    {
        public string Id { get; set; }
        public string CardId { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }

        public LabelDTO(string id, string title, string color)
        {
            this.Id = id;
            this.Title = title;
            this.Color = color;
        }
    }
}
