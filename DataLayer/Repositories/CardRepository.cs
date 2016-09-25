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
    public class CardRepository : ICardRepository
    {
        private readonly MongoDBAccess db_;


        public CardRepository(MongoDBAccess db)
        {
            db_ = db;
        }

        public CardDTO CreateCard(CardDTO card)
        {
            var result = db_.CreateCard(card);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception();
            }
        }

        public bool DeleteCard(string id)
        {
            db_.DeleteCard(id);
            return true;
        }

        public IEnumerable<CardDTO> GetCardsByListId(string listId)
        {
            var result = db_.GetCardsByListId(listId)
                            .Select(c => new CardDTO(c.Id.ToString(), c.Name, c.Order, c.Description));
            return result;
        }

        public CardDTO UpdateCard(CardDTO card)
        {
            throw new NotImplementedException();
        }
    }
}
