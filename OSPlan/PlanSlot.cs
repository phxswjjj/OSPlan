using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPlan
{
    class PlanSlot : ISlot<SlotItem>
    {
        public string ProductName;
        public int EqpCount;

        public PlanSlot(string productName, int eqpCount)
        {
            this.ProductName = productName;
            this.EqpCount = eqpCount;
        }

        public bool ApplyPlan(int planCount)
        {
            var origin = planCount;
            planCount -= planCount;
            return origin != planCount;
        }

        public int TryPlan(int avaiableCount)
        {
            if (avaiableCount == 0) return 0;
            var eqpCount = this.EqpCount;
            var planCount = 0;
            if (avaiableCount >= eqpCount)
                planCount = eqpCount;
            else
                planCount = avaiableCount;
            return planCount;
        }
    }
}
