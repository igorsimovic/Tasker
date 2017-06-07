using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class CheckListDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public IEnumerable<CheckItemDTO> CheckItems { get; set; }
    }
}
