using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proba
{
    public class HouseRule
    {
        //fields
        private String HouseRuleText;

        public HouseRule(String HouseRule)
        {
            this.HouseRuleText = HouseRule;
        }
        //methods
        public String GetHouseRule()
        {
            return this.HouseRuleText;
        }
        public string GetInfo()
        {
            return $"HouseRule: {this.HouseRuleText}";
        }

    }
}
