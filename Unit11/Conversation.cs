using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Unit11.Commands;
using Unit11.Model;

namespace Unit11
{
    public class Conversation
    {
        private Chat telegramChat;
        private List<Message> telegramMessages;

        public string Stage { get; set; }
        public IChatCommand Command { get; set; }

        public Dictionary<string, Word> Dictionary { get; set; }
        public Word Word { get; set; }
        public TrainingType TrainingType { get; set; }

        public Conversation(Chat chat)
        {
            telegramChat = chat;
            telegramMessages = new List<Message>();

            Dictionary = DefaultDataHelper.DictionaryInitializer(new Dictionary<string, Word>());
        }

        public void AddMessage(Message message)
        {
            telegramMessages.Add(message);
        }

        public string GetLastMessage() => telegramMessages[telegramMessages.Count - 1].Text;

        public List<string> GetTextMessages()
        {
            var textMessages = new List<string>();

            foreach (var message in telegramMessages)
            {
                if (message.Text != null)
                {
                    textMessages.Add(message.Text);
                }
            }

            return textMessages;
        }

        public long GetId() => telegramChat.Id;

        public string GetTrainingWord()
        {
            var rand = new Random();
            var item = rand.Next(0, Dictionary.Count);

            Word = Dictionary.Values.AsEnumerable().ElementAt(item);
            string text = "";

            switch (TrainingType)
            {
                case TrainingType.EngToRus:
                    text = Word.English;
                    break;

                case TrainingType.RusToEng:
                    text = Word.Russian;
                    break;
            }

            return text;
        }

        public bool CheckWord(string answer)
        {
            var result = false;

            switch (TrainingType)
            {
                case TrainingType.EngToRus:
                    result = Word.Russian == answer;
                    break;
                case TrainingType.RusToEng:
                    result = Word.English == answer;
                    break;
            }

            return result;
        }
    }
}
