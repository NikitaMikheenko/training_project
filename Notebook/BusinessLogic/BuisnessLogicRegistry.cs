using StructureMap;

namespace BuisnessLogic
{
    public class BuisnessLogicRegistry : Registry
    {
        public BuisnessLogicRegistry()
        {
            For<IAuthentificationService>().Use<AuthentificationService>();

            For<ICategoryService>().Use<CategoryService>();

            For<ISearchService>().Use<SearchService>();

            For<IUserService>().Use<UserService>();

            For<INoteService>().Use<NoteService>();
        }
    }
}
