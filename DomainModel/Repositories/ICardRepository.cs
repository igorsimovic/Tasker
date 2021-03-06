﻿using DomainModel.Entities;
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

        void UpdateCardName(string id, string name);
        void UpdateCardDescription(string id, string description);

        bool DeleteCard(string id);

        CommentDTO InsertComment(string cardId, string userId, string text);
        CardDTO InsertLabels(string cardId, List<string> labelIds);
        CardDTO RemoveLabel(string cardId, string labelId);

        bool UpdateOrder(IEnumerable<UpdateOrderModel> model);

        bool MoveCard(string id, MoveModel model);
        CheckListDTO AddCheckList(string id, string name);
        IEnumerable<CheckListDTO> GetCheckListsByCardID(string id);
        CheckItemDTO AddCheckListItem(string id,CheckItemDTO model);
        void CheckItem(CheckItemDTO model);
        void SetDueDate(string id, string date);
    }
}
