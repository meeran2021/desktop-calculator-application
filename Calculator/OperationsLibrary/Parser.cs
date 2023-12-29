using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
//using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;


namespace OperationsLibrary
{
    public class Parser : PredefinedOperators
    {
        public List<Token> TokenList = new List<Token>();
         

        public Parser()
        {
            InitializeOperatorDictionary();
        }


        public List<Token> Tokenize(string expression)
        {
            int LengthOfExpression = expression.Length;
            
            for (int ExpresionIndex = 0; ExpresionIndex < LengthOfExpression; ExpresionIndex++)
            {
                string Token = string.Empty;
                char ExpressionCurrentChar = expression[ExpresionIndex];

                // For Functions like Sqrt, Sin, Cos, etc
                if (char.IsLetter(ExpressionCurrentChar))
                {
                    while (ExpresionIndex < LengthOfExpression && char.IsLetter(expression[ExpresionIndex]))
                    {
                        Token += expression[ExpresionIndex];
                        ExpresionIndex++;
                    }
                    ExpresionIndex--; 
                    TokenList.Add(new Token(TokenType.Operator, Token));
                }

                // Needs to handle negative numbers
                else if(OperatorDictionary.ContainsKey(ExpressionCurrentChar.ToString()) == true )
                    //&& ((ExpresionIndex != 0) && OperatorDictionary.ContainsKey(expression[ExpresionIndex].ToString()) == false))
                {
                    Token = ExpressionCurrentChar.ToString();
                    TokenList.Add(new Token(TokenType.Operator, Token));
                }

                //For Numbers Including Multiple Digits
                else if (char.IsDigit(ExpressionCurrentChar))
                {
                    int CounterDecimal = 0;
                    while (ExpresionIndex < LengthOfExpression)
                    {
                        if (char.IsDigit(expression[ExpresionIndex]) || (expression[ExpresionIndex] == '.' && CounterDecimal++ == 0))
                        {
                            Token += expression[ExpresionIndex];
                            ExpresionIndex++;
                        }
                        else break;
                    }
                    ExpresionIndex--;
                    TokenList.Add(new Token(TokenType.Operand, Token));
                }

                else if (ExpressionCurrentChar == '(' || ExpressionCurrentChar == ')')
                {
                    Token = ExpressionCurrentChar.ToString();
                    TokenList.Add(new Token(TokenType.Delimiter, Token));
                }

                else if (char.IsWhiteSpace(ExpressionCurrentChar))
                {
                    continue;
                }

                else
                {
                    try
                    {
                        //Console.WriteLine("Inside Else Condiion");  //Testing
                        throw new Exception(OperationLibraryExceptions.InvalidOperatorException);

                    }

                    catch (Exception Ex)
                    {
                        //ShowErrorPopup(Ex.Message);
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }

            return TokenList;
        }

        public List<Token> ConvertToPostfix(List<Token> infixTokens)
        {
            List<Token> PostfixTokens = new List<Token>();
            Stack<Token> OperatorStack = new Stack<Token>();
            int LastTokenWas = 0;
            foreach (Token TokenUnit in infixTokens)
            {
                if (TokenUnit.Type == TokenType.Operand)
                {
                    try
                    {
                        if (LastTokenWas != 1)
                            PostfixTokens.Add(TokenUnit);
                        else
                        {
                            throw new Exception(OperationLibraryExceptions.InvalidOperatorException);
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    LastTokenWas = 1;
                }

                else if (TokenUnit.Type == TokenType.Operator)
                {
                    while (OperatorStack.Count > 0 &&
                           OperatorStack.Peek().Type == TokenType.Operator &&
                           IsOperatorPrecedenceHigher(TokenUnit.Value, OperatorStack.Peek().Value))
                    {
                        PostfixTokens.Add(OperatorStack.Pop()); 
                    }
                    OperatorStack.Push(TokenUnit);
                    LastTokenWas = 2;
                }
                // currently not in use
                else if (TokenUnit.Type == TokenType.Delimiter)
                {
                    if (TokenUnit.Value == "(")
                    {
                        OperatorStack.Push(TokenUnit); 
                    }
                    else if (TokenUnit.Value == ")")
                    {
                        while (OperatorStack.Count > 0 && OperatorStack.Peek().Value != "(")
                        {
                            PostfixTokens.Add(OperatorStack.Pop());
                        }
                        if (OperatorStack.Count > 0 && OperatorStack.Peek().Value == "(")
                        {
                            OperatorStack.Pop();
                        }
                        else
                        {
                            ResourceManager RmInstance = new ResourceManager("Resources", typeof(Parser).Assembly);
                            Exception Ex = new ArgumentException(RmInstance.GetString("MismatchedParentheses"));
                            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw Ex;
                        }
                    }
                    LastTokenWas = 3;
                }
            }

            while (OperatorStack.Count > 0)
            {
                PostfixTokens.Add(OperatorStack.Pop());
            }

            return PostfixTokens;
        }


        public double EvaluatePostfix(List<Token> postfixTokens)
        {
            Stack<double> operandStack = new Stack<double>();
            
            foreach (Token token in postfixTokens)
                {
                if (token.Type == TokenType.Operand)
                {
                    operandStack.Push(double.Parse(token.Value));  // Parsing based on current culture -> Need fixing
                }
                else if (token.Type == TokenType.Operator)
                {
                    //if (OperatorDictionary.TryGetValue(token.Value, out OperatorItem operatorItem))
                    IOperations operatorClass = GetOperatorClass(token.Value);
                    int operandCount = operatorClass.OperandCount;

                    try
                    {
                        if (operandCount <= operandStack.Count)
                        {
                            double[] operands = new double[operandCount];
                            for (int operandIndex = operandCount - 1; operandIndex >= 0; operandIndex--)
                            {
                                operands[operandIndex] = operandStack.Pop();
                            }
                            double result = operatorClass.Evaluate(operands);
                            operandStack.Push(result);
                        }
                        else
                        {
                            throw new Exception(OperationLibraryExceptions.NotEnoughOperandsInStackToPerformOperation);
                        }

                    }
                    catch (Exception Ex)
                        {
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }

                    // Codeblock unreachable as input is taken from clicking the buttons displayed on calculator and no undefined button present there.
                    //else
                    //{
                    //    throw new InvalidOperationException("Operator not found in the dictionary");
                    //}

            }
 
            if (operandStack.Count == 1)
            {
                return operandStack.Peek();
            }
            else
            {
                Exception Ex = new InvalidOperationException("Invalid postfix expression");
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw Ex;
            }
            
        }
    }
}


