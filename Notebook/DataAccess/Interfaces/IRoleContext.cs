using Common;
using System.Collections.Generic;

namespace DataAccess
{
    public interface IRoleContext
    {
        List<Role> GetRoles();
    }
}
