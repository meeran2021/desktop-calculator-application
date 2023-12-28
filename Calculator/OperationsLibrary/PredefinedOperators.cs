using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperationsLibrary
{
    public class PredefinedOperators 
    {
        protected Dictionary<string, IOperations> OperatorDictionary = new Dictionary<string, IOperations>();

        public int AddNewOperator(string operatorSymbol, IOperations operatorName)
        {
            if (OperatorDictionary.ContainsKey(operatorSymbol))
            {
                return 0;
            }
            else
            {
                OperatorDictionary.Add(operatorSymbol, operatorName);
                return 1;
            }
        }


        // Method to load and initialize operators from JSON
        protected void InitializeOperatorDictionary()
        {
            string directoryPath = "E:/Visual Studio/Project/Calculator";
            Directory.SetCurrentDirectory(directoryPath);

            string jsonPath = "./OperationsLibrary/OperatorDatabase.json";


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
                //Console.WriteLine($"Error: JSON file not found - {ex.Message}");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (JsonException ex)
            {
                //Console.WriteLine($"Error: JSON parsing error - {ex.Message}");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error: An unexpected error occurred - {ex.Message}");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        public IOperations GetOperatorClass(string operatorSymbol)
        {
            if (OperatorDictionary.ContainsKey(operatorSymbol))
            {
                return OperatorDictionary[operatorSymbol];
            }
            else
            {
                throw new InvalidOperationException($"Operator '{operatorSymbol}' not found.");
            }
        }


        public int GetOperatorPrecedence(string operatorSymbol)
        {
            return GetOperatorClass(operatorSymbol) != null ? GetOperatorClass(operatorSymbol).OperatorPrecedence : throw new InvalidOperationException($"Operator '{operatorSymbol}' not found.");
        }


        public bool IsOperatorPrecedenceHigher(string operator1, string operator2)
        {
            return GetOperatorPrecedence(operator1) > GetOperatorPrecedence(operator2);
        }
    }
}
