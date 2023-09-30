using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    public class Subtract : BinaryOperation  
    {
        public override int OperatorPrecedence => 1;

        protected override double EvaluateBinary(double operand1, double operand2)
        {
            return operand1 - operand2;
        }
    }
}
