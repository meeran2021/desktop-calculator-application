using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    public class OldOperator 
    {
        private Dictionary<string, IOperations> _operatorDictionary = new Dictionary<string, IOperations>();

        public int AddNewOperator(string operatorSymbol, IOperations operatorName)
        {
            if (_operatorDictionary.ContainsKey(operatorSymbol))
            {
                return 0;
            }
            else
            {
                _operatorDictionary.Add(operatorSymbol, operatorName);
                return 1;
            }
        }

        public IOperations GetOperatorClass(string operatorSymbol)
        {
            if (_operatorDictionary.ContainsKey(operatorSymbol))
            {
                return _operatorDictionary[operatorSymbol];
            }
            else
            {
                return null;
            }
        }

        public int GetOperatorPrecedence(string operatorSymbol)
        {
            return GetOperatorClass(operatorSymbol).OperatorPrecedence;
        }
    }
}
