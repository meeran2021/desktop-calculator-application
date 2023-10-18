using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using OperationsLibrary;

namespace NewUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestTokenize()
        {
            //Arrange
            Parser ParserInstance = new Parser();
            //string Path = "E:\\Visual Studio\\GrapeCity\\Assignment\\Calculator\\OperationsLibrary\\OperatorDatabase.json";
            //string JsonText = File.ReadAllText(Path);

            // Act
            List<Token> ExpressionToken = ParserInstance.Tokenize("3+4*(2-1) +4-5/6");
            //List<Token> ExpressionToken = ParserInstance.Tokenize("3+4*(2-1) + Sin ( 30 )");
            //List<Token> ExpressionToken = ParserInstance.Tokenize("3+4");

            // Assert
            //Assert.AreEqual(14, JsonText.Length);
            Assert.AreEqual(15, ExpressionToken.Count);
            Assert.AreEqual("3", ExpressionToken[0].Value);
            Assert.AreEqual("+", ExpressionToken[1].Value);
            Assert.AreEqual("4", ExpressionToken[2].Value);
            Assert.AreEqual("*", ExpressionToken[3].Value);
            Assert.AreEqual("(", ExpressionToken[4].Value);
            Assert.AreEqual("2", ExpressionToken[5].Value);
            Assert.AreEqual("-", ExpressionToken[6].Value);
            Assert.AreEqual("1", ExpressionToken[7].Value);
            Assert.AreEqual(")", ExpressionToken[8].Value);
            Assert.AreEqual("+", ExpressionToken[9].Value);
            Assert.AreEqual("4", ExpressionToken[10].Value);
            Assert.AreEqual("-", ExpressionToken[11].Value);
            Assert.AreEqual("5", ExpressionToken[12].Value);
            Assert.AreEqual("/", ExpressionToken[13].Value);
            Assert.AreEqual("6", ExpressionToken[14].Value);
        }


        [TestMethod]
        public void TestConvertToPostfix()
        {
            Parser ParserInstance = new Parser();
            List<Token> InfixTokens = ParserInstance.Tokenize("3 + 4 * ( 2 - 1 )");

            List<Token> PostfixTokens = ParserInstance.ConvertToPostfix(InfixTokens);

            Assert.AreEqual(7, PostfixTokens.Count);
            Assert.AreEqual("3", PostfixTokens[0].Value);
            Assert.AreEqual("4", PostfixTokens[1].Value);
            Assert.AreEqual("2", PostfixTokens[2].Value);
            Assert.AreEqual("1", PostfixTokens[3].Value);
            Assert.AreEqual("-", PostfixTokens[4].Value);
            Assert.AreEqual("*", PostfixTokens[5].Value);
            Assert.AreEqual("+", PostfixTokens[6].Value);
        }

        [TestMethod]
        public void TestEvaluatePostfix()
        {
            Parser ParserInstance = new Parser();
            List<Token> InfixTokens = ParserInstance.Tokenize("3 + 4 / 5"); // "3 + 4 / 5*(4*12)/3"

            List<Token> PostfixTokens = ParserInstance.ConvertToPostfix(InfixTokens);

            double result = ParserInstance.EvaluatePostfix(PostfixTokens);

            Assert.AreEqual(3.8, result);
        }

    }
}
