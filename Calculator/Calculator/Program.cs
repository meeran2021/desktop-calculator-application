﻿using OperationsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());



            //Console.WriteLine("Testing Message");
            //OperationsLibrary.Evaluator evaluate = new OperationsLibrary.Evaluator();
            //Console.WriteLine(evaluate.Solve("2+3+4+5"));

            //Parser ParserInstance = new Parser();

            //List<Token> ExpressionToken = ParserInstance.Tokenize("3 + 4 * 2 - 1");

            //Console.WriteLine("Length of ExpressionToken: " + ExpressionToken.Count);
            //foreach (Token TokenUnit in ExpressionToken)
            //{
            //    Console.WriteLine("Token: " + TokenUnit.ToString());
            //}
            //Console.ReadLine();



            //Console.ReadLine();

        }
    }
}