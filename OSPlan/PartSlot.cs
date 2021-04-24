using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPlan
{
    class PartSlot
    {
        public PartType PartType { get; private set; }

        public PartSlot(PartType partType, List<ProductPartEntity> productPartEntities)
        {
            this.PartType = partType;

            var dic = new Dictionary<string, Part>();
            foreach (var part in productPartEntities)
            {
                var p = new Part(part.PartName, part.Avaiable);
                dic.Add(part.PartName, p);
            }
            this.Parts = dic.Select(d => d.Value).ToList();
        }

        public List<Part> Parts { get; private set; }
    }
}
