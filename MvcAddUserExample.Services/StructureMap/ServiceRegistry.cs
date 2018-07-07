using MvcAddUserExample.Core.Interfaces;
using StructureMap;

namespace MvcAddUserExample.Services.StructureMap
{
    /// <summary>
    /// StructureMap registry for services.
    /// </summary>
    public class ServiceRegistry: Registry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRegistry"/> class.
        /// </summary>
        public ServiceRegistry()
        {
            For<IUserService>().Use<UserService>();
        }
    }
}
