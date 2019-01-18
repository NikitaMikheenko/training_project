using System.Collections.Generic;
using System.Web.Helpers;
using Common;
using DataAccess;

namespace BuisnessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserContext userContext = new UserContext();

        private readonly IRoleContext roleContext = new RoleContext();

        public bool DeleteUserById(int? id)
        {
            if (id != null)
            {
                return userContext.DeleteUserById((int)id);
            }

            return false;
        }

        public bool EditUser(UserEditModel model)
        {
            int? id = userContext.GetUserIdByLogin(model.Login);

            if (id == null || id == model.Id)
            {
                return userContext.EditUser(new User()
                {
                    Id = model.Id,
                    Login = model.Login,
                    Password = Crypto.HashPassword(model.Password),
                    Role = new Role()
                    {
                        Id = model.RoleId
                    }
                });
            }

            return false;
        }

        public UserInfoModel GetUserInfoByLogin(string login)
        {
            User user = userContext.GetUserByLogin(login);

            if (user != null)
            {
                return new UserInfoModel()
                {
                    Id = user.Id,
                    Login = user.Login,
                    Role = user.Role
                };
            }

            return null;
        }

        public UserEditModel GetUserByLogin(string login)
        {
            User user = userContext.GetUserByLogin(login);

            if (user != null)
            {
                return new UserEditModel()
                {
                    Id = user.Id,
                    Login = user.Login,
                    RoleId = (int)user.Role.Id
                };
            }

            return null;
        }

        public int? GetUserRoleIdByLogin(string login)
        {
            return userContext.GetUserRoleIdByLogin(login);
        }

        public List<UserInfoModel> GetUsersInfo()
        {
            return userContext.GetUsersInfo();
        }

        public List<Role> GetRoles()
        {
            return roleContext.GetRoles();
        }

        public bool AddUser(UserEditModel model)
        {
            if (userContext.GetUserByLogin(model.Login) == null)
            {
                return userContext.AddUser(new User()
                {
                    Login = model.Login,
                    Password = Crypto.HashPassword(model.Password),
                    Role = new Role()
                    {
                        Id = model.RoleId
                    }
                });
            }

            return false;
        }
    }
}
