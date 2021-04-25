using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Unit11
{
    public class BotMessageLogic
    {
        private ITelegramBotClient botClient;
        private Messenger messanger;
        private Dictionary<long, Conversation> chatList;

        public BotMessageLogic(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
            messanger = new Messenger(botClient);
            chatList = new Dictionary<long, Conversation>();
        }

        public async Task ResponseAsync(MessageEventArgs e)
        {
            var id = e.Message.Chat.Id;

            if (!chatList.ContainsKey(id))
            {
                var newChat = new Conversation(e.Message.Chat);
                chatList.Add(id, newChat);
            }

            var chat = chatList[id];
            chat.AddMessage(e.Message);

            await messanger.MakeAnswerAsync(chat);
        }

        public async Task ResponseAsync(CallbackQueryEventArgs e)
        {
            var id = e.CallbackQuery.Message.Chat.Id;

            if (!this.chatList.ContainsKey(id))
            {
                var newChat = new Conversation(e.CallbackQuery.Message.Chat);
                this.chatList.Add(id, newChat);
            }

            var chat = this.chatList[id];
            chat.AddMessage(new Message() { Text = e.CallbackQuery.Data });

            await messanger.MakeAnswerAsync(chat);
        }
    }
}