using DataLayer.DataModel.MongoData;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using System.Reflection;
using System.Collections;

namespace DataLayer
{
    public class MongoDBAccess
    {
        private IMongoClient mongoClient_;
        private static MongoDBAccess instance_;
        private IMongoDatabase db_;

        public MongoDBAccess()
        {
            this.mongoClient_ = new MongoClient("mongodb://localhost:27017");
            this.db_ = mongoClient_.GetDatabase("TaskerDB");
        }


        public UserModel getUserByID(string userId)
        {
            var filter = Builders<UserModel>.Filter.Eq("_id", new ObjectId(userId));
            var userResult = db_.GetCollection<UserModel>("User").Find<UserModel>(filter).FirstOrDefault();
            if (userResult == null)
            {
                throw new Exception("User not found");
            }
            return userResult;
            //var result = new UserDTO(userResult.Id.ToString(), userResult.FullName, userResult.UserName, userResult.Initials, userResult.Bio, userResult.Picture);
            //var boardFilter = Builders<BoardModel>.Filter.In("_id", userResult.Boards);
            //var boardList = db_.GetCollection<BoardModel>("Boards").Find(boardFilter).ToEnumerable<BoardModel>();
            //List<BoardDTO> boardsDTO = boardList.Select(b => new BoardDTO(b.Id.ToString(),b.BoardName,b.Starred,b.Color,b.OrderNo)).ToList();

            //result.Boards = boardsDTO;
            //return result;
        }

