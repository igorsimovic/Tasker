﻿using DataLayer.DataModel.MongoData;
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
                            .Select(c => new CardDTO(c.Id.ToString(), 
                                c.Name,
                                c.Order, 
                                c.Description, 
                                c.Comments.Select(com => new CommentDTO(com.Id.ToString(), com.UserId.ToString(), com.Text)).ToList(),
                                c.Labels.Select(l => db_.GetLabelById(l)).ToList()));
            return result;
        }

        public void UpdateCardName(string id, string name)
        {
            db_.UpdateField<string, CardModel>(id, "Name", name, "Cards");
        }

        public void UpdateCardDescription(string id, string description)
        {
            db_.UpdateField<string, CardModel>(id, "Description", description, "Cards");
        }

        public CommentDTO InsertComment(string cardId, string userId, string text)
        {
            return db_.InsertComment(cardId, userId, text);
        }

        public CardDTO InsertLabels(string cardId, List<string> labelIds)
        {
            var result = db_.InsertLabels(cardId, labelIds);
            return result;
        }

        public CardDTO RemoveLabel(string cardId, string labelId)
        {
            var result = db_.RemoveLabel(cardId, labelId);
            return result;
        }
    }
}
