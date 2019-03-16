using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Class;

namespace Filtering
{
    class MeetingFilter : IFilter<Meeting>
    {
        public IEnumerable<Meeting> Filter(IEnumerable<Meeting> content, params ISpecification<Meeting>[] specifications)
        {
            var filteredContent = content.Where(
                instructor =>
                {
                    foreach (ISpecification<Meeting> spec in specifications)
                        if (!spec.Satisfied(instructor))
                            return (false);
                    return true;
                }
            );
            return filteredContent;
        }
    }
}