        internal void SetUserSession(CredentialsModel credentials)
        {
            try
            {   
                //FOr future use for now we will not store UserSession in db
               // db_.GetCollection<CredentialsModel>("UserSession").InsertOne(credentials);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal IEnumerable<UserDTO> GetBoardCollaborators(string boardId)
        {
            var result = new List<UserDTO>();
            var userCollection = db_.GetCollection<UserModel>("User").Find(_ => true).ToList();
            foreach (var user in userCollection)
            {
                if (user.Boards.Contains(new ObjectId(boardId)))
                {
                    result.Add(new UserDTO
                    {
                        Id = user.Id.ToString(),
                        Initials = user.Initials,
                        FullName = user.FullName,
                        Email = user.Email,
                    });
                }

            }
            return result;
        }

        internal void InsertCardIntoList(string cardId, string listId)
        {
            var listObjectId = new ObjectId(listId);
            var cardObjectId = new ObjectId(cardId);

            var filter = Builders<ListModel>.Filter.Eq(l => l.Id, listObjectId);
            var update = Builders<ListModel>.Update.Push(l => l.Cards, cardObjectId);

            db_.GetCollection<ListModel>("Lists").UpdateOne(filter, update);
        }

        internal void RemoveCardFromList(string cardId, string listId)
        {
            var listObjectId = new ObjectId(listId);
            var cardObjectId = new ObjectId(cardId);

            var filter = Builders<ListModel>.Filter.Eq(l => l.Id, listObjectId);
            var update = Builders<ListModel>.Update.Pull(l => l.Cards, cardObjectId);

            db_.GetCollection<ListModel>("Lists").UpdateOne(filter, update);
        }

        internal void ChangePassword(UserDTO model)
        {
            var filter = Builders<UserModel>.Filter.Eq("_id", new ObjectId(model.Id));
            var usercollection = db_.GetCollection<UserModel>("User");
            var user = usercollection.Find(filter).FirstOrDefault();
            if (user != null)
            {
                if (user.NewPassword != model.OldPassword)
                {
                    throw new Exception("Old password is incorect");
                }
                else
                {
                    var update = Builders<UserModel>.Update.Set(u => u.NewPassword, model.NewPassword);
                    usercollection.UpdateOne(filter, update);
                }
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        internal LabelDTO GetLabelById(ObjectId l)
        {
            var filter = Builders<LabelModel>.Filter.Eq("_id", l);
            var labelModel = db_.GetCollection<LabelModel>("Labels").Find(filter).FirstOrDefault();
            return new LabelDTO(labelModel.Id.ToString(), labelModel.Title, labelModel.Color);
        }

        internal List<LabelDTO> GetLabels()
        {
            return db_.GetCollection<LabelModel>("Labels")
                .AsQueryable()
                .AsEnumerable()
                .Select(l => new LabelDTO(l.Id.ToString(), l.Title, l.Color))
                .ToList();
        }

        internal void InviteUserToBoard(string id, string user)
        {
            try
            {
                var userCollection = db_.GetCollection<UserModel>("User");
                var filter = Builders<UserModel>.Filter.Eq("_id", new ObjectId(user));
                var update = Builders<UserModel>.Update.Push(u => u.Boards, new ObjectId(id));
                userCollection.UpdateOne(filter, update);


            }
            catch (Exception)
            {

                throw new Exception("Invite to board failed");
            }
        }

        internal IEnumerable<UserDTO> GetAllUsers()
        {
            var result = db_.GetCollection<UserModel>("User").Find(_ => true).ToList().Select(u => new UserDTO
            {
                FullName = u.FullName,
                Email = u.Email,
                Id = u.Id.ToString()
            });
            return result.AsEnumerable();
        }

        //private BsonDocument createUpdateQuery(BsonDocument doc)
        //{
        //    var elementsEnum = doc.GetEnumerator();
        //    IDictionary<string, string> bsonDic = new Dictionary<string, string>();
        //    while (elementsEnum.Current != null)
        //    {
        //        if (elementsEnum.Current.Value != null)
        //        {
        //            var current = elementsEnum.Current;
        //            bsonDic.Add(current.Name, current.Value.ToString());
        //        }
        //        elementsEnum.Current = elementsEnum.MoveNext().;
        //    }

        //    return bsonDic.ToBsonDocument();
        //}


        //TODO: Make this globaly accesible(he)
        private IDictionary<string, string> makeDictionaryFromModel(Object v)
        {

            PropertyInfo[] props = v.GetType().GetProperties();
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name != "Id")
                {
                    var objectValue = prop.GetValue(v, null);
                    if (objectValue != null)
                    {
                        var first = prop.Name.ElementAt(0).ToString();
                        first = first.ToLower();
                        var propName = first + prop.Name.Substring(1, prop.Name.Count() - 1);
                        result.Add(propName, objectValue != null ? objectValue.ToString() : string.Empty);
                    }
                }
            }
            return result;
        }

        public void updateUser(UserModel userModel)
        {
            var filter = Builders<UserModel>.Filter.Eq("_id", userModel.Id);
            try
            {
                var dictionary = makeDictionaryFromModel(userModel);
                var bsonDocument = new BsonDocument("$set", dictionary.ToBsonDocument());
                var update = new BsonDocumentUpdateDefinition<UserModel>(bsonDocument);
                db_.GetCollection<UserModel>("User").UpdateOne(filter, update);

            }
            catch (Exception ex)
            {

                throw new Exception("User update fail" + ex.Message);
            }
        }

        #region BOARDS

        public List<BoardModel> getBoardsByUser(string userID)
        {
            var filter = Builders<UserModel>.Filter.Eq("_id", new ObjectId(userID));
            var userResult = db_.GetCollection<UserModel>("User").Find<UserModel>(filter).FirstOrDefault();
            try
            {
                var boardFilter = Builders<BoardModel>.Filter.In("_id", userResult.Boards);
                var boardList = db_.GetCollection<BoardModel>("Boards").Find(boardFilter).ToEnumerable<BoardModel>().ToList<BoardModel>();
                return boardList;
            }
            catch (Exception ex)
            {

                return new List<BoardModel>();
            }
            //List<BoardDTO> boardsDTO = boardList.Select(b => new BoardDTO(b.Id.ToString(), b.BoardName, b.Starred, b.Color, b.OrderNo)).ToList();
            //return boardsDTO;
        }

        public BoardDTO CreateBoard(BoardDTO board)
        {
            try
            {
                var boardModel = new BoardModel
                {
                    Color = board.Color,
                    BoardName = board.BoardName,
                    OrderNo = board.OrderNo,
                    Lists = new List<ObjectId>(),
                    UserCreatedBy = new ObjectId(board.UserCreatedBy)
                };

                db_.GetCollection<BoardModel>("Boards").InsertOne(boardModel);


                var filter = Builders<UserModel>.Filter.Eq(u => u.Id, boardModel.UserCreatedBy);
                var update = Builders<UserModel>.Update.Push(u => u.Boards, boardModel.Id);

                db_.GetCollection<UserModel>("User").UpdateOne(filter, update);
                board.Id = boardModel.Id.ToString();

                return board;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public IEnumerable<BoardModel> GetAllBoards(string userId)
        {
            var objectUserId = new ObjectId(userId);
            var result = db_.GetCollection<BoardModel>("Boards").Find<BoardModel>(b => b.UserCreatedBy == objectUserId).ToEnumerable<BoardModel>();

            return result;
        }

        public BoardModel GetBoardById(string id)
        {
            var filter = Builders<BoardModel>.Filter.Eq("_id", new ObjectId(id));
            var result = db_.GetCollection<BoardModel>("Boards").Find(filter).FirstOrDefault();

            return result;
        }

        public void updateBoard(BoardDTO model)
        {
            var filter = Builders<BoardModel>.Filter.Eq("_id", new ObjectId(model.Id));
            BoardModel dbModel = new BoardModel
            {
                BoardName = model.BoardName,
                Color = model.Color,
                Starred = model.Starred,
                Id = new ObjectId(model.Id),
                OrderNo = model.OrderNo,
            };
            try
            {
                var result = db_.GetCollection<BoardModel>("Boards").ReplaceOne(filter, dbModel, new UpdateOptions { IsUpsert = true });
                var initialIndex = model.OrderNo;
                var collection = db_.GetCollection<BoardModel>("Boards");

                //updating indexes in the targetList
                foreach (var board in collection.Find(b => b.Starred == dbModel.Starred && b.OrderNo >= dbModel.OrderNo && b.Id != dbModel.Id).ToListAsync().Result.OrderBy(b => b.OrderNo))
                {
                    var currentFilter = Builders<BoardModel>.Filter.Eq("_id", board.Id);
                    var update = new BsonDocument("$set", new BsonDocument("OrderNo", ++initialIndex));
                    var a = collection.UpdateOne(currentFilter, update);

                }
                //updating indexes in the start list
                initialIndex = model.OriginalIndex;
                foreach (var board in collection.Find(b => b.Starred != dbModel.Starred && b.OrderNo > initialIndex && b.Id != dbModel.Id).ToListAsync().Result.OrderByDescending(b => b.OrderNo))
                {
                    var currentValue = board.OrderNo;
                    var currentFilter = Builders<BoardModel>.Filter.Eq("_id", board.Id);
                    var update = new BsonDocument("$set", new BsonDocument("OrderNo", --currentValue));
                    var a = collection.UpdateOne(currentFilter, update);
                }

                //var prevValue = initialIndex;
                //var resultUpdate = db_.GetCollection<BoardModel>("Boards")
                //    .UpdateMany(Builders<BoardModel>.Filter.Gte("OrderNo", initialIndex)
                //    ,Builders<BoardModel>.Update.Set("OrderNo", prevValue++));


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        internal void UpdateBoardField<T>(string id, string fieldName, T fieldValue)
        {

            var filter = Builders<BoardModel>.Filter.Eq("_id", new ObjectId(id));
            var collection = db_.GetCollection<BoardModel>("Boards");

            var update = Builders<BoardModel>.Update
                .Set(fieldName, fieldValue);

            var result = collection.UpdateOne(filter, update);
        }


        #endregion

        #region LISTS
        public ListModel GetListById(string id)
        {
            var filter = Builders<ListModel>.Filter.Eq("_id", new ObjectId(id));
            var result = db_.GetCollection<ListModel>("Lists").Find(filter).FirstOrDefault();

            return result;
        }

        public IEnumerable<ListModel> GetListByBoardId(string boardId)
        {
            var board = this.GetBoardById(boardId);
            if (board.Lists != null)
            {
                var filter = Builders<ListModel>.Filter.In("_id", board.Lists);
                var result = db_.GetCollection<ListModel>("Lists").Find(filter).ToEnumerable<ListModel>();
                return result;
            }
            return null;
        }

        public ListDTO CreateList(ListDTO list)
        {
            try
            {
                var listModel = new ListModel
                {
                    Name = list.Name,
                    Order = list.Order,
                    Description = list.Description,
                    Cards = new List<ObjectId>()
                };

                db_.GetCollection<ListModel>("Lists").InsertOne(listModel);
                ObjectId boardObjectId = new ObjectId(list.BoardId);

                var filter = Builders<BoardModel>.Filter.Eq(b => b.Id, boardObjectId);
                var update = Builders<BoardModel>.Update.Push(b => b.Lists, listModel.Id);

                db_.GetCollection<BoardModel>("Boards").UpdateOne(filter, update);

                list.Id = listModel.Id.ToString();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void UpdateListField<T>(string id, string fieldName, T fieldValue)
        {

            var filter = Builders<ListModel>.Filter.Eq("_id", new ObjectId(id));
            var collection = db_.GetCollection<ListModel>("Lists");

            var update = Builders<ListModel>.Update
                .Set(fieldName, fieldValue);

            var result = collection.UpdateOne(filter, update);
        }

        internal void DeleteList(string id)
        {
            db_.GetCollection<ListModel>("Lists").DeleteOne<ListModel>(l => l.Id == ObjectId.Parse(id));

        }
        #endregion

        #region CARDS
        public IEnumerable<CardModel> GetCardsByListId(string id)
        {
            var list = this.GetListById(id);

            if (list.Cards != null)
            {
                var filter = Builders<CardModel>.Filter.In("_id", list.Cards);
                var result = db_.GetCollection<CardModel>("Cards").Find(filter).ToEnumerable<CardModel>();
                return result;
            }
            return null;
        }

        public CardDTO CreateCard(CardDTO card)
        {
            try
            {
                var cardModel = new CardModel
                {
                    Name = card.Name,
                    Order = card.Order,
                    Description = card.Description,
                    Labels = new List<ObjectId>(),
                    Comments = new List<CommentModel>()
                };

                db_.GetCollection<CardModel>("Cards").InsertOne(cardModel);
                ObjectId listObjectId = new ObjectId(card.ListId);

                var filter = Builders<ListModel>.Filter.Eq(l => l.Id, listObjectId);
                var update = Builders<ListModel>.Update.Push(l => l.Cards, cardModel.Id);

                db_.GetCollection<ListModel>("Lists").UpdateOne(filter, update);

                card.Id = cardModel.Id.ToString();
                return card;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal void DeleteCard(string id)
        {
            db_.GetCollection<CardModel>("Cards").DeleteOne<CardModel>(c => c.Id == ObjectId.Parse(id));

        }

        internal CommentDTO InsertComment(string cardId, string userId, string text)
        {
            var commentModel = new CommentModel
            {
                Id = ObjectId.GenerateNewId(),
                UserId = new ObjectId(userId),
                Text = text
            };
            var cardIdObject = new ObjectId(cardId);

            var filter = Builders<CardModel>.Filter.Eq(c => c.Id, cardIdObject);
            var update = Builders<CardModel>.Update.Push(l => l.Comments, commentModel);

            db_.GetCollection<CardModel>("Cards").UpdateOne(filter, update);

            return new CommentDTO(commentModel.Id.ToString(), commentModel.UserId.ToString(), commentModel.Text);
        }
        #endregion

        internal void UpdateField<T1, T2>(string id, string fieldName, T1 fieldValue, string collectionName)
        {

            var filter = Builders<T2>.Filter.Eq("_id", new ObjectId(id));
            var collection = db_.GetCollection<T2>(collectionName);

            var update = Builders<T2>.Update
                .Set(fieldName, fieldValue);

            var result = collection.UpdateOne(filter, update);
        }


        #region LABELS
        public IEnumerable<LabelModel> GetLabelsByCardId(string id)
        {
            var filter = Builders<CardModel>.Filter.Eq("_id", new ObjectId(id));
            var card = db_.GetCollection<CardModel>("Cards").Find(filter).FirstOrDefault();

            if (card.Labels != null)
            {
                var labelFilter = Builders<LabelModel>.Filter.In("_id", card.Labels);
                var result = db_.GetCollection<LabelModel>("Labels").Find(labelFilter).ToEnumerable<LabelModel>();
                return result;
            }
            return null;
        }

        public LabelDTO CreateLabel(LabelDTO label)
        {
            try
            {
                var labelModel = new LabelModel
                {
                    Title = label.Title,
                    Color = label.Color
                };

                db_.GetCollection<LabelModel>("Labels").InsertOne(labelModel);
                ObjectId cardObjectId = new ObjectId(label.CardId);

                var filter = Builders<CardModel>.Filter.Eq(l => l.Id, cardObjectId);
                var update = Builders<CardModel>.Update.Push(c => c.Labels, labelModel.Id);

                db_.GetCollection<CardModel>("Cards").UpdateOne(filter, update);

                label.Id = labelModel.Id.ToString();
                return label;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal void DeleteLabel(string id)
        {
            db_.GetCollection<LabelModel>("Labels").DeleteOne<LabelModel>(c => c.Id == ObjectId.Parse(id));

        }

        public CardDTO InsertLabels(string cardId, List<string> labelIds)
        {
            var cardObjectId = new ObjectId(cardId);
            var labelObjectIds = labelIds.Select(labelId => new ObjectId(labelId));

            var filter = Builders<CardModel>.Filter.Eq(l => l.Id, cardObjectId);
            var update = Builders<CardModel>.Update.PushEach(c => c.Labels, labelObjectIds);

            db_.GetCollection<CardModel>("Cards").UpdateOne(filter, update);

            filter = Builders<CardModel>.Filter.Eq("_id", cardObjectId);
            CardModel result = db_.GetCollection<CardModel>("Cards").Find(filter).FirstOrDefault();
            return new CardDTO(result.Id.ToString(),
                        result.Name,
                        result.Order,
                        result.Description,
                        "",
                        result.Comments.Select(com => new CommentDTO(com.Id.ToString(), com.UserId.ToString(), com.Text)).ToList(),
                        result.Labels.Select(l => this.GetLabelById(l)).ToList());
        }

        public CardDTO RemoveLabel(string cardId, string labelId)
        {
            var cardObjectId = new ObjectId(cardId);
            var labelObjectId = new ObjectId(labelId);

            var filter = Builders<CardModel>.Filter.Eq(l => l.Id, cardObjectId);
            var update = Builders<CardModel>.Update.Pull(c => c.Labels, labelObjectId);

            db_.GetCollection<CardModel>("Cards").UpdateOne(filter, update);

            filter = Builders<CardModel>.Filter.Eq("_id", cardObjectId);
            CardModel result = db_.GetCollection<CardModel>("Cards").Find(filter).FirstOrDefault();
            return new CardDTO(result.Id.ToString(),
                        result.Name,
                        result.Order,
                        result.Description,
                        "",
                        result.Comments.Select(com => new CommentDTO(com.Id.ToString(), com.UserId.ToString(), com.Text)).ToList(),
                        result.Labels.Select(l => this.GetLabelById(l)).ToList());
        }

        #endregion
        public UserModel GetUserByCredentials(string username, string password)
        {
            var filter = Builders<UserModel>.Filter.Eq("userName", username);
            var userResult = db_.GetCollection<UserModel>("User").Find<UserModel>(filter).FirstOrDefault();

            if (userResult == null)
            {
                return null;
            }


            if (userResult.NewPassword != password)
            {
                return null;
            }

            return userResult;
        }

        public UserModel CreateUser(UserModel user)
        {
            try
            {
                db_.GetCollection<UserModel>("User").InsertOne(user);

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
