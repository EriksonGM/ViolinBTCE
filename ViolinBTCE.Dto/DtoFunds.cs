using System;

namespace ViolinBtce.Dto
{
    [Serializable]
	public class DtoFunds
	{
        public decimal Btc { get; set; }
        public decimal Ltc { get; set; }
        public decimal Nmc { get; set; }
        public decimal Nvc { get; set; }
        public decimal Trc { get; set; }
        public decimal Ppc { get; set; }
        public decimal Ftc { get; set; }
		public decimal Usd { get; set; }
		public decimal Rur { get; set; }
        public decimal Eur { get; set; }

	    public override bool Equals(object objectBeingTested)
	    {
	        try
	        {
	            DtoFunds castedObject = (DtoFunds) objectBeingTested;

	            return Btc == castedObject.Btc &&
	                   Ltc == castedObject.Ltc &&
	                   Nmc == castedObject.Nmc &&
	                   Nvc == castedObject.Nvc &&
	                   Trc == castedObject.Trc &&
	                   Ppc == castedObject.Ppc &&
	                   Ftc == castedObject.Ftc &&
	                   Usd == castedObject.Usd &&
	                   Eur == castedObject.Eur;
	        }
	        catch (Exception)
	        {
	            return false;
	        }
	    }
	};

}
