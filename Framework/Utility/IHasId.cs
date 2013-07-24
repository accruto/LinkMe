using System;

namespace LinkMe.Framework.Utility
{
    public interface IHasId<I>
        where I : struct, IEquatable<I>
    {
        I Id { get; }
    }

    public interface ICanSetId<I> : IHasId<I>
        where I : struct, IEquatable<I>
    {
        new I Id { get; set; }
    }
}
