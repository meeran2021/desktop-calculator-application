using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    internal static class TestClass
    {
        public static void Main()
        {
            Parser ParserInstance = new Parser();

            List<Token> ExpressionToken = ParserInstance.Tokenize("3 + 4 * 2 - 1");

            Console.WriteLine("Length of ExpressionToken: " + ExpressionToken.Count);
            foreach(Token TokenUnit in ExpressionToken) 
            {
                Console.WriteLine("Token: " + TokenUnit.ToString());
            }
            Console.ReadLine();
        }
    }
}
