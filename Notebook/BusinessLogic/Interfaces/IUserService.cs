using Common;
using System.Collections.Generic;

namespace BuisnessLogic
{
    public interface IUserService
    {
        bool AddUser(UserEditModel model);

        bool EditUser(UserEditModel model);

        UserEditModel GetUserByLogin(string login);

        UserInfoModel GetUserInfoByLogin(string login);

        List<UserInfoModel> GetUsersInfo();

        bool DeleteUserById(int? id);

        int? GetUserRoleIdByLogin(string login);

        List<Role> GetRoles();
    }
}
