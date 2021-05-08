using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPlan
{
    abstract class SlotItem
    {
        public string Name { get; protected set; }
        public int Avaiable { get; protected set; }

        internal int Apply(int planCount)
        {
            var cnt = 0;
            if (this.Avaiable >= planCount)
                cnt = planCount;
            else
                cnt = this.Avaiable;
            this.Avaiable -= cnt;
            return cnt;
        }
    }
}
