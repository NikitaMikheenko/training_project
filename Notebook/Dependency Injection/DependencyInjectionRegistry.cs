using BuisnessLogic;
using DataAccess;
using StructureMap;

namespace Dependency_Injection
{
    public class DependensyInjectionRegistry : Registry
    {
        public DependensyInjectionRegistry()
        {
            Scan(s =>
            {
                s.AssemblyContainingType<BuisnessLogicRegistry>();
                s.AssemblyContainingType<DateAccessRegistry>();
                s.LookForRegistries();
            });
        }
    }
}
