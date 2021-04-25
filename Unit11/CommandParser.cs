using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit11.Commands;

namespace Unit11
{
    public class CommandParser
    {
        private List<IChatCommand> Command;

        public CommandParser(List<IChatCommand> defaultCommands)
        {
            Command = new List<IChatCommand>();

            Command.AddRange(DefaultDataHelper.defaultCommands);
        }

        public bool IsTextCommand(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message));

            return command is IChatCommand;
        }

        public IChatCommand GetCommand(string message)
        {
            return Command.Find(x => x.CheckMessage(message));
        }
    }
}
