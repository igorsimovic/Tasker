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
        void updateUser(UserDTO model);
    }
}
