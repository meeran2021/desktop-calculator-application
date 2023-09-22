using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


// This file contains lots of errors
namespace OperationsLibrary
{
    internal class Parser 
    {
        Dictionary<string, TokenType> ParsingTable = new Dictionary<string, TokenType>
        {
            { "+" , TokenType.Operator },
            { "-" , TokenType.Operator },
            { "*" , TokenType.Operator },
            { "/" , TokenType.Operator },
            { "%" , TokenType.Operator },
            { "(" , TokenType.Delimiter },
            { ")" , TokenType.Delimiter }
        };

        public static int GetPrecedence(char ch)
        {

            //switch (ch)
            //{
            //    case '+':
            //    case '-':
            //        return 1;

            //    case '*':
            //    case '/':
            //    case '%':
            //        return 2;
            //}
            return -1;
        }

        public string ConvertToPostfix(List<string> expression)
        {
            string Result = string.Empty;
            Stack<string> TokenStack = new Stack<string>();

            //for (int i = 0; i < expression.Length; ++i)
            for(int TokenIndex= 0; TokenIndex<expression.Count; TokenIndex++)
            {
                string MyToken = expression[TokenIndex];
                Token Type = MyToken;
                if (Token == TokenType)
                {
                    Result += Token;
                }

                // Will be used later for functions
                //else if (char.isletter(token))
                //{
                //    string func = "" + token;
                //    int idx = i + 1;
                //    while (idx < expression.length && char.isletter(expression[idx]))
                //    {
                //        func += expression[idx++];
                //    }
                //    stack.push(func);
                //}

                else if (Token == '(')
                {
                    TokenStack.Push(Token);
                }

                else if (Token == ')')
                {
                    while (TokenStack.Count > 0
                        && TokenStack.Peek() != '(')
                    {
                        Result += TokenStack.Pop();
                    }

                    if (TokenStack.Count > 0
                        && TokenStack.Peek() != '(')
                    {
                        return "Invalid Expression";
                    }
                    else
                    {
                        TokenStack.Pop();
                    }
                }

                else
                {
                    while (TokenStack.Count > 0
                        && GetPrecedence(Token) <= GetPrecedence(TokenStack.Peek()))
                    {
                        Result += TokenStack.Pop();
                    }
                    TokenStack.Push(Token);
                }
            }

            while (TokenStack.Count > 0)
            {
                Result += TokenStack.Pop();
            }

            return Result;
        }

        public List<string> Tokenize(string expression)
        {
            List<TokenType> TokenList = new List<TokenType>();
            int LengthOfExpression= expression.Length;
            string Token = string.Empty;
            for(int i= 0; i<LengthOfExpression; i++)
            {
                if (char.IsLetter(expression[i]))
                {
                    while (i > LengthOfExpression && expression[i] != (char)TokenType.Delimiter)
                    {
                        Token += expression[i];
                        i++;
                    }
                }
                else
                    Token += expression[i];
                TokenList.Add(new Token();
            }
            return TokenList;   
        }
    }
}
