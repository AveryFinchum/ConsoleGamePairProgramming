using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairProgramming
{
    public class WordListClass
    {
       public List<string> wordList = new List<string> { "cigar", "rebut", "sissy", "humph", "awake", "blush", "focal", "evade", "naval", "serve", "heath", "dwarf", "model" };

        public string RandomWord()
        {
            Random randy = new Random();
            int n = randy.Next(wordList.Count-1);

            string todaysWord = wordList[n];

            return todaysWord;

        }
    }
}
