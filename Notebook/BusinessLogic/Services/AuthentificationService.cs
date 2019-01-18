using System.Web.Helpers;
using Common;
using DataAccess;

namespace BuisnessLogic
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IUserContext userContext = new UserContext();

        public bool LoginValidate(LoginModel model)
        {
            User user = userContext.GetUserByLogin(model.Login);

            if (user != null)
            {
                return Crypto.VerifyHashedPassword(user.Password, model.Password);
            }

            return false;
        }

        public bool Registration(RegistrationModel model)
        {
            if (userContext.GetUserByLogin(model.Login) == null)
            {
                return userContext.AddUser(new User()
                {
                    Login = model.Login,
                    Password = Crypto.HashPassword(model.Password),
                    Role = new Role()
                    {
                        Id = 3
                    }
                });
            }

            return false;
        }
    }
}
