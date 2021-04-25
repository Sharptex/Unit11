using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Unit11.Commands
{
    public class TrainingCommand : AbstractCommand<TrainingStages>
    {
        public TrainingCommand()
        {
            CommandText = "/training";
        }

        public override async Task ExecuteAsync(ITelegramBotClient botClient, Conversation chat)
        {
            string text = "";

            switch (GetStageByName(chat.Stage))
            {
                case TrainingStages.TrainingType:
                    text = "Выберите тип тренировки.\nДля окончания тренировки введите команду /stop";
                    await botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text, replyMarkup: ReturnKeyBoard());
                    return;
                case TrainingStages.Question:
                    if (chat.GetLastMessage() == "1")
                    {
                        chat.TrainingType = TrainingType.RusToEng;
                    }
                    else if (chat.GetLastMessage() == "2")
                    {
                        chat.TrainingType = TrainingType.EngToRus;
                    }

                    text = chat.GetTrainingWord();
                    break;
                case TrainingStages.Result:
                    if (chat.CheckWord(chat.GetLastMessage()))
                    {

                        text = "Верно! Следующее слово - " + chat.GetTrainingWord();
                    }
                    else
                    {
                        text = "Ошибка! Следующее слово - " + chat.GetTrainingWord();
                    }
                    break;
                case TrainingStages.Finish:
                    text = "Тренировка окончена";
                    break;
                default:
                    break;
            }

            await botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text);
        }

        public InlineKeyboardMarkup ReturnKeyBoard()
        {
            var buttonList = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton
                {
                    Text = "С русского на английский",
                    CallbackData = "1"
                },

                new InlineKeyboardButton
                {
                    Text = "С английского на русский",
                    CallbackData = "2"
                }
            };

            var keyboard = new InlineKeyboardMarkup(buttonList);

            return keyboard;
        }

        public override string NextStage(string stage)
        {
            if (GetStageByName(stage) != TrainingStages.Result)
            {
                int index = Array.IndexOf(Stages, stage) + 1;

                if (index >= 0 && index < Stages.Count())
                {
                    return Stages[index];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return TrainingStages.Result.ToString();
            }
        }
    }
}
