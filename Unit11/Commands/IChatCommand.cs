using System.Threading.Tasks;
using Telegram.Bot;

namespace Unit11.Commands
{
    public interface IChatCommand
    {
        string[] Stages { get; }

        bool CheckMessage(string message);

        Task ExecuteAsync(ITelegramBotClient botClient, Conversation chat);

        string NextStage(string staget);
    }
}
