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
    public class ListRepository : IListRepository
    {
        private readonly MongoDBAccess db_;


        public ListRepository(MongoDBAccess db)
        {
            db_ = db;
        }

        public IEnumerable<ListDTO> GetListsByBoardId(string boardId)
        {
            var result = db_.GetListByBoardId(boardId)
                .Select(l => new ListDTO(l.Id.ToString(), l.Name, l.Order, l.Description));

            return result;
        }
    }
}
