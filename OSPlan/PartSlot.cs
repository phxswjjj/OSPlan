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
            if (planCount <= 0)
                throw new Exception($"plan is equal or less than zero");

            var origin = planCount;
            var maxAvaiable = this.SlotItems.Max(p => p.Avaiable);
            //先處理數量較多的項目，逐一抵用
            for (var i = maxAvaiable; i > 0; i--)
            {
                var checkAvaiable = i;

                var parts = (from p in this.SlotItems
                             orderby p.Avaiable * -1, p.Name
                             select p).GetEnumerator();
                while (parts.MoveNext())
                {
                    var part = parts.Current;
                    if (part.Avaiable == checkAvaiable)
                    {
                        planCount -= part.Apply(1);
                        if (planCount == 0) break;
                    }
                }
                if (planCount == 0) break;
            }
            return origin != planCount;
        }
    }
}
