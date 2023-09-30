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
        public void TestMethod1()
        {
            //Arrange
            Parser ParserInstance = new Parser();
            //string Path = "E:\\Visual Studio\\GrapeCity\\Assignment\\Calculator\\OperationsLibrary\\OperatorDatabase.json";
            //string JsonText = File.ReadAllText(Path);

            // Act
            List<Token> ExpressionToken = ParserInstance.Tokenize("3+4*(2-1) + Sin ( 30 )");
            //List<Token> ExpressionToken = ParserInstance.Tokenize("3+4");

            // Assert
            //Assert.AreEqual(14, JsonText.Length);
            Assert.AreEqual(14, ExpressionToken.Count);
            Assert.AreEqual("3", ExpressionToken[0].Value);
            Assert.AreEqual("+", ExpressionToken[1].Value);
            Assert.AreEqual("4", ExpressionToken[2].Value);
            Assert.AreEqual("*", ExpressionToken[3].Value);
            Assert.AreEqual("(", ExpressionToken[4].Value);
            Assert.AreEqual("2", ExpressionToken[5].Value);
            Assert.AreEqual("-", ExpressionToken[6].Value);
            Assert.AreEqual("1", ExpressionToken[7].Value);
            Assert.AreEqual(")", ExpressionToken[8].Value);
        }


        [TestMethod]
        public void TestConvertToPostfix()
        {
            Parser ParserInstance = new Parser();
            List<Token> InfixTokens = ParserInstance.Tokenize("3 + 4 * ( 2 - 1 )");

            List<Token> PostfixTokens = ParserInstance.ConvertToPostfix(InfixTokens);

            Assert.AreEqual(9, PostfixTokens.Count);
            Assert.AreEqual("3", PostfixTokens[0].Value);
            Assert.AreEqual("4", PostfixTokens[1].Value);
            Assert.AreEqual("2", PostfixTokens[2].Value);
            Assert.AreEqual("1", PostfixTokens[3].Value);
            Assert.AreEqual("-", PostfixTokens[4].Value);
            Assert.AreEqual("*", PostfixTokens[5].Value);
            Assert.AreEqual("+", PostfixTokens[6].Value);
            Assert.AreEqual("(", PostfixTokens[7].Value);
            Assert.AreEqual(")", PostfixTokens[8].Value);
        }

    }
}
