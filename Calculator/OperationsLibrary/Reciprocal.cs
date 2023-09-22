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
                    ResourceManager rm = new ResourceManager("Resources", typeof(Reciprocal).Assembly);
                    throw new ArgumentOutOfRangeException("value", rm.GetString("PrecedenceValueException"));
                }
                else
                    this._operatorPrecedence = value;
            }
        }
        protected override double EvaluateUnary(double operand)
        {
            return 1 / operand;
        }
    }
}
