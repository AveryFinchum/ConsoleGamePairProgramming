using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairProgramming
{
    public class ProgramUI
    {

        public void Run()
        {

            //method to do welcome screen 

            WordListClass wordListClass = new WordListClass();

            Console.Write("This is how you play please enter a word ");

            string word = wordListClass.RandomWord();

            string answer = Console.ReadLine();

            char lOne = answer[0];
            char lTwo = answer[1];
            char lThree = answer[2];
            char lFour = answer[3];
            char lFive = answer[4];

            word = "eeeks";
            answer = "belkj";

            for (int aword = 0; aword < word.Length; aword++) //deals with one character at a time daily word
            {
                bool shouldBeYellow = false;
                bool shouldBeGreen = false;
                bool shouldBeblack = true;

                for (int wletter = 0; wletter < answer.Length; wletter++)//iterates over each letter in users word
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
                                shouldBeblack = false;
                            }

                        }

                    }
                    else //the letter is not in the word
                    {
                        //letter goes black
                        //Console.WriteLine($"Letter {answer[wletter]} should not be lit up.");
                    }
                }
                Console.WriteLine($"Letter {answer[aword]}: Should be yellow: {shouldBeYellow}   Should be green: {shouldBeGreen} Should be black: {shouldBeblack}");

            }
            Console.ReadLine();
        }
    }
}
