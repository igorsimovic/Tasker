using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repositories
{
    public interface IUserRepository
    {
        UserDTO getUser(string v);
        void UpdateUser(UserDTO model);
        UserDTO GetUserByCredentials(string username, string password);
        UserDTO CreateUser(UserDTO user);
        void ChangePassword(UserDTO model);

        IEnumerable<UserDTO> GetUsers();

        bool StartUserSession(CredentialsModel credentials);
    }
}
