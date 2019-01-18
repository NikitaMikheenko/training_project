using Common;
using System.Collections.Generic;

namespace DataAccess
{
    public interface IUserContext
    {
        bool AddUser(User model);

        bool EditUser(User model);

        User GetUserByLogin(string login);

        List<UserInfoModel> GetUsersInfo();

        bool DeleteUserById(int id);

        int? GetUserIdByLogin(string login);

        int? GetUserRoleIdByLogin(string login);
    }
}
