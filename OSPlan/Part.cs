using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPlan
{
    class Part : SlotItem
    {
        public PartType PartType;
        public string Name;

        public Part(PartType ptype, string name, int avaiable)
        {
            this.PartType = ptype;
            this.Name = name;
            this.Avaiable = avaiable;
        }
    }
}
