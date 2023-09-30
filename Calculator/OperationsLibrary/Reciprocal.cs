using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    public class Reciprocal : UnaryOperation
    {
        public override int OperatorPrecedence => 3;

        protected override double EvaluateUnary(double operand)
        {
            return 1 / operand;
        }
    }
}
