using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentsApi.Data.Model;

namespace PaymentsApi.Data.Repository
{
    public class UserRepositoryAsync : IUserRepositoryAsync
    {
        private readonly DataContext _datacontext;
        public UserRepositoryAsync(DataContext datacontext)
        {
            _datacontext = datacontext;
        }

        public async Task<User> GetUser(long idcard)
        {
            return await _datacontext.User.FindAsync(idcard);
        }
    }
}