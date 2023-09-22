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
        private int _operatorPrecedence = 3;
        
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
                    ResourceManager RmInstance = new ResourceManager("Resources", typeof(SquareRoot).Assembly);
                    throw new ArgumentOutOfRangeException("value", RmInstance.GetString("precedenceValueException"));
                }
                else
                    this._operatorPrecedence = value;
            }
        }

        protected override double EvaluateUnary(double operand)
        {
            return Math.Sqrt(operand);
        }
    }
}
