using System;
using System.Collections.Generic;

namespace ViolinBtce.Dto
{
    [Serializable]
    public class DtoTransHistory {
        public Dictionary<int, DtoTransaction> List { get; set; }
    }
}
