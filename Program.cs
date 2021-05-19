using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace WorkshopConsole
{
    class Program
    {
        static ITelegramBotClient botClient;

        static HangmanGame currentGame;

        static bool gameStarted;

        static void Main()
        {
            botClient = new TelegramBotClient("1789504086:AAEf_uS6bhIaMVe958NgIn3UEOMcKa8sz_A");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            botClient.StopReceiving();
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received '{e.Message.Text}' in chat {e.Message.Chat.Id}.");

                string msg = e.Message.Text;

                if (msg.StartsWith("-new") || msg.StartsWith("-play"))
                {
                    currentGame = new HangmanGame();
                    gameStarted = true;

                    await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: "Ok! Lets go!\nHere is the description of the word:\n"
                            + currentGame.WordToGuess.Description + "\nsize of the word: "
                            + currentGame.WordToGuess.ToString().Length
                    );

                    await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: currentGame.CurrentHangedManPicture()
                    );
                }

                if (msg.StartsWith("-l") || msg.StartsWith("-letter"))
                {
                    if (gameStarted)
                    {
                        bool res = currentGame.MakeGuess((msg.Split(' '))[1][0]);

                        if (currentGame.GameOver)
                        {
                            if (currentGame.GameResult == true)
                            {
                                await botClient.SendTextMessageAsync(
                                  chatId: e.Message.Chat,
                                  text: "You have won! Congrats!"
                                );
                            }
                            else
                            {
                                await botClient.SendTextMessageAsync(
                                  chatId: e.Message.Chat,
                                  text: "You have lost..."
                                );
                            }
                        }
                        else
                        {
                            if (res == true)
                            {
                                await botClient.SendTextMessageAsync(
                                  chatId: e.Message.Chat,
                                  text: "Nice: " + currentGame.MaskedWord() + "\n"
                                        + currentGame.CurrentHangedManPicture()
                                );
                            }
                            else
                            {
                                await botClient.SendTextMessageAsync(
                                  chatId: e.Message.Chat,
                                  text: "No: " + currentGame.MaskedWord() + "\n"
                                        + currentGame.CurrentHangedManPicture()
                                );
                            }
                        }
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "Unknown command"
                        );

                        if (currentGame.GameResult == true)
                        {
                            await botClient.SendTextMessageAsync(
                              chatId: e.Message.Chat,
                              text: "You have won! Congrats!"
                            );
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(
                              chatId: e.Message.Chat,
                              text: "You have lost..."
                            );
                        }
                    }
                }
                
                if (msg.StartsWith("-w") || msg.StartsWith("-word"))
                {
                    bool res = currentGame.MakeGuess((msg.Split(' '))[1]);

                    if (currentGame.GameResult == true)
                    {
                        await botClient.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: "You have won! Congrats!"
                        );
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: "You have lost..."
                        );
                    }
                }

                if (msg.StartsWith("-help"))
                {
                    await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: "Available commands:\n" +
                            "-new | -play -- start a new game\n" +
                            "-l | -letter [letter] –- to guess a letter\n" +
                            "-w | -word [word] –- to guess a whole word\n"
                    );
                }
            }
        }
    }
}
