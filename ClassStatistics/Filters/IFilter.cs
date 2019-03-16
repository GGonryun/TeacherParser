using System;
using System.Collections.Generic;
namespace Utility
{
    interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> content, ISpecification<T> specification);
    }
}
