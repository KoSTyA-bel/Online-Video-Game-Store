using GameStore.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore
{
    public class UserContextFactory
    {
        private string _connectionString;

        public UserContextFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public UserContextADO Build() => new UserContextADO(_connectionString);
    }
}
