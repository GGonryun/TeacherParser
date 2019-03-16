using System;
using System.Collections.Generic;
using Class;


namespace Utility
{
    public abstract class EquatibleSpecification<T> : ISpecification<IEquatable<T>>
    {
        public IEquatable<T> Comparand { get; protected set; }

        public EquatibleSpecification(IEquatable<T> comparand)
        {
            this.Comparand = comparand;
        }

        public bool Satisfied(IEquatable<T> item)
        {
            return Comparand.Equals(item);
        }
    }
}
