using System;

namespace ViolinBtce.Dto
{
    [Serializable]
	public class DtoCancelOrderAnswer
	{
		public int OrderId { get; set; }
		public DtoFunds DtoFunds { get; set; }
	}
}
