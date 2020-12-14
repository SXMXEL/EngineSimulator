using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Engine_model
{
    static class Controller
    {
        private static void Main()
        {
            var engine = new InternalCombustionEngine(
                0,
                0,
                0.1,
                110,
                0.01,
                0.0001,
                0.1,
                new int[] {20, 75, 100, 105, 75, 0},
                new int[] {0, 75, 150, 200, 250, 300});
            var testStand = new TestStand(engine);
            while (true)
            {
                Console.WriteLine(CommandUtil.CommandsText);

                Console.Write("Please, enter your command: ");
                switch (CommandUtil.GetCommandByKey(Console.ReadLine()))
                {
                    case Commands.TestCurrentEngine:
                        TestCurrentStand(testStand);
                        break;
                    case Commands.TestCustomEngine:
                        var m = 0;
                        var v = 0;
                        Console.Write("Please enter engine moment of inertia \"I\": ");
                        var i = double.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                        Console.Write("Please enter engine overheat temperature \"Toverheat\": ");
                        var overheatTemperature =
                            double.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                        Console.Write(
                            "Please enter the coefficient of the dependence of the heating rate on the torque \"Hm\": ");
                        var hm = double.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                        Console.Write(
                            "Please enter the coefficient of the dependence of the heating rate" +
                            " on the crankshaft rotation speed \"Hv\": ");
                        var hv = double.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                        Console.Write("Please enter Coefficient of dependence of cooling rate" +
                                      " on engine temperature and ambient temperature \"C\": ");
                        var c = double.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                        Console.Write("Please enter M array like this \" 20, 75, 100, 105, 75, 0\" : ");
                        var startM = ToIntArray(Console.ReadLine(),',');
                        Console.Write("Please enter V array like this \" 0, 75, 150, 200, 250, 300\" : ");
                        var startV = ToIntArray(Console.ReadLine(),',');
                        
                        var customEngine = new InternalCombustionEngine(m, v, i, overheatTemperature,
                            hm, hv, c, startM, startV);
                        var customTestStand = new TestStand(customEngine);
                        TestCurrentStand(customTestStand);
                        break;
                    case Commands.Default:
                    default:
                        Console.WriteLine("Wrong command! Try again...");
                        break;
                }
            }
        }

        private static int[] ToIntArray(string value, char sep)
        {
            var input = value.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            var startArr = new int[input.Length];
            for (var i = 0; i < startArr.Length; ++i)
            {
                var s = input[i];
                if (int.TryParse(s, out var j))
                {
                    startArr[i] = j;
                }
            }
            return startArr;
        }

        private static void TestCurrentStand(TestStand testStand)
        {
            var regex = new Regex(@"^\d{1,10}$");
            Console.Write("Please, enter ambient temperature: ");
            var input = Console.ReadLine();
            if (!regex.IsMatch(input ?? throw new InvalidOperationException()))
            {
                Console.WriteLine("Incorrect input! ");
                return;
            }

            testStand.SetTemperatureData(int.Parse(input));
            var data = testStand.GetTestData();
            Console.WriteLine(data);
        }
    }
}