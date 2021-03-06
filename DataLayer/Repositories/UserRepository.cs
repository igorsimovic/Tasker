﻿using DomainModel.Repositories;
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
            return result;
        }

        public void UpdateUser(UserDTO model)
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

        public UserDTO GetUserByCredentials(string username, string password)
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
            db.CreateUser(new UserModel { UserName = user.UserName,
                NewPassword = user.NewPassword,
                Bio = user.Bio,
                FullName = user.FullName,
                Initials = user.Initials,
                Email = user.Email,
                Boards = new List<MongoDB.Bson.ObjectId>()
            });

            return user;
        }

        public void ChangePassword(UserDTO model)
        {
            try
            {
                db.ChangePassword(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            try
            {
                return db.GetAllUsers();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool StartUserSession(CredentialsModel credentials)
        {
            try
            {
                db.SetUserSession(credentials);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public void LeaveBoard(string userId, string boardId)
        {
            try
            {
                db.LeaveBoard(userId, boardId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
