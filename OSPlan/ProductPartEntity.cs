using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPlan
{
    class ProductPartEntity
    {
        public string ProductName;
        public PartType PartType;
        public string PartName;
        public int Avaiable;
    }

    enum PartType
    {
        ProbeCard,
        Sensor,
        WireProbeCard,
    }
}
