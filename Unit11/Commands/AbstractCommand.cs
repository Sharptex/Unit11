using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Unit11.Commands
{
    public abstract class AbstractCommand<T> : IChatCommand where T : struct
    {
        public string CommandText;

        public string[] Stages 
        { 
            get
            {
                return Enum.GetNames(typeof(T));
            } 
        }

        public bool CheckMessage(string message)
        {
            return CommandText == message;
        }

        abstract public Task ExecuteAsync(ITelegramBotClient botClient, Conversation chat);

        public virtual string NextStage(string stage)
        {
            int index = Array.IndexOf(Stages, stage) + 1;

            if (index >= 0 && index < Stages.Count())
            {
                return Stages[index];
            }

            return null;
        }

        public T GetStageByName(string stage)
        {
            Enum.TryParse(stage, true, out T resultInputType);
            return resultInputType;
        }
    }
}
