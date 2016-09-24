using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repositories
{
    public interface IListRepository
    {
        IEnumerable<ListDTO> GetListsByBoardId(string boardId);

        ListDTO CreateList(ListDTO list);

        ListDTO UpdateList(ListDTO list);

        bool DeleteList(string id);
    }
}
