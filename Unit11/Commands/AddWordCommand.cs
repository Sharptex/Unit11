using System.Threading.Tasks;
using Telegram.Bot;
using Unit11.Model;

namespace Unit11.Commands
{
    public class AddWordCommand : AbstractCommand<AddWordStages>
    {
        public AddWordCommand()
        {
            CommandText = "/addword";
        }

        public override async Task ExecuteAsync(ITelegramBotClient botClient, Conversation chat)
        {
            string text = "";

            switch (GetStageByName(chat.Stage))
            {
                case AddWordStages.Rus:
                    text = "Введите русское значение слова";
                    chat.Word = new Word();
                    break;
                case AddWordStages.Eng:
                    chat.Word.Russian = chat.GetLastMessage();
                    text = "Введите английское значение слова";
                    break;
                case AddWordStages.Theme:
                    chat.Word.English = chat.GetLastMessage();
                    text = "Введите тематику";
                    break;
                case AddWordStages.Finish:
                    chat.Word.Theme = chat.GetLastMessage();
                    chat.Dictionary.Add(chat.Word.Russian, chat.Word);
                    text = "Успешно! Слово " + chat.Word.English + " добавлено в словарь. ";
                    break;
                default:
                    break;
            }

            await botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text);
        }
    }
}
