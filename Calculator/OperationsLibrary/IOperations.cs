using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    public interface IOperations
    {
        int OperandCount { get; }
        int OperatorPrecedence { get; set; }

        double Evaluate(double[] operands);
    }
}
