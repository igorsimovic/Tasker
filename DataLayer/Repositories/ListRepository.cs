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

        public ListDTO CreateList(ListDTO list)
        {
            var result = db_.CreateList(list);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception();
            }
        }


        public bool DeleteList(string id)
        {
            db_.DeleteList(id);
            return true;
        }

        public IEnumerable<ListDTO> GetListsByBoardId(string boardId)
        {
            var result = db_.GetListByBoardId(boardId)
                .Select(l => new ListDTO(l.Id.ToString(), l.Name, l.Order, l.Description));

            return result;
        }
        public bool UpdateOrder(IEnumerable<UpdateOrderModel> model)
        {
            foreach (var item in model)
            {
                db_.UpdateListField<int>(item.Id, "Order", item.NewIndex);
            }
            return true;
        }

        public bool UpdateName(string id, UpdateNameModel model)
        {
            db_.UpdateListField<string>(id, "Name", model.Name);
            return true;
        }
        public ListDTO UpdateList(ListDTO list)
        {
            throw new NotImplementedException();
        }

       
    }
}
