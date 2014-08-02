using System;
using System.Collections.Generic;

namespace ViolinBtce.Dto
{
    [Serializable]
    public class DtoTradeHistory
	{
		public Dictionary<int, DtoTrade> List { get; set; }
	}
}
