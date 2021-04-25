using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit11.Commands;
using Unit11.Model;
using System.Collections;

namespace Unit11
{
    public static class DefaultDataHelper
    {
        private static Dictionary<string, Word> defaultDictionary = new Dictionary<string, Word>()
        {
            ["слово"] = new Word() { Russian = "слово", English = "word" },
            ["стол"] = new Word() { Russian = "стол", English = "table" },
            ["ручка"] = new Word() { Russian = "ручка", English = "pen" },
            ["ананас"] = new Word() { Russian = "ананас", English = "Pineapple" },
            ["яблоко"] = new Word() { Russian = "яблоко", English = "apple" }
        };

        public static readonly string wrongCommandMessage = "Wrong command format, try again!";

        public static readonly List<IChatCommand> defaultCommands = new List<IChatCommand>() { new AddWordCommand(), new DeleteWordCommand(), new ShowDictionaryCommand(), new TrainingCommand(), new StopTrainingCommand() };

        public static Dictionary<string, Word> DictionaryInitializer(Dictionary<string, Word> dictionary)
        {
            foreach (var item in defaultDictionary)
            {
                dictionary.Add(item.Key, item.Value);
            }

            return dictionary;
        }
    }
}
