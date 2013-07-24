using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

namespace LinkMe.Framework.Instrumentation.Connection
{
    public class NullRepositoryReader
        : IRepositoryReader
    {
        Catalogue IRepositoryReader.Read()
        {
            return new Catalogue();
        }
    }
}
