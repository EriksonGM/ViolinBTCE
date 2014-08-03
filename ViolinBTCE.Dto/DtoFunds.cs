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

        public override int GetHashCode()
        {
            return 17 + 31 * Btc.GetHashCode() + 31 * Ltc.GetHashCode() + 31 * Nmc.GetHashCode() +
                        31 * Nvc.GetHashCode() + 31 * Trc.GetHashCode() + 31 * Ppc.GetHashCode() +
                        31 * Ftc.GetHashCode() + 31 * Usd.GetHashCode() + 31 * Eur.GetHashCode();
        }
	};

}
