using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Unit11
{
    public class BotWorker
    {
        private ITelegramBotClient botClient;
        private BotMessageLogic logic;

        public void Inizalize()
        {
            botClient = new TelegramBotClient(BotCredentials.BotToken);
            logic = new BotMessageLogic(botClient);
        }

        private async void Bot_OnMessageAsync(object sender, MessageEventArgs e)
        {
            if (e.Message != null)
            {
                await logic.ResponseAsync(e);
            }
        }

        private async void Bot_Callback(object sender, CallbackQueryEventArgs e)
        {
            if (e.CallbackQuery.Data != null)
            {
                await logic.ResponseAsync(e);
            }

            await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
        }

        public void Start()
        {
            botClient.OnMessage += Bot_OnMessageAsync;
            botClient.OnCallbackQuery += Bot_Callback;
            botClient.StartReceiving();
        }

        public void Stop()
        {
            botClient.StopReceiving();
        }
    }
}
