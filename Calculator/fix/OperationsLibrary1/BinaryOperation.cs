using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Diagnostics;
//using Calculator;

namespace OperationsLibrary
{
    public abstract class BinaryOperation : IOperations
    {
        private int _operandCount => 2;
        private int _operatorPrecedence;
        public int OperandCount
        {
            get
            {
                return this._operandCount;
            }
        }

        public int OperatorPrecedence 
        {
            get
            {
                return this._operatorPrecedence;
            }
            set
            {
                if(value <= 0)
                {
                    ResourceManager rm = new ResourceManager("Resources", typeof(BinaryOperation).Assembly);
                    throw new ArgumentOutOfRangeException("value", rm.GetString("precedenceValueException"));
                }
                else
                    _operatorPrecedence = value;
            }
        }

        protected abstract double EvaluateBinary(double operand1, double operand2);

        public double Evaluate(double[] operands)
        {
            // validation

            if (operands.Length != _operandCount)
            {
                ResourceManager rm = new ResourceManager("Resources", typeof(BinaryOperation).Assembly);
                throw new ArgumentException(rm.GetString("OperandException"));
            }
            // call absrat
            else
            {
                return EvaluateBinary(operands[0], operands[1]);
            }
        }
    }
}
