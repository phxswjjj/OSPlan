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
        public List<Part> Parts { get; private set; }

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

        internal int TryPlan(int avaiableCount)
        {
            if (avaiableCount == 0) return 0;
            var partCount = this.Parts.Sum(p => p.Avaiable);
            var planCount = 0;
            if (avaiableCount >= partCount)
                planCount = partCount;
            else
                planCount = avaiableCount;
            return planCount;
        }

        internal bool ApplyPlan(int planCount)
        {
            var origin = planCount;
            foreach (var part in this.Parts)
            {
                planCount -= part.Apply(planCount);
            }
            return origin != planCount;
        }
    }
}
