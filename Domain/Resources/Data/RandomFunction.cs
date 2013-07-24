using System;
using System.Data.Linq.Mapping;

namespace LinkMe.Domain.Resources.Data
{
    partial class ResourcesDataContext
    {
        [Function(Name = "NEWID", IsComposable = true)]
        public Guid Random()
        { // not used by C# code... 
            throw new NotImplementedException();
        }
    }
}
