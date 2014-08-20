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
        public decimal Xpm { get; set; }
        public decimal Cnh { get; set; }
        public decimal Gbp { get; set; }

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
	                   Eur == castedObject.Eur &&
                       Xpm == castedObject.Xpm &&
                       Cnh == castedObject.Cnh &&
                       Gbp == castedObject.Gbp;
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

        public override string ToString()
        {
            return String.Format("BTC: {0}\n" +
                                 "LTC: {1}\n" +
                                 "NMC: {2}\n" +
                                 "NVC: {3}\n" +
                                 "TRC: {4}\n" +
                                 "PPC: {5}\n" +
                                 "FTC: {6}\n" +
                                 "USD: {7}\n" +
                                 "RUR: {8}\n" +
                                 "EUR: {9}\n" +
                                 "XPM: {10}\n" +
                                 "CNH: {11}\n" +
                                 "GBP: {12}\n",
                                 Btc,
                                 Ltc,
                                 Nmc,
                                 Nvc,
                                 Trc,
                                 Ppc,
                                 Ftc,
                                 Usd,
                                 Rur,
                                 Eur,
                                 Xpm,
                                 Cnh,
                                 Gbp);
        }
	};

}
