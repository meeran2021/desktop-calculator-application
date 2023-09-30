using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OperationsLibrary
{
    public class Parser : PredefinedOperator
    {
        public List<OperatorItem> OperatorList;// = new List<Operator>();
        public List<Token> TokenList = new List<Token>();


        public bool IsOperatorPrecedenceHigher(string operator1, string operator2)
        {
            return GetOperatorPrecedence(operator1) > GetOperatorPrecedence(operator2);
        }


        public void DeserializeOperatorJson(string jsonText)
        {
            // Parse the JSON document
            JsonDocument ParsedJsonDocument = JsonDocument.Parse(jsonText);

            // Get the root element
            JsonElement Root = ParsedJsonDocument.RootElement;

            // Extract the "Operator" array from the JSON data
            JsonElement OperatorArray = Root.GetProperty("Operator");

            // Create a list of Operator objects
            //List<Operator>;
            OperatorList = new List<OperatorItem>();

            foreach (JsonElement OperatorItem in OperatorArray.EnumerateArray())
            {
                OperatorList.Add(new OperatorItem
                {
                    Symbol = OperatorItem.GetProperty("Symbol").GetString(),
                    Name = OperatorItem.GetProperty("Name").GetString(),
                    Precedence = OperatorItem.GetProperty("Precedence").GetString()
                });
            }

            //return OperatorList;
        }


        public List<Token> Tokenize(string expression)
        {
            string Path = "E:\\Visual Studio\\Project\\Calculator\\OperationsLibrary\\OperatorDatabase.json";
            string JsonText = File.ReadAllText(Path);


            //List<Operator>;
            //OperatorList = 
            //DeserializeOperatorJson(JsonText);
            //List<Operator>
            //OperatorList = JsonSerializer.Deserialize<List<Operator>>(JsonText);

            //OperatorList = JsonConvert.DeserializeObject<List<OperatorItem>>(JsonText);

            JsonObject root = JsonConvert.DeserializeObject<JsonObject>(JsonText);


            Dictionary<string, OperatorItem> OperatorDictionary = root.Operator.ToDictionary(
                OperatorInstance => OperatorInstance.Symbol, 
                OperatorInstance => OperatorInstance);

            //List<Token> TokenList = new List<Token>();


            int LengthOfExpression = expression.Length;
            
            for (int ExpresionIndex = 0; ExpresionIndex < LengthOfExpression; ExpresionIndex++)
            {
                string Token = string.Empty;
                char ExpressionCurrentChar = expression[ExpresionIndex];

                // For Functions like Sin, Cos, etc
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

                //else if(_parsingTable.Any(entry => entry.Key == ExpressionCurrentChar.ToString() && 
                //        entry.Value == TokenType.Operator)) 
                else if(OperatorDictionary.ContainsKey(ExpressionCurrentChar.ToString()) )
                {
                    Token = ExpressionCurrentChar.ToString();
                    TokenList.Add(new Token(TokenType.Operator, Token));
                    //Operator FetchedOperator = OperatorDictionary[ExpressionCurrentChar.ToString()];
                    //if(FetchedOperator.Symbol == TokenType.Operator.ToString())
                    //{
                    //    Token = ExpressionCurrentChar.ToString();
                    //    TokenList.Add(new Token(TokenType.Operator, Token));
                    //}
                }

                //For Numbers Including Multiple Digits
                // Handle decimal numbers required
                else if (char.IsDigit(ExpressionCurrentChar))
                {
                    while (ExpresionIndex < LengthOfExpression && char.IsDigit(expression[ExpresionIndex]))
                    {
                        Token += expression[ExpresionIndex];
                        ExpresionIndex++;
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
                    Console.WriteLine("Inside Else Condiion");  //Testing
                    //throw new Exception("Undefined Token");
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
    }
}
