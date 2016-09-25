using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repositories
{
    public interface IBoardRepository
    {
        IEnumerable<BoardDTO> GetAll(string userId);

        BoardDTO GetById(string id);
        void update(BoardDTO model);
        BoardDTO CreateBoard(BoardDTO board);

        //List<BoardDTO> GetBoardsByUserID(string userID);
    }
}
