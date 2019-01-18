using StructureMap;

namespace DataAccess
{
    public class DateAccessRegistry : Registry
    {
        public DateAccessRegistry()
        {
            For<ICategoryContext>().Use<CategoryContext>();

            For<INoteContext>().Use<NoteContext>();

            For<IRoleContext>().Use<RoleContext>();

            For<IUserContext>().Use<UserContext>();
        }
    }
}
