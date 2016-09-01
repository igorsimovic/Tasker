using DataLayer.DataModel.MongoData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModel.POCOData
{
    public class BoardPOCO : BoardModel
    {
        public string ID { get; set; }

        
        public BoardPOCO()
        {
            this.ID = base.Id.ToString();
            
        }
    }
}
