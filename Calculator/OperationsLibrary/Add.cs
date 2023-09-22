using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    public class Add : BinaryOperation
    {
        private int _operatorPrecedence = 1;

        public new int OperatorPrecedence
        {
            get
            {
                return this._operatorPrecedence;
            }
            set
            {
                if (value <= 0)
                {
                    ResourceManager RmInstance = new ResourceManager("OperationLibraryExceptions", typeof(Add).Assembly);
                    throw new ArgumentOutOfRangeException("value", RmInstance.GetString("PrecedenceValueException"));
                }
                else
                    _operatorPrecedence = value;
            }
        }

        protected override double EvaluateBinary(double operand1, double operand2)
        {
            return operand1 + operand2;
        }
    }
}
