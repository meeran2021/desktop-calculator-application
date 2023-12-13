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

namespace OperationsLibrary
{
    public class Parser : PredefinedOperator
    {
        public List<OperatorItem> OperatorList; 
        public List<Token> TokenList = new List<Token>();


        public Parser()
        {
            InitializeOperatorDictionary();
        }


        // Method to load and initialize operators from JSON
        private void InitializeOperatorDictionary()
        {
            string jsonPath = "E:\\Visual Studio\\Project\\Calculator\\OperationsLibrary\\OperatorDatabase.json";

            try
            {
                string jsonText = File.ReadAllText(jsonPath);
                var jsonObject = JsonConvert.DeserializeObject<JsonObject>(jsonText);

                if (jsonObject.Operator != null)
                {
                    var operators = jsonObject.Operator;

                    foreach (var operatorItem in operators)
                    {
                        IOperations operatorInstance = Activator.CreateInstance(Type.GetType(operatorItem.ClassName)) as IOperations;
                        AddNewOperator(operatorItem.Symbol, operatorInstance);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: JSON file not found - {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error: JSON parsing error - {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: An unexpected error occurred - {ex.Message}");
            }
        }



        public bool IsOperatorPrecedenceHigher(string operator1, string operator2)
        {
            return GetOperatorPrecedence(operator1) > GetOperatorPrecedence(operator2);
        }


        public List<Token> Tokenize(string expression)
        {
            string Path = "E:\\Visual Studio\\Project\\Calculator\\OperationsLibrary\\OperatorDatabase.json";
            string JsonText = File.ReadAllText(Path);

            JsonObject root = JsonConvert.DeserializeObject<JsonObject>(JsonText);


            Dictionary<string, OperatorItem> OperatorDictionary = root.Operator.ToDictionary(
                OperatorInstance => OperatorInstance.Symbol, 
                OperatorInstance => OperatorInstance);


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

                else if(OperatorDictionary.ContainsKey(ExpressionCurrentChar.ToString()) )
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
                    //Console.WriteLine("Inside Else Condiion");  //Testing
                    throw new Exception("Undefined Token");
                }
            }

            return TokenList;
        }

        public List<Token> ConvertToPostfix(List<Token> infixTokens)
        {
            List<Token> PostfixTokens = new List<Token>();
            Stack<Token> OperatorStack = new Stack<Token>();

            foreach (Token TokenUnit in infixTokens)
            {
                if (TokenUnit.Type == TokenType.Operand)
                {
                    PostfixTokens.Add(TokenUnit); 
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
                }

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
                            throw new ArgumentException(RmInstance.GetString("MismatchedParentheses"));
                        }
                    }
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
                    operandStack.Push(double.Parse(token.Value));
                }
                else if (token.Type == TokenType.Operator)
                {
                    //if (OperatorDictionary.TryGetValue(token.Value, out OperatorItem operatorItem))
                    IOperations operatorClass = GetOperatorClass(token.Value);
                    int operandCount = operatorClass.OperandCount;
                    double[] operands = new double[operandCount];
                    for (int operandIndex = operandCount - 1; operandIndex >= 0; operandIndex--)
                    {
                        operands[operandIndex] = operandStack.Pop();
                    }
                    double result = operatorClass.Evaluate(operands);
                    operandStack.Push(result);
                }
                //{
                //    IOperations operatorInstance = GetOperatorClass(operatorItem.Symbol);

                //    if (operatorInstance is BinaryOperation binaryOperator)
                //    {
                //        double operand2 = operandStack.Pop();
                //        double operand1 = operandStack.Pop();
                //        double result = binaryOperator.EvaluateBinary(operand1, operand2);
                //        operandStack.Push(result);
                //    }
                //    else if (operatorInstance is UnaryOperation unaryOperator)
                //    {
                //        double operand = operandStack.Pop();
                //        double result = unaryOperator.EvaluateUnary(operand);
                //        operandStack.Push(result);
                //    }
                //    // Add additional cases for other types of operators as needed
                //}
                else
                {
                    throw new InvalidOperationException("Operator not found in the dictionary");
                }
                
            }

            if (operandStack.Count == 1)
            {
                return operandStack.Peek();
            }
            else
            {
                throw new InvalidOperationException("Invalid postfix expression");
            }
        }



    }
}
