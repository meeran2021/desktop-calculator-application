using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    public class Subtract : BinaryOperation
    {
        protected override double EvaluateBinary(double operand1, double operand2)
        {
            return operand1 - operand2;
        }
    }
}
