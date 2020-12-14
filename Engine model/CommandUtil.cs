using System.Collections.Generic;
using System.Linq;

namespace Engine_model
{
    public enum Commands
    {
        TestCurrentEngine,
        TestCustomEngine,
        Default,
    }

    public static class CommandUtil
    {
        public static readonly Dictionary<Commands, string> CommandKeys = new Dictionary<Commands, string>()
        {
            {Commands.TestCurrentEngine, "test"},
            {Commands.TestCustomEngine, "CustomTest"},
        };

        public static Commands GetCommandByKey(string dataValue)
        {
            return CommandKeys.All(item => item.Value != dataValue)
                ? Commands.Default
                : CommandKeys.FirstOrDefault(item => item.Value == dataValue).Key;
        }

        public static string CommandsText => "\n*************\n" + CommandUtil.CommandKeys.Aggregate("All commands",
            (current, pair) => current + ("\n" + pair.Key + " -> \"" + pair.Value + "\"")) + "\n";
    }
}