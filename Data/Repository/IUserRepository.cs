using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentsApi.Data.Model;

namespace PaymentsApi.Data.Repository
{
    public interface IUserRepositoryAsync
    {
        public Task<User> GetUser(long idcard);
    }
}