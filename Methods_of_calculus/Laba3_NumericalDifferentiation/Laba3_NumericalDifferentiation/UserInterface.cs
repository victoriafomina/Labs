using System;
using System.Collections.Generic;
using System.Text;

namespace Laba3_NumericalDifferentiation
{
    public class UserInterface
    {
        private NumericalDifferentiationLogic logic;

        public UserInterface()
        {
            logic = new NumericalDifferentiationLogic(new Function(), new FirstDerivative(), new SecondDerivative());
        }

        
    }
}
