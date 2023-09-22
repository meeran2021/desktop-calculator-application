using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    public class SquareRoot : UnaryOperation
    {
        protected override double EvaluateUnary(double operand)
        {
            return Math.Sqrt(operand);
        }
    }
}
