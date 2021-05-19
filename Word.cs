using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopConsole
{
    class Word
    {
        public string WordToGuess { get; set; } = "undefined";
        public string Description { get; set; } = "undefined";

        public Word() { }

        public Word(string word, string description)
        {
            if (word == null || description == null)
            {
                throw new ArgumentNullException();
            }

            WordToGuess = word;
            Description = description;
        }

        public override string ToString() => WordToGuess;
    }
}
