using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class CardDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public string ListId { get; set; }
        public IEnumerable<LabelDTO> Labels { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
        public IEnumerable<CheckListDTO> CheckLists { get; set; }
        public string DueDate { get; set; }

        public CardDTO(string id, string name, int order, string description, string listId, List<CommentDTO> comments, List<LabelDTO> labels, string dueDate)
        {
            this.Id = id;
            this.Name = name;
            this.Order = order;
            this.Description = description;
            this.Comments = comments;
            this.Labels = labels;
            this.ListId = listId;
            this.DueDate = dueDate;
        }
    }
}
