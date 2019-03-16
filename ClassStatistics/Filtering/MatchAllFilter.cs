using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Class;
using Filtering.Specifications;

namespace Filtering
{
    class MatchAllFilter<T> : IFilter<T>
    {
        public IEnumerable<T> Filter(IEnumerable<T> content, params ISpecification<T>[] specifications)
        {
            var filteredContent = content.Where(
                item =>
                {
                    foreach (ISpecification<T> spec in specifications)
                        if (!spec.Satisfied(item))
                            return (false);
                    return true;
                }
            );
            return filteredContent;
        }
    }
}
