using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPlan
{
    class Product
    {
        public string Name;
        public List<PartSlot> PartSlots { get; private set; }
        public int EqpCount { get; private set; }

        public Product(string name, List<ProductPartEntity> productPartEntities)
        {
            this.Name = name;

            var dic = new Dictionary<PartType, PartSlot>();
            this.PartSlots = new List<PartSlot>();
            var gPartTypes = productPartEntities.GroupBy(p => p.PartType);
            foreach (var gPartType in gPartTypes)
            {
                var slot = new PartSlot(gPartType.Key, gPartType.ToList());
                dic.Add(gPartType.Key, slot);
            }
            this.PartSlots = dic.Select(d => d.Value).ToList();
        }

        public int ApplyPlan(int avaiableCount)
        {
            if (avaiableCount == 0) return 0;
            if (this.PartSlots.Count == 0) return 0;

            var planCount = avaiableCount;
            #region check
            foreach (var slot in this.PartSlots)
            {
                var cnt = slot.TryPlan(planCount);
                if (cnt < planCount)
                    planCount = cnt;
                if (planCount == 0)
                    break;
            }
            #endregion

            if (planCount == 0)
                return 0;

            #region apply
            foreach (var slot in this.PartSlots)
            {
                if (!slot.ApplyPlan(planCount))
                    throw new Exception($"{this.Name} Apply {planCount} Fail");
            }
            #endregion
            this.EqpCount = planCount;
            return planCount;
        }
    }
}
