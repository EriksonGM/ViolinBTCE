using System;
using System.Collections.Generic;
using System.Linq;

namespace ViolinBtce.Dto
{
    [Serializable]
    public class DtoActiveOrders
	{
		public Dictionary<int, DtoOrder> List { get; set; }
        
        public override bool Equals(object objectBeingTested)
        {
            if (!(objectBeingTested is DtoActiveOrders)) return false;

            DtoActiveOrders castedObject = objectBeingTested as DtoActiveOrders;

            if (List == null && castedObject.List == null) return true;

            if (List != null && castedObject.List == null || castedObject.List.Count != List.Count )
                return false;

            return castedObject.List.All(kvp => List.ContainsKey(kvp.Key) && List.ContainsValue(kvp.Value));
        }
	}

    // Created only to allow testing class DtoActiveOrders completely using TestCases
    [Serializable]
    public class DummyDtoActiveOrders : DtoActiveOrders
    {
        
    }
}
