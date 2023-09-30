using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    public class SquareRoot : UnaryOperation
    {
        public override int OperatorPrecedence => 3;

        protected override double EvaluateUnary(double operand)
        {
            return Math.Sqrt(operand);
        }
    }
}
