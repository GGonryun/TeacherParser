using Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public interface IBuilder<T>
    {
         T GetResult();
    }
}
