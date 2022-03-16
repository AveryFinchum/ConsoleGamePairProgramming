﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairProgramming
{
    public class DisplayMethods
    {

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

        public void YouWin(int x, int y)
        {
            ProgramUI p = new ProgramUI();
            Random randy = new Random();

            string winMsg = "you win!asd";



            if (x > 0)
            {
                x = x - ((winMsg.Length / 2) * p.gameBlockSize / 2);
            }
            foreach (char l in winMsg)
            {
                Thread.Sleep(200);

                int n = randy.Next(1, 16);
                Console.ForegroundColor = (ConsoleColor)n;
                PrintLetterTile(l, x, y);
                x += p.gameBlockSize;

            }
        }

        public void PrintLetterTile(char letter, int x, int y)
        {

            Console.SetCursorPosition(x, y++); Console.Write("╔═══╗");
            Console.SetCursorPosition(x, y++); Console.Write("║░░░║");
            Console.SetCursorPosition(x, y++); Console.Write($"║░{letter.ToString().ToUpper()}░║");
            Console.SetCursorPosition(x, y++); Console.Write("║░░░║");
            Console.SetCursorPosition(x, y); Console.Write("╚═══╝");

            Console.ForegroundColor = ConsoleColor.White;

        }
        char[] keysToDraw = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm' };

        public void DrawKeyboard()
        {
            ProgramUI p = new ProgramUI();
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
                PrintLetterTile(keysToDraw[i], distanceToPrint, ((8 * p.gameBlockSize) + (row * p.gameBlockSize)));
                numb++;
            }


        }
    }
}