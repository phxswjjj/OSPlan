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

        public Product(string name, List<ProductPartEntity> productPartEntities)
        {
            this.Name = name;

            var dic = new Dictionary<PartType, PartSlot>();
            this.PartSlots = new List<PartSlot>();
            var gPartTypes = productPartEntities.GroupBy(p => p.PartType);
            foreach(var gPartType in gPartTypes)
            {
                var slot = new PartSlot(gPartType.Key, gPartType.ToList());
                dic.Add(gPartType.Key, slot);
            }
            this.PartSlots = dic.Select(d => d.Value).ToList();
        }
    }
}
