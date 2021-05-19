using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopConsole
{
    class RandomWordPicker
    {
        private static readonly RandomWordPicker instance = new RandomWordPicker();

        private Random random = new Random();

        private List<Word> words = new List<Word>
        {
            new Word("apple", "a fruit"), new Word("car", "doesn't need hints)"), new Word("coffee", "a drink"),
            new Word("university", "a building"), new Word("program", "something in programming")
        };

        private RandomWordPicker() { }

        public static RandomWordPicker GetInstance() => instance;

        public Word Pick()
        {
            return words[random.Next(0, words.Count)];
        }
    }
}
