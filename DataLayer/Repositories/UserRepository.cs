using DomainModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DataLayer.DataModel.MongoData;

namespace DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDBAccess db;
        public UserRepository(MongoDBAccess db_)
        {
            db = db_;
        }

        public UserDTO getUser(string userId)
        {
            var userResult = db.getUserByID(userId);
            var result = new UserDTO(userResult.Id.ToString(), userResult.FullName, userResult.UserName, userResult.Initials, userResult.Bio, userResult.Picture);
            var boards = db.getBoardsByUser(userResult.Id.ToString());
            IEnumerable<BoardDTO> boardList = boards.Select(b => new BoardDTO(b.Id.ToString(), b.BoardName, b.Starred, b.Color, b.OrderNo));
            result.Boards = boardList.ToList();
            return result;
        }

        public void updateUser(UserDTO model)
        {
            var userModel = new UserModel
            {
                Bio = model.Bio,
                FullName = model.FullName,
                Id = new MongoDB.Bson.ObjectId(model.Id),
                Initials = model.Initials,
                UserName = model.UserName,
            };

            try
            {
                db.updateUser(userModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UserDTO getUserByCredentials(string username, string password)
        {
            var userResult = db.GetUserByCredentials(username, password);

            if (userResult == null)
            {
                return null;
            }

            var result = new UserDTO(userResult.Id.ToString(), userResult.FullName, userResult.UserName, userResult.Initials, userResult.Bio, userResult.Picture);
            return result;
        }

        public UserDTO CreateUser(UserDTO user)
        {
            db.CreateUser(new UserModel { UserName = user.UserName, NewPassword = user.NewPassword, Bio = user.Bio, FullName = user.FullName });

            return user;
        }
    }
}
