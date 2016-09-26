using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class UpdateListOrderModel
    {
        public string ListId { get; set; }
        public int NewIndex { get; set; }
    }
}
