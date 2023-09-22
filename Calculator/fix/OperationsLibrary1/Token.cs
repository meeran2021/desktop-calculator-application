using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsLibrary
{
    public enum TokenType
    {
        Operator,
        Operand,
        Delimiter
    };

    public class Token
    {
        public TokenType Type {  get; }
        string Value { get; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }   
    }
}
