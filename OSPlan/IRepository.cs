using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPlan
{
    interface IRepository<T>
    {
        T Read(Predicate<T> find);
        IEnumerable<T> Reads(Predicate<T> find);
        IEnumerable<T> ReadAll();
    }
}
