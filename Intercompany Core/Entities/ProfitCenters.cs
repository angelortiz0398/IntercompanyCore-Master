using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntercompanyCore
{
    public class ProfitCenters
    {
        public string CenterCode { get; set; }
        public string CenterName { get; set; }
        public int InWhichDimension { get; set; }
        public char Active { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }

        public ProfitCenters() { 
        
        }
        public ProfitCenters(string centerCode, string centerName, int inWhichDimension, char active, DateTime effectiveTo, DateTime effectiveFrom)
        {
            CenterCode = centerCode;
            CenterName = centerName;
            InWhichDimension = inWhichDimension;
            Active = active;
            EffectiveTo = effectiveTo;
            EffectiveFrom = effectiveFrom;
        }
    }
} 
