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
        static int GuessesGiven = 7;
        static int GuessRemaining = 0;
        static int PauseDuration = 1000;
        static int CursorX = Console.WindowWidth;
        static int CursorY = Console.WindowHeight;

        static bool IsPlaying = true;
        static Random rng = new Random();
        static List<string> wordBank = new List<string>() { "puzzling", "trucking", "haphazard", "jawbreaker", "kazoo", "nowadays", "numbskull", "rhubarb", "jigsaw" };
        static string WordToGuess = string.Empty;

        static string LettersGuessed = string.Empty;
        static string MaskedWord = string.Empty;
        static string CurrentLetterGuess = string.Empty;


        static void Main(string[] args)
        {
            Console.SetWindowSize(70, 35);
            Greeting();
            IntroAnimation();
            RunGame();
        }

        static void OldTimeyTextPrinter(string inputText, int pause)
        {
            //loop through each character
            for (int i = 0; i < inputText.Length; i++)
            {
                //get a letter
                char letter = inputText[i];
                Console.Write(letter);
                Thread.Sleep(pause);
            }
        }

        static void Greeting()
        {
            Console.CursorVisible = false;
            string name = string.Empty;

            Thread.Sleep(PauseDuration);
            Console.SetCursorPosition(CursorX / 5, CursorY / 2);
            OldTimeyTextPrinter("Hey kid.  ", 50);
            Thread.Sleep(PauseDuration * 2);
            OldTimeyTextPrinter("So what's your name? ", 30);
            name = Console.ReadLine();
            Thread.Sleep(PauseDuration);

            Console.SetCursorPosition(CursorX / 5, CursorY / 2);
            OldTimeyTextPrinter("        Cool                  ", 50);
            Thread.Sleep(PauseDuration / 4);
            Console.SetCursorPosition((CursorX / 5) + 12, CursorY / 2);
            OldTimeyTextPrinter("  nice to meet you", 50);
            Thread.Sleep(PauseDuration * 2);

            Console.SetCursorPosition(CursorX / 5, CursorY / 2);
            OldTimeyTextPrinter("                                                 ", 10);
            Console.SetCursorPosition(CursorX / 5, CursorY / 2);
            OldTimeyTextPrinter("   Have you ever heard of the game  ", 60);
            Thread.Sleep(PauseDuration / 2);
            OldTimeyTextPrinter(".  .  .  ", 200);
        }
       
        static void IntroAnimation()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.SetCursorPosition(0, 8);
            string cat = @"
 ██░ ██  ▄▄▄       ███▄    █   ▄████     ▄████▄   ▄▄▄     ▄▄▄█████▓
