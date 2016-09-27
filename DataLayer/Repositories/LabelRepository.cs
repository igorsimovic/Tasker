using DataLayer.DataModel.MongoData;
using DomainModel.Entities;
using DomainModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        private readonly MongoDBAccess db_;


        public LabelRepository(MongoDBAccess db)
        {
            db_ = db;
        }

        public LabelDTO CreateLabel(LabelDTO label)
        {
            var result = db_.CreateLabel(label);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception();
            }
        }

        public bool DeleteLabel(string id)
        {
            db_.DeleteLabel(id);
            return true;
        }

        public IEnumerable<LabelDTO> GetLabelsByCardId(string cardId)
        {
            var result = db_.GetLabelsByCardId(cardId)
                            .Select(l => new LabelDTO(l.Id.ToString(), l.Title, l.Color));
            return result;
        }

        //public void UpdateLabel(LabelDTO label)
        //{
        //    db_.UpdateLabel(label);
        //}
    }
}
