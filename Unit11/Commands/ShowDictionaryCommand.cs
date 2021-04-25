using System.Threading.Tasks;
using Telegram.Bot;

namespace Unit11.Commands
{
    public class ShowDictionaryCommand : AbstractCommand<DefaultStages>
    {
        public ShowDictionaryCommand()
        {
            CommandText = "/dictionary";
        }

        public override async Task ExecuteAsync(ITelegramBotClient botClient, Conversation chat)
        {
            string text = "";

            switch (GetStageByName(chat.Stage))
            {
                case DefaultStages.Finish:
                    text = string.Join("\n", chat.Dictionary.Values);
                    break;
                default:
                    break;
            }

            await botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text);
        }
    }
}