▓██░ ██▒▒████▄     ██ ▀█   █  ██▒ ▀█▒   ▒██▀ ▀█  ▒████▄   ▓  ██▒ ▓▒
▒██▀▀██░▒██  ▀█▄  ▓██  ▀█ ██▒▒██░▄▄▄░   ▒▓█    ▄ ▒██  ▀█▄ ▒ ▓██░ ▒░
░▓█ ░██ ░██▄▄▄▄██ ▓██▒  ▐▌██▒░▓█  ██▓   ▒▓▓▄ ▄██▒░██▄▄▄▄██░ ▓██▓ ░ 
░▓█▒░██▓ ▓█   ▓██▒▒██░   ▓██░░▒▓███▀▒   ▒ ▓███▀ ░ ▓█   ▓██▒ ▒██▒ ░ 
 ▒ ░░▒░▒ ▒▒   ▓▒█░░ ▒░   ▒ ▒  ░▒   ▒    ░ ░▒ ▒  ░ ▒▒   ▓▒█░ ▒ ░░   
 ▒ ░▒░ ░  ▒   ▒▒ ░░ ░░   ░ ▒░  ░   ░      ░  ▒     ▒   ▒▒ ░   ░    
 ░  ░░ ░  ░   ▒      ░   ░ ░ ░ ░   ░    ░          ░   ▒    ░      
 ░  ░  ░      ░  ░         ░       ░    ░ ░            ░  ░        
                                        ░                          
            ";
            OldTimeyTextPrinter(cat, 3);
            Thread.Sleep(PauseDuration);
            Console.WriteLine();
            Console.WriteLine();
            OldTimeyTextPrinter("     I'm going to pick a random word...", 40);
            Thread.Sleep(PauseDuration / 2);
            Console.WriteLine();
            OldTimeyTextPrinter("          Win the game by guessing correct letters", 40);
            Thread.Sleep(PauseDuration * 2);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("           OR THE CAT GETS IT!!!");
            Console.WriteLine();
            Thread.Sleep(PauseDuration * 3);
        }


        static void RunGame()
        {
            WordToGuess = wordBank[rng.Next(wordBank.Count)].ToUpper();
            GuessRemaining = GuessesGiven;
            LettersGuessed = string.Empty;
            IsPlaying = true;

            while (IsPlaying)
            {
                Console.Clear();

         //**** FOR TESTING ****
                Console.WriteLine(WordToGuess);
                //Console.WriteLine();
         //**** FOR TESTING ****

                BuildMaskedWord();
                PrintRoundInfo();
                Console.Write("Please enter a letter:");
                CurrentLetterGuess = Console.ReadLine();
                if (CurrentLetterGuess.ToUpper() == WordToGuess.ToUpper())
                {
                    IsPlaying = false;
                }
                if (ValidateLetter(CurrentLetterGuess))
                {
                    IsLetterInWord(CurrentLetterGuess);
                }
                //BuildMaskedWord() here a second time b/c of a bug i couldn't figure out in time...ater all letters are guessed (one at a time) you don't immediately win.  need to enter a letter again, then it registers.  will fix later.
                BuildMaskedWord();
                DidYouWin();

            }
        }

        static void DidYouWin()
        {
            if (WordGuessed(WordToGuess, MaskedWord) || (CurrentLetterGuess.ToUpper() == WordToGuess.ToUpper()))
            {
                YouWon();
            }
            else if (GuessRemaining == 0)
            {
                YouLose();
            }
        }

        static bool WordGuessed(string wordToGuess_, string maskedWord_)
        {
            if (wordToGuess_.ToUpper() == maskedWord_.ToUpper().Replace(" ", string.Empty))
            {
                return true;
            }
            return false;
        }

        static bool ValidateLetter(string letter)
        {
            if (letter.Length > 1)
            {
                return false;
            }
            else if (letter.Length != 1)
            {
                Console.WriteLine();
                OldTimeyTextPrinter("INVALID INPUT...ENTER A SINGLE LETTER", 20);
                Thread.Sleep(PauseDuration*2);
                return false;
            }
            else if (LettersGuessed.ToUpper().Contains(letter.ToUpper()))
            {
                Console.WriteLine();
                OldTimeyTextPrinter("YOU ALREADY TRIED THIS LETTER", 20);
                Thread.Sleep(PauseDuration*2);
                return false;
            }
            else if (char.IsLetter(letter[0]))
            {
                return true;
            }
            return false;
        }

        static void IsLetterInWord(string letter)
        {
            if (WordToGuess.Contains(letter.ToUpper()))
            {
                LettersGuessed += letter.ToUpper();
                Console.WriteLine();
                OldTimeyTextPrinter("CORRECT!!  GOOD CHOICE!", 20);
                Thread.Sleep(PauseDuration);
            }
            else
            {
                LettersGuessed += letter.ToUpper();
                Console.WriteLine();
                OldTimeyTextPrinter("NOPE!!  NOT THIS LETTER!", 20);
                Thread.Sleep(PauseDuration);
                GuessRemaining--;
            }

        }

        static void BuildMaskedWord()
        {
            MaskedWord = string.Empty;
            for (int i = 0; i < WordToGuess.Length; i++)
            {
                if (LettersGuessed.Contains(WordToGuess[i].ToString()))
                {
                    MaskedWord += WordToGuess[i] + " ";
                }
                else
                {
                    MaskedWord += "_ ";
                }
            }
        }

        static void PrintRoundInfo()
        {
            Graphic(GuessesGiven - GuessRemaining);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(MaskedWord);
            Console.WriteLine();
            Console.Write("GUESSES REMAINING: " + GuessRemaining);
            Console.WriteLine("     LETTERS GUESSED: " + LettersGuessed);
            Console.WriteLine();
            Console.WriteLine();
        }

        static void YouWon()
        {
            IsPlaying = false;
            Console.Clear();
            Graphic(8);
            Console.WriteLine();
            Console.WriteLine();
            OldTimeyTextPrinter("THANKS BUD!", 60);
            Console.WriteLine();
            Thread.Sleep(PauseDuration);
            OldTimeyTextPrinter("I'LL SEE YA AROUND", 60);
            Console.WriteLine();
            Console.WriteLine();
            Thread.Sleep(PauseDuration * 2);
            OldTimeyTextPrinter("PLAY AGAIN?   Y or N", 60);
            Console.WriteLine();
            if (Console.ReadLine().ToUpper() == "Y")
            {
                RunGame();
            }
            IsPlaying = false;
        }

        static void YouLose()
        {
            IsPlaying = false;
            Console.Clear();
            Graphic(7);
            Console.WriteLine();
            Console.WriteLine();
            OldTimeyTextPrinter("THAT'S PRETTY MESSED UP", 60);
            Console.WriteLine();
            Thread.Sleep(PauseDuration);
            OldTimeyTextPrinter("BETTER LUCK NEXT TIME", 60);
            Console.WriteLine();
            Console.WriteLine();
            Thread.Sleep(PauseDuration * 2);
            OldTimeyTextPrinter("PLAY AGAIN?   Y or N", 60);
            Console.WriteLine();
            if (Console.ReadLine().ToUpper() == "Y")
            {
                RunGame();
            }
            IsPlaying = false;
        }

        static void Graphic(int switchNumber)
        {
            switch (switchNumber)
            {
                case 0:
                    Console.WriteLine(@"
 _______________________
||            \\
||            //
||            \\
||            //
||           //\\
||          ||  ||
||          \\  // 
||           ^^^^
||           
||          
||          
||         
||               
||             
||              
||___________________________
-----------------------------
[][]                     [][]
");
                    break;
                case 1:
                    Console.WriteLine(@"
 _______________________
||            \\
||            //
||            \\
||            //
||          |\_/|
||         =)'T'(= 
||           `'` 
||          
||           
||          
||         
||          
||             
||              
||              
||___________________________
-----------------------------
[][]                     [][]
");
                    break;
                case 2:
                    Console.WriteLine(@"
 _______________________
||            \\
||            //
||            \\
||            //
||          |\_/|
||         =)'T'(= 
||          /`'` 
||          \) 
||           
||         
||         
||         
||              
||              
||              
||___________________________
-----------------------------
[][]                     [][]
");
                    break;
                case 3:
                    Console.WriteLine(@"
 _______________________
||            \\
||            //
||            \\
||            //
||          |\_/|
||         =)'T'(= 
||          /`'`\  
||          \) (/
||           
||         
||         
||         
||             
||             
||              
||___________________________
-----------------------------
[][]                     [][]
");
                    break;
                case 4:
                    Console.WriteLine(@"
 _______________________
||            \\
||            //
||            \\
||            //
||          |\_/|
||         =)'T'(= 
||          /`'`\  
||          \) (/
||           | |
||         
||         
||          
||              
||             
||               
||___________________________
-----------------------------
[][]                     [][]
");
                    break;
                case 5:
                    Console.WriteLine(@"
 _______________________
||            \\
||            //
||            \\
||            //
||          |\_/|
||         =)'T'(= 
||          /`'`\  
||          \) (/
||           | |
||          /\ 
||          \ T 
||          (/ 
||               
||              
||              
||___________________________
-----------------------------
[][]                     [][]
");
                    break;
                case 6:
                    Console.WriteLine(@"
 _______________________
||            \\
||            //
||            \\
||            //
||          |\_/|
||         =)'T'(= 
||          /`'`\  
||          \) (/
||           | |
||          /\ /\
||          \ T /
||          (/ \)
||               
||             
||              
||___________________________
-----------------------------
[][]                     [][]
");
                    break;
                case 7:
                    Console.WriteLine(@"
 _______________________
||            \\
||            //
||           ___
||    ,_    '---'    _,
||    \ `-._|\_/|_.-' /
||     |   =)'T'(=   |
||      \   /`'`\   /
||       '._\) (/_.'
||           | |
||          /\ /\
||          \ T /
||          (/ \)\
||               ))
||              ((
||               \)
||___________________________
-----------------------------
[][]                     [][]
");
                    break;
                case 8:
                    Console.WriteLine(@"


           /\
            \ \
             \ \
             / /
            / /
           _\ \_/\/\
          /  *  \@@ =
         |       |Y/
         |       |~
          \ /_\ /
           \\ //
            |||
           _|||_
          ( / \ )
");
                    break;
                case 9:
                    Console.WriteLine(@"
 ██░ ██  ▄▄▄       ███▄    █   ▄████     ▄████▄   ▄▄▄     ▄▄▄█████▓
▓██░ ██▒▒████▄     ██ ▀█   █  ██▒ ▀█▒   ▒██▀ ▀█  ▒████▄   ▓  ██▒ ▓▒
▒██▀▀██░▒██  ▀█▄  ▓██  ▀█ ██▒▒██░▄▄▄░   ▒▓█    ▄ ▒██  ▀█▄ ▒ ▓██░ ▒░
░▓█ ░██ ░██▄▄▄▄██ ▓██▒  ▐▌██▒░▓█  ██▓   ▒▓▓▄ ▄██▒░██▄▄▄▄██░ ▓██▓ ░ 
░▓█▒░██▓ ▓█   ▓██▒▒██░   ▓██░░▒▓███▀▒   ▒ ▓███▀ ░ ▓█   ▓██▒ ▒██▒ ░ 
 ▒ ░░▒░▒ ▒▒   ▓▒█░░ ▒░   ▒ ▒  ░▒   ▒    ░ ░▒ ▒  ░ ▒▒   ▓▒█░ ▒ ░░   
 ▒ ░▒░ ░  ▒   ▒▒ ░░ ░░   ░ ▒░  ░   ░      ░  ▒     ▒   ▒▒ ░   ░    
 ░  ░░ ░  ░   ▒      ░   ░ ░ ░ ░   ░    ░          ░   ▒    ░      
 ░  ░  ░      ░  ░         ░       ░    ░ ░            ░  ░        
                                        ░                          
");
                    break;

                default:
                    break;
            }
            
        }
    }
}
