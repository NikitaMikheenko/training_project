using Common;

namespace BuisnessLogic
{
    public interface IAuthentificationService
    {
        bool LoginValidate(LoginModel model);

        bool Registration(RegistrationModel model);
    }
}
