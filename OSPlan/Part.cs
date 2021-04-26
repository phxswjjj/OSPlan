using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPlan
{
    class Part
    {
        public PartType PartType { get; protected set; }
        public string Name { get; protected set; }
        public int Avaiable { get; protected set; }

        public Part(PartType ptype, string name, int avaiable)
        {
            this.PartType = ptype;
            this.Name = name;
            this.Avaiable = avaiable;
        }

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
