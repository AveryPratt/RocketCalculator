using KSP_Library.Rocketry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        public static class ConversionErrors
        {
            public static double ConvertPayloadToInt(string value, out string errorMessage)
            {
                double payloadMass;
                bool isValueDouble = double.TryParse(value, out payloadMass);
                if (!isValueDouble && value != string.Empty)
                {
                    errorMessage = "<error> Payload Mass must be an integer or decimal to be recognized. </error><br/>";
                }
                else errorMessage = null;
                return payloadMass;
            }

            public static int IntConversion(string value, int rowNumber, int cellNumber, out string errorMessage)
            {
                int result;
                bool isValueInt = int.TryParse(value, out result);
                if (!isValueInt)
                {
                    errorMessage = "<error> Stage " + (rowNumber + 1).ToString() + "'s " + Enum.GetName(typeof(InputValue), cellNumber) + " must be an integer. </error><br/>";
                }
                else errorMessage = null;
                return result;
            }
            public static double DoubleConversion(string value, int rowNumber, int cellNumber, out string errorMessage)
            {
                double result;
                bool isValueDouble = double.TryParse(value, out result);
                if (!isValueDouble)
                {
                    errorMessage = "<error> Stage " + (rowNumber + 1).ToString() + "'s " + Enum.GetName(typeof(InputValue), cellNumber) + " must be an integer or a decimal. </error><br/>";
                }
                else errorMessage = null;
                return result;
            }
        }
    }
}