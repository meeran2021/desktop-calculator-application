using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    public abstract class UnaryOperation
    {
        private int _operandCount => 1;
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
                if (value <= 0)
                {
                    ResourceManager rm = new ResourceManager("Resources", typeof(UnaryOperation).Assembly);
                    throw new ArgumentOutOfRangeException("value", rm.GetString("precedenceValueException"));
                }
                else
                    this._operatorPrecedence = value;
            }
        }

        protected abstract double EvaluateUnary(double operand);

        public double Evaluate(double[] operands)
        {
            // validation

            if (operands.Length != _operandCount)
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
