using System;
using System.Collections.Generic;

namespace ViolinBtce.Dto
{
    [Serializable]
    public class DtoDepth
	{
		public List<DtoOrderInfo> Asks { get; set; }
		public List<DtoOrderInfo> Bids { get; set; }
	}
}
