using System.Threading.Tasks;
using Telegram.Bot;

namespace Unit11.Commands
{
    public class DeleteWordCommand : AbstractCommand<DeleteWordStages>
    {
        public DeleteWordCommand()
        {
            CommandText = "/deleteword";
        }

        public override async Task ExecuteAsync(ITelegramBotClient botClient, Conversation chat)
        {
            string text = "";

            switch (GetStageByName(chat.Stage))
            {
                case DeleteWordStages.Key:
                    text = "Введите слово, которое необходимо удалить";
                    break;
                case DeleteWordStages.Finish:
                    if (chat.Dictionary.Remove(chat.GetLastMessage()))
                    {
                        text = "Успешно! Слово " + chat.GetLastMessage() + " удалено из словаря. ";
                    }
                    else
                    {
                        text = "Ошибка! Слово " + chat.GetLastMessage() + " не найдено в словаре. ";
                    }
                    break;
                default:
                    break;
            }

            await botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text);
        }
    }
}
