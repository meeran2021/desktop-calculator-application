using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    public abstract class UnaryOperation : IOperations
    {
        public int OperandCount => 1;
        public abstract int OperatorPrecedence { get; }

        protected abstract double EvaluateUnary(double operand);

        public double Evaluate(double[] operands)
        {
            // validation

            if (operands.Length != OperandCount)
            {
                ResourceManager rm = new ResourceManager("Resources", typeof(UnaryOperation).Assembly);
                throw new ArgumentException(rm.GetString("OperandException"));
            }
            // call absrat
            else
            {
                return EvaluateUnary(operands[0]);
            }
        }
    }

}
