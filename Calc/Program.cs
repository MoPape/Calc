using System;
using System.Text.RegularExpressions;
using Rationale;

// Ein einfacher Taschenrechner der 2 Zahlen miteinander verrechnet.
namespace Calc
{
    class Rechner
    {
        static void Main(string[] args)
        {
            ShowHelp();
            GetInput();
        }

        // Fordert die Eingabe eines befehls des Users
        // Prüft auf falsche Eingaben.
        // Akzeptiert Eingaben im Format "-" + "Buchstabe"
        public static void GetInput()
        {
            Console.WriteLine("");
            string userInput = Console.ReadLine();
            string patternCommand = @"-[a-z]";    // Dash(Bindestrich) gefolgt von einem Char(a-z, case sensitive)
            Match m = Regex.Match(userInput, patternCommand);
            if (m.Success)
            {
                Options(userInput);
                return;
            }
            else
                return;
        }
        // Forder eine Eingabe vom Nutzer.
        // Zuässige Zeichen beinhalten + - * /
        // Prüft auf Korrektheit der Eingabe.
        /// <summary>
        /// jhgjhhg
        /// </summary>
        /// <returns></returns>
        public static char GetOperator()
        {
            Console.WriteLine("Type an operator");
            string userInput = Console.ReadLine();
            string patternOperator = @"[+\-*\/]";    // Eins der Zeichen.
            Match m = Regex.Match(userInput, patternOperator);
            if (m.Success)
            {
                char h = userInput[0];
                return h;
            }
            Console.WriteLine("Try again. Valid operators are: + - * / ");
            return GetOperator();
        }
        public static Rational GetRational()
        {
            long tmpNum = 0;
            long tmpDen = 0;

            Console.WriteLine("Type an Numerator and Denominator seperated by space.");
            string userInput = Console.ReadLine();
            int spaceIndex = userInput.IndexOf(" ");
            string sub1 = userInput.Substring(0, spaceIndex);
            string sub2 = userInput.Substring(spaceIndex+1,(userInput.Length-spaceIndex-1));

            //userInput = userInput.Replace('.', ','); // "." in "," ändern (Internationalisierung)
            var isSuccess = long.TryParse(sub1, out long result1);
            if (isSuccess)
                tmpNum = result1;
            else
            {
                Console.WriteLine("Try again. Please enter Digits only! (Decimals not allowed!)");
                return GetRational();
            }
            isSuccess = long.TryParse(sub2, out long result2);
            if (isSuccess)
                tmpDen = result2;
            else
            {
                Console.WriteLine("Try again. Please enter Digits only! (Decimals not allowed!)");
                return GetRational();
            }
            return new Rational(tmpNum,tmpDen);
        }
        public static decimal GetNumber()
        {
            Console.WriteLine("Type a number");
            string userInput = Console.ReadLine();
            userInput=userInput.Replace('.', ','); // "." in "," ändern (Internationalisierung)
            var isSuccess = decimal.TryParse(userInput,out decimal result);
            if (isSuccess)
                return result;
            else
            {
                Console.WriteLine("Try again. Please enter Digits only! (Use commata for decimals)");
                return GetNumber();
            }

            // RegEx ist wie gewalt. Funktioniert es nicht nutzt du nicht genug davon!

            //string patternDigit = @"(?:-)?(?:\d+[^,])|(?:-?\d+,\d+)"; // Entweder eine Ganzzahl beliebeiger länge oder zwei Ganzzahlen jeweils beliebiger länge durch Kommata getrennt.
            ////(-?\d+)|(-?\d+),(-?\d+)                
            //Match m = Regex.Match(userInput, patternDigit);
            //if (m.Success)
            //    return Convert.ToDecimal(userInput);
            //else
            //{
            //    Console.WriteLine("Try again. Please enter Digits only! (Use commata for decimals");
            //    return getNumber();
            //}
            // return result;
        }
        // Ruft getNumber() und getOperator() auf um eine Gleichung zu erstellen.
        // Die Lösung der Gleichung wird auf der Konsole ausgegeben.
        // Ruft loop() wieder auf.
        public static void Calc()
        {
            decimal a = GetNumber();
            char op = GetOperator();
            decimal b = GetNumber();


            switch (op)
            {
                case '+':
                    Console.WriteLine("{0} {1} {2} = " + (a + b), a, op, b);
                    break;
                case '-':
                    Console.WriteLine("{0} {1} {2} = " + (a - b), a, op, b);
                    break;
                case '*':
                    Console.WriteLine("{0} {1} {2} = " + (a * b), a, op, b);
                    break;
                case '/':
                    if (b == 0)
                        Console.WriteLine("You tryna to break the universe here.");
                    else
                        Console.WriteLine("{0} {1} {2} = " + (a / b), a, op, b);
                    break;



            }
            GetInput();
        }
        public static void CalcRational()
        {
            Rational a = GetRational();
            char op = GetOperator();
            Rational b = GetRational();


            switch (op)
            {
                case '+':
                    Console.WriteLine("{0} {1} {2} = " + (a.RationalAdd(b)), a, op, b);
                    break;
                case '-':
                    Console.WriteLine("{0} {1} {2} = " + (a.RationalSub(b)), a, op, b);
                    break;
                case '*':
                    Console.WriteLine("{0} {1} {2} = " + (a.RationalMult(b)), a, op, b);
                    break;
                case '/':
                    Console.WriteLine("{0} {1} {2} = " + (a.RationalDiv(b)), a, op, b);
                    break;



            }
            GetInput();
        }
        // Zeigt die möglichen Befehle auf der Konsole an.
        // Ruft loop() wieder auf
        public static void ShowHelp()
        {
            Console.Write("Usage:  \n" +
                          "-h \t Displays this message. \n" +
                          "-q \t Quits the program. \n" +
                          "-s \t Start a calculation. \n" +
                          "-r \t Start a calculation using rationals. \n");
            GetInput();
        }
        // Führt Funktionen basierend auf der Eingabe des nutzers aus
        public static void Options(string str)
        {
            switch (str)
            {
                case "-h":
                    ShowHelp();
                    break;
                case "-q":
                    System.Environment.Exit(1);
                    break;
                case "-r":
                    CalcRational();
                    break;
                case "-s":
                    Calc();
                    break;



            }

        }
    }
}