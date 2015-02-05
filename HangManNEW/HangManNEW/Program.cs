using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangManNEW
{
    class Program
    {
        //GLOBAL VARIABLES
        static int GuessRemaining = 8;
        static int pauseDuration = 1000;
        static int cursorX = Console.WindowWidth;
        static int cursorY = Console.WindowHeight;

        static Random rng = new Random();
        static List<string> wordBank = new List<string>() { "puzzling", "trucking", "haphazard", "jawbreaker", "kazoo", "nowadays", "numbskull", "rhubarb", "jigsaw" };
        static string WordToGuess = string.Empty;

        static string LettersGuessed = string.Empty;
        static string WordFromLetters = string.Empty;

        static bool IsPlaying = true;



        static void Main(string[] args)
        {

            //Greeting();

        }

        static void Greeting()
        {
            Console.CursorVisible = false;
            string name = string.Empty;

            Thread.Sleep(pauseDuration * 2);

            Console.SetCursorPosition(cursorX / 4, cursorY / 2);
            OldTimeyTextPrinter("Hey man.  ", 50);
            Thread.Sleep(pauseDuration * 2);
            OldTimeyTextPrinter("So what's your name? ", 30);
            name = Console.ReadLine();
            Thread.Sleep(pauseDuration);

            Console.SetCursorPosition(cursorX / 4, cursorY / 2);
            OldTimeyTextPrinter("        Cool                  ", 50);
            Thread.Sleep(pauseDuration / 2);
            Console.SetCursorPosition((cursorX / 4) + 12, cursorY / 2);
            OldTimeyTextPrinter("  nice to meet you", 50);
            Thread.Sleep(pauseDuration * 2);

            Console.SetCursorPosition(cursorX / 4, cursorY / 2);
            OldTimeyTextPrinter("                                    ", 10);
            Console.SetCursorPosition(cursorX / 4, cursorY / 2);
            OldTimeyTextPrinter("   Have you ever heard of the game  ", 60);
            Thread.Sleep(pauseDuration / 2);
            OldTimeyTextPrinter(".  .  .  ", 200);

    //ANIMATION FUNCTION (keep it simple)
            //IntroAnimation();

            Thread.Sleep(pauseDuration * 3);

    //****ADD MORE EXPLANATION***


        }

        static void OldTimeyTextPrinter(string inputText, int pauseDuration)
        {
            //loop through each character
            for (int i = 0; i < inputText.Length; i++)
            {
                //get a letter
                char letter = inputText[i];
                Console.Write(letter);
                Thread.Sleep(pauseDuration);
            }
        }

        static void IntroAnimation()
        {
    //****NEED TO MAKE***
        }

        static void RunGame()
        {
            IsPlaying = true;
            WordToGuess = wordBank[rng.Next(wordBank.Count)];

            while (IsPlaying)
            {
                if (GuessRemaining > 0)
                {
                    if (WordGuessed(WordToGuess, WordFromLetters))
                    {
                        YouWon();
                    }
                    else
                    {
                        //RoundInfo(GuessRemaining, 
                        //get info
                    }
                }
                //run out of guesses
                else
                {
                    YouLose();
                }
            }

        }


        static bool WordGuessed(string wordToGuess_, string wordFromLetters_)
        {
            if (wordToGuess_.Replace(" ", string.Empty) == wordFromLetters_.Replace(" ", string.Empty))
            {
                return true;
            }
            return false;
        }

        private static void YouWon()
        {
            //Graphic(number);
            Console.WriteLine("Congrats");
            Console.WriteLine("do you want to play again?  y or n");
            string answer = Console.ReadLine();
            if (answer.ToLower() == "y")
            {
                LettersGuessed = string.Empty;
                RunGame();
            }
            IsPlaying = false;
        }

        private static void YouLose()
        {
            //Graphic(number);
            Console.WriteLine("better luck next time");
            Console.Write("do you want to play again?  y or n");
            string answer = Console.ReadLine();
            if (answer.ToLower() == "y")
            {
                LettersGuessed = string.Empty;
                RunGame();
            }
            IsPlaying = false;
        }




    //****HAVE TO MAKE GRAPHICS OF MAN HANGING****
        private static void Graphic(int switchNumber)
        {
    //****MAKE A SWITCH STATEMENT*****
            throw new NotImplementedException();
        }
    }
}
