using System;
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
                    case Commands.Default:
                    default:
                        Console.WriteLine("Wrong command! Try again...");
                        break;
                }
            }
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