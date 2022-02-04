using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.Services.Users
{
    /// <summary>
    /// Work with DB by ADO.Net with using tasks.
    /// </summary>
    public class UserContextADOThreading: UserContextADO
    {
        private readonly AutoResetEvent handler = new AutoResetEvent(true);

        /// <summary>
        /// Crates a new instanse of <see cref="UserContextADOThreading"/>.
        /// </summary>
        /// <param name="connectionString"></param>
        public UserContextADOThreading(string connectionString)
            : base(connectionString)
        {
        }

        /// <inheritdoc/>
        public override int AddUser(User user)
        {
            var task = Task<int>.Factory.StartNew(() =>
            {
                handler.WaitOne();

                var res = base.AddUser(user);

                handler.Set();

                return res;
            });

            task.Wait();

            return task.Result;
        }

        /// <inheritdoc/>
        public override void DeleteUser(int id)
        {
            Task.Factory.StartNew(() =>
            {
                handler.WaitOne();

                base.DeleteUser(id);

                handler.Set();
            });
        }

        /// <inheritdoc/>
        public override IEnumerable<User> GetAllUsers()
        {
            var task = Task<IEnumerable<User>>.Factory.StartNew(() =>
            {
                handler.WaitOne();

                var res = base.GetAllUsers();

                handler.Set();

                return res;
            });

            task.Wait();

            return task.Result;
        }

        /// <inheritdoc/>
        public override int GetCountOfUsers()
        {
            var task = Task<int>.Factory.StartNew(() =>
            {
                handler.WaitOne();

                var res = base.GetCountOfUsers();

                handler.Set();

                return res;
            });

            task.Wait();

            return task.Result;
        }

        /// <inheritdoc/>
        public override Role SelectRole(int id)
        {
            var task = Task<Role>.Factory.StartNew(() =>
            {
                handler.WaitOne();

                var res = base.SelectRole(id);

                handler.Set();

                return res;
            });

            task.Wait();

            return task.Result;
        }

        /// <inheritdoc/>
        public override void UpdateUser(User user)
        {
            Task.Factory.StartNew(() =>
            {
                handler.WaitOne();

                base.UpdateUser(user);

                handler.Set();
            });
        }

        /// <inheritdoc/>
        public override User SelectUser(int id)
        {
            var task = Task<User>.Factory.StartNew(() =>
            {
                handler.WaitOne();

                var res = base.SelectUser(id);

                handler.Set();

                return res;
            });

            task.Wait();

            return task.Result;
        }

        /// <inheritdoc/>
        public override User SelectUser(string login)
        {
            var task = Task<User>.Factory.StartNew(() =>
            {
                handler.WaitOne();

                var res = base.SelectUser(login);

                handler.Set();

                return res;
            });

            task.Wait();

            return task.Result;
        }
    }
}
