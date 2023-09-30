using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Diagnostics;

namespace OperationsLibrary
{
    public abstract class BinaryOperation : IOperations
    {
        public int OperandCount => 2;

        public abstract int OperatorPrecedence { get; }

        protected abstract double EvaluateBinary(double operand1, double operand2);

        public double Evaluate(double[] operands)
        {
            // validation

            if (operands.Length != OperandCount)
            {
                ResourceManager RmInstance = new ResourceManager("Resources", typeof(BinaryOperation).Assembly);
                throw new ArgumentException(RmInstance.GetString("OperandException"));
            }
            // call absrat
            else
            {
                return EvaluateBinary(operands[0], operands[1]);
            }
        }
    }
}
