using Filtering.Specifications;
using System;
using System.Collections.Generic;
namespace Filtering
{
    interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> content, params ISpecification<T>[] specification);
    }
}
