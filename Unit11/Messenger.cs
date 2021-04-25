using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Unit11.Commands;

namespace Unit11
{
    public class Messenger
    {
        private CommandParser parser;
        private ITelegramBotClient botClient;

        public Messenger(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
            parser = new CommandParser(DefaultDataHelper.defaultCommands);
        }

        public async Task MakeAnswerAsync(Conversation chat)
        {
            string message = chat.GetLastMessage();

            if (parser.IsTextCommand(message) && (chat.Command == null || parser.GetCommand(message) is StopTrainingCommand))
            {
                var command = parser.GetCommand(message);
                chat.Stage = command.NextStage(chat.Stage);
                await command.ExecuteAsync(botClient, chat);

                if (command.NextStage(chat.Stage) != null) 
                { 
                    chat.Command = command; 
                    return; 
                } 
                else
                {
                    chat.Command = null;
                    chat.Stage = null;
                }
            }

            if (chat.Command != null && chat.Stage != null)
            {
                chat.Stage = chat.Command.NextStage(chat.Stage);
                await chat.Command.ExecuteAsync(botClient, chat);

                int lastStageIndex = chat.Command.Stages.Length - 1;

                if (chat.Command.Stages[lastStageIndex] == chat.Stage)
                {
                    chat.Command = null;
                    chat.Stage = null;
                }
            }
        }
    }
}