using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        static int tries;
        static bool guessed;
        static bool again = true;

        static void Main(string[] args)
        {
            while (again == true) {
                tries = 0;
                guessed = false;
                string[] lines = System.IO.File.ReadAllLines(@"d:\documenten\visual studio 2017\Projects\Hangman\Hangman\words.txt");
                Random rnd = new Random();
                int nr = rnd.Next(1, lines.Length);
                string word = lines[nr];
                Console.WriteLine("Welcome to hangman!\n\nThe CPU has chosen a word. Press any letter to start guessing\n");

                var newlist = new List<Tuple<int, string>>();
                while (guessed == false)
                {
                    Guess(word, newlist);
                    if (newlist.Count == word.Length || tries == 11)
                    {
                        Console.Clear();
                        guessed = true;
                    }
                }

                if (tries < 10)
                {
                    Console.WriteLine(" Well done! You found the word '{0}'", word);
                }
                else
                {
                    Console.WriteLine(" Unfortunately, you didn't find the word. The word was '{0}'", word);
                    Console.WriteLine(Man(tries));
                }

                string answer = "";
                while (answer.ToLower() != "n" && answer.ToLower() != "y" || answer.Length != 1)
                {
                    Console.WriteLine("\n Do you want to try again? (Y/N)");
                    answer = Console.ReadLine();
                }

                if (answer == "n")
                {
                    again = false;
                }
                Console.Clear();
            }

        }

        static void Guess(string word, List<Tuple<int, string>> newlist)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (newlist.Count == 0)
                {
                    Console.Write("  _");
                }
                else
                {
                    bool foundj = false;
                    for (int j = 0; j < newlist.Count; j++)
                    {
                        if (newlist[j].Item1 == i)
                        {
                            Console.Write("  {0}", newlist[j].Item2);
                            foundj = true;
                        }
                    }
                    if (foundj == false)
                    {
                        Console.Write("  _");
                    }
                }
            }
            Console.WriteLine("\n");
            string man = Man(tries);
            Console.WriteLine(man);

            bool valid = false;
            string letter = "";
            while (valid == false)
            {
                Console.Write("\n Please input a letter:\t");
                letter = Console.ReadLine();
                if (letter.Length == 1)
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine(" Invalid input, please try again\n");
                }
            }
            Console.Clear();
            char[] letterCh = letter.ToCharArray();
            char[] wordAr = word.ToCharArray();
            bool found = false;

            for (int i = 0; i < wordAr.Length; i++)
            {
                if (wordAr[i] == letterCh[0])
                {
                    bool alreadyIn = false;
                    
                    foreach (var tst in newlist)
                    {
                        if (tst.Item1 == i)
                        {
                            alreadyIn = true;
                        }
                    }
                    if (alreadyIn == false) {
                        newlist.Add(Tuple.Create(i, letter));
                    }
                    found = true;
                }
            }
            if (found == false)
            {
                Console.WriteLine(" Nope, the letter '{0}' is not in the word\n", letter);
                tries++;
            }
            else
            {
                Console.WriteLine(" Well done, you found the letter '{0}'\n", letter);
            }

            //Console.WriteLine("\n The amount of tries: {0}", tries.ToString());
        }

        static string Man (int tries)
        {
            string drawing = "";
            switch (tries)
            {
                case 1:
                    drawing = "\n\n\n\n\n  ________";
                    break;
                case 2:
                    drawing = "\n |\n |\n |\n |\n |________";
                    break;
                case 3:
                    drawing = "\n |/\n |\n |\n |\n |________";
                    break;
                case 4:
                    drawing = " ________\n |/\n |\n |\n |\n |________";
                    break;
                case 5:
                    drawing = " ________\n |/     |\n |\n |\n |\n |________";
                    break;
                case 6:
                    drawing = " ________\n |/     |\n |      0\n |\n |\n |________";
                    break;
                case 7:
                    drawing = " ________\n |/     |\n |      0\n |      |\n |\n |________";
                    break;
                case 8:
                    drawing = " ________\n |/     |\n |      0\n |     "+@"\"+"|\n |\n |________";
                    break;
                case 9:
                    drawing = " ________\n |/     |\n |      0\n |     " + @"\" + "|/\n |\n |________";
                    break;
                case 10:
                    drawing = " ________\n |/     |\n |      0\n |     " + @"\" + "|/\n |     /\n |________";
                    break;
                case 11:
                    drawing = " ________\n |/     |\n |      0\n |     " + @"\" + "|/\n |     / "+@"\"+"\n |________";
                    break;
            }
            return drawing;
        }
    }
}
