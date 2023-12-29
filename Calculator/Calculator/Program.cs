using OperationsLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
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
            // Set the culture to French (France)
            //CultureInfo culture = CultureInfo.GetCultureInfo("fr-FR");
            //Thread.CurrentThread.CurrentCulture = culture;
            //Thread.CurrentThread.CurrentUICulture = culture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CalculatorForm());


            //Parser ParserInstance = new Parser();

            //List<Token> ExpressionToken = ParserInstance.Tokenize("3+4*(2-1)+7/3*4");
            //Console.WriteLine("Length of ExpressionToken: " + ExpressionToken.Count);

            //List<Token> PostfixExpression = ParserInstance.ConvertToPostfix(ExpressionToken);
            //Console.WriteLine("Length of Postfix Expression: " + PostfixExpression.Count);
                        
            //Console.ReadLine();

        }
    }
}
