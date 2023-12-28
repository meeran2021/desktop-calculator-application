using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    // This class is not in use currently
    public class Evaluator : PredefinedOperators
    {
        //private string _expression;

        //public int Expression { get; set; }
        private double TokenToDouble(Token operand)
        {
            string TokenString = operand.ToString();
            int TokenStringIndex = 0;
            double TokenValueInDouble = 0;
            while (TokenStringIndex < TokenString.Length)
            {
                TokenValueInDouble = TokenValueInDouble * 10 + (double)TokenString[TokenStringIndex++] - '0';
            }
            return TokenValueInDouble;
        }

        public double SolvePostfix(List<Token> Tokens)
        {
            Stack<double> TokenStack = new Stack<double>();
            int StackLength = Tokens.Count;
            double[] Operands;
            ResourceManager RmInstance = new ResourceManager("Resources", typeof(Parser).Assembly);
            for (int TokenIndex = 0; TokenIndex < StackLength; TokenIndex++)
            {
                Token Token = Tokens[TokenIndex];
                if (Token.Type == TokenType.Operand)
                {
                    TokenStack.Push(TokenToDouble(Token));
                }
                else if (Token.Type == TokenType.Operator)
                {
                    IOperations OperatorClass = GetOperatorClass(Token.Value);

                    if (OperatorClass != null)
                    {
                        int RequiredOperandCount = OperatorClass.OperandCount;

                        Operands = new double[RequiredOperandCount];
                        if (TokenStack.Count >= RequiredOperandCount)
                        {
                            int OperandsIndex = 0;
                            while (OperandsIndex < RequiredOperandCount)
                            {
                                Operands[OperandsIndex++] = TokenStack.Pop();
                            }
                            double EvaluationResult = OperatorClass.Evaluate(Operands);
                            TokenStack.Push(EvaluationResult);
                        }
                        else
                        {
                            throw new Exception(RmInstance.GetString("NotEnoughOperandsInStackToPerformOperation"));
                        }
                    }
                    else
                    {
                        throw new Exception(RmInstance.GetString("OperatorClassNotFoundException"));
                    }

                }
                else
                {
                    throw new Exception(RmInstance.GetString("InvalidOperandType"));
                }
            }
            return TokenStack.Pop();
        }

        public List<string> Solve(string expression)
        {
            if (expression == string.Empty) 
            {
                ResourceManager rm = new ResourceManager("Resources", typeof(Evaluator).Assembly);
                throw new Exception(rm.GetString("EmptyExpression"));
            }
            else
            {
                Parser ExpressionParser = new Parser();
                List<Token> ListOfTokens = ExpressionParser.Tokenize(expression);
                ExpressionParser.ConvertToPostfix(ListOfTokens);

                throw new Exception();  // Temp
            }
        }
    }
}
