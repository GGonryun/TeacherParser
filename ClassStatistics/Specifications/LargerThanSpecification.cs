using System;
using System.Collections.Generic;
using Class;


namespace Utility
{
    public abstract class LargerThanSpecification<T> : ISpecification<T> where T : IComparable<T>
    {
        public T Comparand { get; protected set; }

        public LargerThanSpecification(T comparand)
        {
            this.Comparand = comparand;
        }

        public bool Satisfied(T item)
        {
            return Comparand.CompareTo(item) > 0;
        }
    }
}
