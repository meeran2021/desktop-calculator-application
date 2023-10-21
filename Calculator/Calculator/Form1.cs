using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OperationsLibrary;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TextExpressionBox.Select();
        }

        private void TextResult_DisplayBox(object sender, EventArgs e)
        {

        }

        private void TextExpression_DisplayBox(object sender, EventArgs e)
        {

        }
        private void btnDecimal_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += ".";
        }

        private void btnEqualsto_Click(object sender, EventArgs e)
        {
            string Expression = TextExpressionBox.Text;
            if (Expression != "")
            {
                Parser ParserInstance = new Parser();

                List<Token> ExpressionToken = ParserInstance.Tokenize(Expression);
                List<Token> PostfixExpression = ParserInstance.ConvertToPostfix(ExpressionToken);
                double result = ParserInstance.EvaluatePostfix(PostfixExpression);

                TextResultBox.Text = result.ToString();
            }
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "0";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "2";
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "5";
        }
        private void btn6_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "9";
        }


        private void btnChangeSign_Click(object sender, EventArgs e)
        {
            string expression = TextExpressionBox.Text;
            if (expression[0] != '-')
                TextExpressionBox.Text = "-" + TextExpressionBox.Text;
            else
                TextExpressionBox.Text = expression.Substring(1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "+";
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "-";
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "*";
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "/";
        }

        private void btnSquareRoot_Click(object sender, EventArgs e)
        {

        }

        private void btnSquare_Click(object sender, EventArgs e)
        {

        }

        private void btnReceprocal_Click(object sender, EventArgs e)
        {

        }

        private void btmPercentage_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text += "%";
        }

        private void btnClearEntry_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text = "";
            TextResultBox.Text = "";
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            TextExpressionBox.Text.Remove(TextExpressionBox.Text.Length - 1);
        }
    }
}
