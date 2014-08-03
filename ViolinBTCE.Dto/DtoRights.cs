﻿using System;

namespace ViolinBtce.Dto
{
    [Serializable]
	public class DtoRights
	{
		public bool Info { get; set; }
		public bool Trade { get; set; }
        public bool Withdraw { get; set; }

	    public override bool Equals(object objectBeingTested)
	    {
	        if (!(objectBeingTested is DtoRights)) return false;

	        DtoRights castedObject = objectBeingTested as DtoRights;

	        return Info == castedObject.Info &&
	               Trade == castedObject.Trade &&
	               Withdraw == castedObject.Withdraw;
	    }

        public override int GetHashCode()
        {
            return 17 + 31 * Info.GetHashCode() + 31 * Trade.GetHashCode() + 31 * Withdraw.GetHashCode();
        }
	}
}
