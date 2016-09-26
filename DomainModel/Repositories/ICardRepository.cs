using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repositories
{
    public interface ICardRepository
    {
        IEnumerable<CardDTO> GetCardsByListId(string listId);

        CardDTO CreateCard(CardDTO card);

        void UpdateCard(CardDTO card);

        bool DeleteCard(string id);
    }
}
