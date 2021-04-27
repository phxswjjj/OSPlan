using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPlan
{
    interface ISlot<T>
        where T : SlotItem
    {
        /// <summary>
        /// 試算開機數
        /// </summary>
        /// <param name="avaiableCount">可用開機數</param>
        /// <returns>開機數</returns>
        int TryPlan(int avaiableCount);
        /// <summary>
        /// 套用計畫開機數
        /// </summary>
        /// <param name="planCount">計畫開機數</param>
        /// <returns>false=異常，計畫開機數未生效</returns>
        bool ApplyPlan(int planCount);
    }
}
