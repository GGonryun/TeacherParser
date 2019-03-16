using System;
using System.Collections.Generic;
using Class;


namespace Utility
{
    public abstract class EqualsToSpecification<T> : ISpecification<T>
    {
        public T Comparand { get; protected set; }

        public EqualsToSpecification(T comparand)
        {
            this.Comparand = comparand;
        }

        public bool Satisfied(T item)
        {
            return Comparand.Equals(item);
        }
    }
}
