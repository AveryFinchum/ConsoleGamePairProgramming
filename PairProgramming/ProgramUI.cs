using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairProgramming
{
    public class ProgramUI
    {
        private readonly DisplayMethods display = new DisplayMethods();
        public readonly int gameBlockSize = 6;
        public void Run()
        {

            //method to do welcome screen 

            var windowSizeX = (System.Int16)80;
            var windowSizeY = (System.Int16)60;
            const int cursorOffsetX = 24;
            const int cursorOffsetY = 4;

            int gamePositionX = 0;
            int gamePositionY = cursorOffsetY;

            //Console.SetBufferSize(windowSizeX+1, windowSizeY+1);
            //Console.SetWindowSize(windowSizeX, windowSizeY);


            int attempts = 6;
            bool keepRunning = true;
            Console.Clear();

            WordListClass wordListClass = new WordListClass();
            string word = wordListClass.RandomWord();

            while (keepRunning)
            {
                //reset cursor position to left side of new line (with game padding)
                gamePositionX = cursorOffsetX;


                display.DrawKeyboard();

                Console.SetCursorPosition(0, 0);
                Console.Write("This is how you play.\nPlease enter a 5-letter word: ");


                Console.Write("          ");
                Console.SetCursorPosition(Console.CursorLeft - 10, Console.CursorTop);



                string answer = Console.ReadLine();//add input scrubbing here


                //word = "stuff";
                bool isNoProblem = !answer.Any(x => !char.IsLetter(x));


                Console.SetCursorPosition(0, 3);
                Console.WriteLine("                                ");


                Console.SetCursorPosition(0, 3);
                if (isNoProblem == true && answer.Length == 5)

                {

                    for (int wletter = 0; wletter < answer.Length; wletter++)//iterates over each letter in users word
                    {
                        bool shouldBeYellow = false;
                        bool shouldBeGreen = false;
                        bool shouldBeblack = true;

                        for (int aword = 0; aword < word.Length; aword++) //deals with one character at a time daily word
                        {
                            if (answer[wletter] == word[aword]) //if user letter is in word at all
                            {
                                if (answer[wletter] == word[wletter]) //is user letter in the right spot
                                {
                                    // letter lights green
                                    //Console.WriteLine($"Letter {answer[wletter]} should be green.");

                                    shouldBeGreen = true;
                                    shouldBeYellow = false;
                                    shouldBeblack = false;
                                }
                                else
                                {
                                    //letter lights yellow
                                    //Console.WriteLine($"Letter {answer[wletter]} should be yellow.");
                                    if (shouldBeGreen == false)
                                    {

                                        shouldBeYellow = true;
                                    }
                                    shouldBeblack = false;
                                }
                            }
                            else //the letter is not in the word
                            {
                                //letter goes black
                                //Console.WriteLine($"Letter {answer[wletter]} should not be lit up.");
                            }
                        }
                        //Console.WriteLine($"Letter {answer[wletter]}: Should be yellow: {shouldBeYellow}   Should be green: {shouldBeGreen} Should be black: {shouldBeblack}");


                        if (shouldBeGreen)
                        {
                            display.CorrectLetterInCorrectSpot(answer[wletter], gamePositionX, gamePositionY);

                        }
                        else if (shouldBeYellow)
                        {
                            display.CorrectLetterInWord(answer[wletter], gamePositionX, gamePositionY);

                        }
                        else
                        {
                            display.Wrong(answer[wletter], gamePositionX, gamePositionY);

                        }
                        //iterate cursor position
                        gamePositionX += gameBlockSize;

                    }

                    //Check if they won
                    if (answer == word)
                    {
                    display.YouWin(cursorOffsetX, gamePositionY + gameBlockSize);
                        keepRunning = false;

                    }

                    attempts--;
                    //if not attempst--
                    if (attempts <= 0)
                    {
                        Console.WriteLine("YOU LOSE");
                        keepRunning = false;
                    }
                    //5 guess = keepRunning false;

                    //advance game's cursor position downwards
                    gamePositionY += gameBlockSize;
                }
                else
                {
                    Console.SetCursorPosition(0, 3);
                    Console.WriteLine("Your guess needs to be 5 letters");
                }

            }


            //Console.ReadLine();



        }
    }
}
