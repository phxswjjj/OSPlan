using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPlan
{
    class PartSlot : ISlot<SlotItem>
    {
        public PartType PartType { get; private set; }
        public List<SlotItem> SlotItems { get; private set; }

        public PartSlot(PartType partType, List<ProductPartRelation> productPartEntities, IRepository<Part> partRepo)
        {
            this.PartType = partType;

            var dic = new Dictionary<string, Part>();
            foreach (var productPart in productPartEntities)
            {
                var part = partRepo.Read(p => p.PartType == productPart.PartType && p.Name == productPart.PartName);
                if (part == null)
                    throw new Exception($"{partType}, {productPart.PartName} not found");
                dic.Add(productPart.PartName, part);
            }
            this.SlotItems = dic.Select(d => d.Value).Select(d => (SlotItem)d).ToList();
        }

        public int TryPlan(int avaiableCount)
        {
            if (avaiableCount == 0) return 0;
            var partCount = this.SlotItems.Sum(p => p.Avaiable);
            var planCount = 0;
            if (avaiableCount >= partCount)
                planCount = partCount;
            else
                planCount = avaiableCount;
            return planCount;
        }

        public bool ApplyPlan(int planCount)
        {
            var origin = planCount;
            foreach (var part in this.SlotItems)
            {
                planCount -= part.Apply(planCount);
            }
            return origin != planCount;
        }
    }
}
