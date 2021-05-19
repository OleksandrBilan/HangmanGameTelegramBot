using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopConsole
{
    class HangmanGame
    {
        private int gameStep;
        public bool GameResult { get; set; }
        public bool GameOver { get; set; }

        public Word WordToGuess { get; set; } = RandomWordPicker.GetInstance().Pick();
        public List<char> GuessedLetters { get; set; } = new List<char>();

        public HangmanGame() { }

        public string MaskedWord()
        {
            StringBuilder res = new StringBuilder();
            foreach (char c in WordToGuess.WordToGuess)
            {
                if (GuessedLetters.Contains(c))
                {
                    res.Append(c);
                }
                else
                {
                    res.Append('_');
                }
            }

            return res.ToString();
        }

        public bool MakeGuess(char letter)
        {
            if (GuessedLetters.Contains(letter))
            {
                if (++gameStep > 6)
                {
                    GameOver = true;
                    GameResult = false;
                    return false;
                }
            }

            if (WordToGuess.ToString().Contains(letter))
            {
                GuessedLetters.Add(letter);

                if (MaskedWord() == WordToGuess.ToString())
                {
                    GameOver = true;
                    GameResult = true;
                }

                return true;
            }
            else
            {
                if (++gameStep > 6)
                {
                    GameOver = true;
                    GameResult = false;
                    return false;
                }
            }

            return false;
        }

        public bool MakeGuess(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException();
            }

            GameOver = true;
            gameStep = 6;
            return GameResult = WordToGuess.ToString() == word;
        }

        public string CurrentHangedManPicture()
        {
            switch (gameStep)
            {
                case 0:
                    return "  ______\n" +
                            "        |\n" +
                            "        |\n" +
                            "        |\n" +
                            "        |\n" +
                            "_____|\n";

                case 1:
                    return "  ______\n" +
                            "  |      |\n" +
                            "  O     |\n" +
                            "         |\n" +
                            "         |\n" +
                            "_______|\n";

                case 2:
                    return "  ______\n" +
                            "  |     |\n" +
                            "  O     |\n" +
                            "  |      |\n" +
                            "          |\n" +
                            "______|\n";

                case 3:
                    return "  ______\n" +
                            "  |     |\n" +
                            "  O     |\n" +
                            " /|       |\n" +
                            "           |\n" +
                            "______|\n";

                case 4:
                    return "  ______\n" +
                            "  |     |\n" +
                            "  O     |\n" +
                            " /|\\    |\n" +
                            "          |\n" +
                             "______|\n";

                case 5:
                    return "  ______\n" +
                            "  |     |\n" +
                            "  O     |\n" +
                            " /|\\    |\n" +
                            " /        |\n" +
                            "______|\n";

                case 6:
                    return "  ______\n" +
                            "  |     |\n" +
                            "  O     |\n" +
                            " /|\\    |\n" +
                            " / \\     |\n" +
                            "______|\n";

                default:
                    return "problem with gameStage";
            }
        }
    }
}
