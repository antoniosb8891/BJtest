using System;
using System.Threading.Tasks;

namespace BJtest.Common.Managers.UserManager
{
    public interface IUserManager
    {
        Task<bool> Login(string login, string password);
    }
}
