using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPlan
{
    class Part
    {
        public string Name { get; protected set; }
        public int Avaiable { get; protected set; }

        public Part(string name, int avaiable)
        {
            this.Name = name;
            this.Avaiable = avaiable;
        }
        public Part(Part p)
        {
            this.Name = p.Name;
            this.Avaiable = p.Avaiable;
        }

        public Part Clone()
        {
            var p = new Part(this);
            return p;
        }
    }
}
