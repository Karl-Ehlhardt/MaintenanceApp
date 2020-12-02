using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_Helpful_Methods
{
    public class Tools
    {
        //===============================================================================================================//
        //                                               COMMON TASKS                                                    //
        //===============================================================================================================// 
        //In order to stay as "DRY" as possible, I added this Class Library for methods that I wrote to use across all   // 
        //three of my Gold Badge Challenge Projects.                                                                     //  
        //===============================================================================================================//


        //***Write a blocked message bar***//
        public void MessageBar(string message, string border, string filler, int length)
        {
            string newMessage = "";
            string textBumper = "";
            string bar = "";

            if (length - message.Length % 2 == 0) { bar = string.Concat(Enumerable.Repeat(border, length)); }
            else { bar = string.Concat(Enumerable.Repeat(border, length + 1)); }

            int MessageStart = (bar.Length - message.Length) / 2;

            if (bar.Length % 2 == 0) { textBumper = string.Concat(Enumerable.Repeat(filler, bar.Length - message.Length / 2)); }
            else { textBumper = string.Concat(Enumerable.Repeat(filler, ((bar.Length - message.Length) / 2) - 1)); }

            newMessage = textBumper + message.ToUpper();
            textBumper = string.Concat(Enumerable.Repeat(filler, bar.Length - newMessage.Length));

            newMessage = newMessage + textBumper;
            Console.WriteLine(bar);
            Console.WriteLine(newMessage);
            Console.WriteLine(bar);
            Console.WriteLine();
        }


        //***Write a divider border***//
        public void RowDivider(string character, int length)
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat(character, length)));
        }


        //***Write in color without all that pesky code***//
        public void WriteColors(string message, string color)
        {
            string colorLower = color.ToLower();

            switch (colorLower)
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "gray":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteColors(string message, int color)
        {
            //1--Red
            //2--Green
            //3--Blue
            //4--Yellow
            //5--Gray
            //All others white

            switch (color)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        //**********************//
        //***TYPE CONVERSIONS***//
        //**********************//

        //***Converts a string to an int or returns 0***//
        public int StringToInt(string textnumber)
        {
            bool parsed = Int32.TryParse(textnumber, out int newNum);

            if (parsed == true) { return newNum; }
            else { return 0; }
        }

        //***Converts a string to double or returns 0***//
        public double StringToDouble(string textnumber)
        {
            bool parsed = Double.TryParse(textnumber, out double newNum);

            if (parsed == true) { return newNum; }
            else { return 0; }
        }

        //***Converts a string to decimal or returns 0***//
        public decimal StringToDecimal(string textnumber)
        {
            bool parsed = Decimal.TryParse(textnumber, out decimal newNum);

            if (parsed == true) { return newNum; }
            else { return 0; }
        }



        //***Writes a prompt and returns the answer***//
        public string GetStringResponse(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }


        //***Writes a prompt and returns the answer as an Int***//
        public int GetIntResponse(string prompt)
        {
            Console.WriteLine(prompt);
            string stringNum = Console.ReadLine();
            return StringToInt(stringNum);
        }


        //***Writes a prompt and returns the answer as an a double***//
        public double GetDoubleResponse(string prompt)
        {
            Console.WriteLine(prompt);
            string stringNum = Console.ReadLine();
            return StringToDouble(stringNum);
        }


        public decimal GetDecimalResponse(string prompt)
        {
            Console.WriteLine(prompt);
            string stringNum = Console.ReadLine();
            return StringToDecimal(stringNum);
        }


        //***Pads a string for table writing***//
        public void PadString(string info, int padding)
        {
            Console.Write(info.PadRight(padding));
        }


        //***Pads a double for table writing***//
        public void PadDouble(double info, int padding)
        {
            string stringNum = info.ToString();
            Console.Write(stringNum.PadRight(padding));
        }


        //***Pads an int for table writing***//
        public void PadInt(int info, int padding)
        {
            string stringNum = info.ToString();
            Console.Write(stringNum.PadRight(padding));
        }

        public void PadDecimal(decimal info, int padding)
        {
            string stringNum = info.ToString();
            Console.Write(stringNum.PadRight(padding));
        }


        //***Writes a menu number and line***//
        public void MenuLine(int number, string activity)
        {
            Console.WriteLine(number + ".) " + activity);
        }


        //***Waits for a key from user before moving***//
        public void KeyForward()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }


        public string StringTruncate(string content, int maxwide)
        {
            if (content.Length > maxwide - 3)
            {
                return content.Substring(0, maxwide - 5) + "...";
            }
            else { return content; }



        }


    }
}
