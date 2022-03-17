using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairProgramming
{
    public class DisplayMethods
    {
        // private Dictionary<char, letterColorValue> _keyboardColorGuesses = new Dictionary<char, letterColorValue>();

        //  public struct letterColorValue
        //{
        //  public bool green;
        //public bool yellow;
        // public bool black;
        // }




        public void GetStruct(bool green, bool yellow, bool black)
        {



        }



        public void CorrectLetterInCorrectSpot(char letter, int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            PrintLetterTile(letter, x, y);
        }


        public void CorrectLetterInWord(char letter, int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintLetterTile(letter, x, y);
        }

        public void Wrong(char letter, int x, int y)
        {
            PrintLetterTile(letter, x, y);
        }

        public void GameEnd(int x, int y, bool won)
        {
            ProgramUI p = new ProgramUI();
            Random randy = new Random();

            string msg = "you win!";
            if (won)
            {
                msg = "you win!";
            }
            else
            {
                msg = "you fail!";
            }



            if (x > 0)
            {
                x = x - ((msg.Length / 2) * p.gameBlockSize / 2);
            }
            foreach (char l in msg)
            {
                Thread.Sleep(200);

                int n = randy.Next(1, 16);
                
                if (won)
                {
                    Console.ForegroundColor = (ConsoleColor)n;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    
                }

                PrintLetterTile( ((l==' ')? '░': l ), x, y);
                x += p.gameBlockSize;


            }
                Console.ReadKey();
            Console.Clear();
        }
        public void PrintLetterTile(char letter)
        {
            PrintLetterTile(letter, Console.CursorLeft, Console.CursorTop);
        }


        public void PrintLetterTile(char letter, int x, int y)
        {

            Console.SetCursorPosition(x, y++); Console.Write("╔═══╗");
            Console.SetCursorPosition(x, y++); Console.Write("║░░░║");
            Console.SetCursorPosition(x, y++); Console.Write($"║░{letter.ToString().ToUpper()}░║");
            Console.SetCursorPosition(x, y++); Console.Write("║░░░║");
            Console.SetCursorPosition(x, y);   Console.Write("╚═══╝");

            Console.ForegroundColor = ConsoleColor.White;

        }
        char[] keysToDraw = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm' };

        public void DrawKeyboard(Dictionary<char, KeyboardDict> letters)
        {

            ProgramUI p = new ProgramUI();
            int boxPadding = 1;
            DrawBox(boxPadding, ((8 * p.gameBlockSize)) - boxPadding, (boxPadding * 2) + (10 * p.gameBlockSize), (boxPadding * 2) + (3 * p.gameBlockSize));

            int row = 0;
            int column = 3;
            int distanceToPrint = 0;
            int numb = 0;
            for (int i = 0; i <= 25; i++)
            {
                if (i == 10)
                {
                    column = 7;
                    row++;
                    numb = 0;
                }
                if (i == 19)
                {
                    column = 11;
                    row++;
                    numb = 0;
                }
                distanceToPrint = column + (p.gameBlockSize * numb);

                //lookup characters color
                //set console color

                if (letters[keysToDraw[i]].Green)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (letters[keysToDraw[i]].Yellow)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (letters[keysToDraw[i]].Black && !letters[keysToDraw[i]].Green && !letters[keysToDraw[i]].Yellow)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }


                PrintLetterTile(keysToDraw[i], distanceToPrint, ((8 * p.gameBlockSize) + (row * p.gameBlockSize)));
                numb++;
            }


        }

        public void DrawBox(int offsetX, int offsetY, int sizeX, int sizeY)
        {
            for (int y = offsetY; y <= offsetY + sizeY; y++)
            {
                for (int x = offsetX; x <= offsetX + sizeX; x++)
                {
                    Console.SetCursorPosition(x, y);

                    if (y == offsetY || y != offsetY + sizeY) // top and bottom
                    {
                        if (y == offsetY && x == offsetX)//Top left corner 
                        {
                            Console.Write("╔");
                        }
                        else if (y == offsetY && x == offsetX + sizeX)//Top Right Corner 
                        {
                            Console.Write("╗");
                        }
                        else if (y == offsetY + sizeY - 1 && x == offsetX)//Bottom Left Corner 
                        {
                            Console.Write("╚");
                        }
                        else if (y == offsetY + sizeY - 1 && x == offsetX + sizeX)//Bottom Right Corner 
                        {
                            Console.Write("╝");
                        }
                        else if ((x == offsetX || x == offsetX + sizeX))//sides 
                        {
                            Console.Write("║");
                        }
                        else if (y == offsetY || y == offsetY + sizeY - 1)  // Middle section for top and bottom
                        {
                            Console.Write("═");

                        }
                        else
                        {
                            //Middle area
                        }

                    }
                }
            }
        }





    }
}
