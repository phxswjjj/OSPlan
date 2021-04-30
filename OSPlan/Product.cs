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
        public List<ISlot<SlotItem>> Slots { get; private set; }
        public int EqpCount { get; private set; }

        public Product(ProductEqpPlan productPlan, IRepository<Part> partRepo, IRepository<ProductPartRelation> productRepo)
        {
            this.Name = productPlan.ProductName;

            var dic = new Dictionary<PartType, PartSlot>();
            var gPartTypes = productRepo.Reads(p => p.ProductName == this.Name).GroupBy(p => p.PartType);
            foreach (var gPartType in gPartTypes)
            {
                var slot = new PartSlot(gPartType.Key, gPartType.ToList(), partRepo);
                dic.Add(gPartType.Key, slot);
            }
            this.Slots = dic.Select(d => d.Value).Select(d => (ISlot<SlotItem>)d).ToList();

            this.Slots.Add(new PlanSlot(this.Name, productPlan.EqpCount));
        }

        public int ApplyPlan(int avaiableCount)
        {
            if (avaiableCount == 0) return 0;
            if (this.Slots.Count == 0) return 0;

            var planCount = avaiableCount;
            #region check
            foreach (var slot in this.Slots)
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
            foreach (var slot in this.Slots)
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
