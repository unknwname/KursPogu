using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPogud
{
    internal class LogicMain
    {
        private int FinalCost;
        public int ValidateLoging(string l, string p, string key)
        {
            Logic logic = new Logic();
            return logic.Logging(l, p, key);
        }
    }
}
