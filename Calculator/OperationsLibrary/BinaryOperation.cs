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
        private int _operandCount = 2;

        public int OperandCount
        {
            get
            {
                return this._operandCount;
            }
        }

        public int OperatorPrecedence { get; set; }

        protected abstract double EvaluateBinary(double operand1, double operand2);

        public double Evaluate(double[] operands)
        {
            // validation

            if (operands.Length != _operandCount)
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
