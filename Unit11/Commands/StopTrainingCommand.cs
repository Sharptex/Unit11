using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Unit11.Commands
{
    public class StopTrainingCommand : AbstractCommand<DefaultStages>
    {
        public StopTrainingCommand()
        {
            CommandText = "/stop";
        }

        public override async Task ExecuteAsync(ITelegramBotClient botClient, Conversation chat)
        {
            string text = "";

            switch (GetStageByName(chat.Stage))
            {
                case DefaultStages.Finish:
                    text = "Тренировка окончена";
                    break;
                default:
                    break;
            }

            await botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text);
        }
    }
}
