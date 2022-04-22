using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Interfaces
{
    public interface IUserDb : IDbService<User>
    {
        public Task<User> GetUserByEmail(string email);
        public Task<bool> DoesEmailExist(string email);
    }
}
