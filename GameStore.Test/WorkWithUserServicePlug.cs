using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Services.Users;

namespace GameStore.Test
{
    class WorkWithUserServicePlug
    {
        private IUserService _service;

        public WorkWithUserServicePlug(IUserService service)
        {
            _service = service;
        }

        public void Find()
        {
            _service.ContainsUser(null);
        }

        public void RegU()
        {
            _service.RegistrUser(null, null);
        }

        public void RegA()
        {
            _service.RegistrAdmin(null, null);
        }

        public void TryGetRole()
        {
            _service.TryGetRole(null);
        }
    }
}
