using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSLoanDecision
{
    public class WMLResponse
    {
        public Double prediction { get; set; }
        public Double probabilty { get; set; }
        public string description { get; set; }

    }
}
