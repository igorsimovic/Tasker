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

        void UpdateName(string id, UpdateNameModel model);
        void InviteUser(string id, string user);
        IEnumerable<UserDTO> GetBoardCollaborators(string boardId);
        void StarStatus(string id, bool starred);

        //List<BoardDTO> GetBoardsByUserID(string userID);
    }
}
