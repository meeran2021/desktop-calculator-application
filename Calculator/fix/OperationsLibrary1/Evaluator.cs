using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    internal class Evaluator
    {
        private string _expression;

        public int Expression { get; set; }

        public List<string> Solve(string expression)
        {
            if (expression == string.Empty) 
            {
                ResourceManager rm = new ResourceManager("Resources", typeof(BinaryOperation).Assembly);
                throw new Exception(rm.GetString("EmptyExpression"));
            }
            else
            {
                Parser ExpressionParser = new Parser();
                List<string> ListOfToken = ExpressionParser.Tokenize(expression);
                ExpressionParser.ConvertToPostfix(ListOfTokens);


            }
        }
    }
}
