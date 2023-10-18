using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    public class PredefinedOperator 
    {
        protected Dictionary<string, IOperations> OperatorDictionary = new Dictionary<string, IOperations>();

        public int AddNewOperator(string operatorSymbol, IOperations operatorName)
        {
            if (OperatorDictionary.ContainsKey(operatorSymbol))
            {
                return 0;
            }
            else
            {
                OperatorDictionary.Add(operatorSymbol, operatorName);
                return 1;
            }
        }

        public IOperations GetOperatorClass(string operatorSymbol)
        {
            if (OperatorDictionary.ContainsKey(operatorSymbol))
            {
                return OperatorDictionary[operatorSymbol];
            }
            else
            {
                throw new InvalidOperationException($"Operator '{operatorSymbol}' not found.");
            }
        }


        public int GetOperatorPrecedence(string operatorSymbol)
        {
            return GetOperatorClass(operatorSymbol) != null ? GetOperatorClass(operatorSymbol).OperatorPrecedence : throw new InvalidOperationException($"Operator '{operatorSymbol}' not found.");
        }
    }
}
