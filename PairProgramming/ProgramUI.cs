using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairProgramming
{
    public class ProgramUI
    {
        public readonly int gameBlockSize = 6;
        private Dictionary<char, KeyboardDict> _letters = new Dictionary<char, KeyboardDict>();
        private DisplayMethods _display = new DisplayMethods();

        //public Dictionary<char, letterColorValue> KeyboardColorGuesses = new Dictionary<char, letterColorValue>();        

        private void Seed()
        {
            string letters = "abcdefghijklmnopqrstuvwxyz";
            foreach (char character in letters)
            {
                _letters.Add(character, new KeyboardDict());
            }
        }



        public void Run()
        {
            //Seed();

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

            System.Threading.Tasks.Task.Run((Action)PlaySong);



            WordListClass wordListClass = new WordListClass();
            string word = wordListClass.RandomWord();

            //TODO:
            //?maybe add letter count checking
            //wrap while in UI, play again etc
            //add more words
            //add border boxes
            //scrub user input for only letters, only allow 5 ✓
            //add character at a time

            string Splash = @"
 __       __                              __   __           
|  \  _  |  \                            |  \ |  \          
| $$ / \ | $$   ______    ______     ____| $$ | $$   ______
| $$/  $\| $$ /       \  /      \   /      $$ | $$  /      \ 
| $$  $$$\ $$ |  $$$$$$\ |  $$$$$$\|  $$$$$$$ | $$ |  $$$$$$\
| $$ $$\$$\$$ | $$  | $$ | $$   \$$| $$  | $$ | $$ | $$    $$
| $$$$  \$$$$ | $$__/ $$ | $$      | $$__| $$ | $$ | $$$$$$$$
| $$$    \$$$  \$$    $$ | $$       \$$    $$ | $$  \$$     \
 \$$      \$$   \$$$$$$   \$$        \$$$$$$$  \$$   \$$$$$$$
                                                         
";

            void drawSplash()
            {
                foreach (char l in Splash)
                {
                    Thread.Sleep(2);
                    Console.Write(l);
                }
                Console.WriteLine();
            }



            bool mainMenu = true;
            while (mainMenu)

            {
                drawSplash();
                Console.ReadKey();
                Console.Clear();
                Console.Write("Welcome to FakeWordle! Would you like to: \n" +
                    "1. Read the rules? \n" +
                    "2. Play the game? \n" +
                    "3. Exit\n ");

                string userInput = Console.ReadLine();
                Console.Clear();
                switch (userInput)
                {
                    case "1":
                        Rules();
                        break;
                    case "2":
                        NewGame();
                        MainGame();
                        break;
                    case "3":
                    case "exit":
                    case "e":
                        mainMenu = false;
                        break;

                }

            }


            void Rules()
            {


                Console.WriteLine("Wordle gives players six chances to guess a randomly selected five-letter word.");
                Console.ForegroundColor = ConsoleColor.Green;
                _display.PrintLetterTile('X');
                Console.WriteLine("If you have the right letter in the right spot, it shows up green.");


                Console.ForegroundColor = ConsoleColor.Yellow;
                _display.PrintLetterTile('X');
                Console.WriteLine("A correct letter in the wrong spot shows up yellow. ");



                Console.ForegroundColor = ConsoleColor.Gray;
                _display.PrintLetterTile('X');
                Console.WriteLine("A letter that isn't in the word in any spot shows up gray.");


                Console.ReadKey();

                Console.Clear();

            }

            void NewGame()
            {
                _letters = new Dictionary<char, KeyboardDict>();
                Seed();
                //windowSizeX = (System.Int16)80;
                //windowSizeY = (System.Int16)60;
             

                gamePositionX = 0;
                gamePositionY = cursorOffsetY;
                attempts = 6;


                word = wordListClass.RandomWord();
            }


            void MainGame()
            {

                keepRunning = true;
                while (keepRunning)
                {
                    //reset cursor position to left side of new line (with game padding)
                    gamePositionX = cursorOffsetX;


                    _display.DrawKeyboard(_letters);

                    Console.SetCursorPosition(0, 0);
                    Console.Write("The game has started! \nPlease enter a 5-letter word: ");


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


                            letterColorValue letterVal;
                            letterVal.green = false;
                            letterVal.yellow = false;
                            letterVal.black = false;




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
                                        letterVal.green = true;
                                        letterVal.yellow = false;
                                        letterVal.black = false;
                                    }
                                    else
                                    {
                                        //letter lights yellow
                                        //Console.WriteLine($"Letter {answer[wletter]} should be yellow.");
                                        if (shouldBeGreen == false)
                                        {
                                            letterVal.yellow = true;
                                            shouldBeYellow = true;
                                        }
                                        shouldBeblack = false;
                                        letterVal.black = false;
                                    }
                                }
                                else //the letter is not in the word
                                {
                                    //letter goes black
                                    //Console.WriteLine($"Letter {answer[wletter]} should not be lit up.");
                                    letterVal.black = true;
                                }
                                //set color of character in dictionary _letters
                                _letters[answer[wletter]].Green = _letters[answer[wletter]].Green || letterVal.green;
                                _letters[answer[wletter]].Yellow = _letters[answer[wletter]].Yellow || letterVal.yellow;
                                _letters[answer[wletter]].Black = _letters[answer[wletter]].Black || letterVal.black;


                            }
                            //Console.WriteLine($"Letter {answer[wletter]}: Should be yellow: {shouldBeYellow}   Should be green: {shouldBeGreen} Should be black: {shouldBeblack}");


                            if (shouldBeGreen)
                            {
                                _display.CorrectLetterInCorrectSpot(answer[wletter], gamePositionX, gamePositionY);

                            }
                            else if (shouldBeYellow)
                            {
                                _display.CorrectLetterInWord(answer[wletter], gamePositionX, gamePositionY);

                            }
                            else
                            {
                                _display.Wrong(answer[wletter], gamePositionX, gamePositionY);

                            }
                            //iterate cursor position
                            gamePositionX += gameBlockSize;

                        }

                        //Check if they won
                        if (answer == word)
                        {
                            _display.GameEnd(cursorOffsetX, gamePositionY + gameBlockSize, true);
                            keepRunning = false;

                        }

                        attempts--;
                        //if not attempst--
                        if (attempts <= 0)
                        {
                            _display.GameEnd(cursorOffsetX, gamePositionY + gameBlockSize, false);
                            //Console.WriteLine("YOU LOSE");
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
            }


            //Console.ReadLine();


        }

        static void PlaySong()
        {
            while (true)
            {
                Console.Beep(659, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(523, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(375); Console.Beep(392, 125); Thread.Sleep(375); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(375); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125); Thread.Sleep(1125); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125);
                Thread.Sleep(350);
            }
        }
    }

}
