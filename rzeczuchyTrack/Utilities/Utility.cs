using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rzeczuchyTrack.Utilities
{
    static class Utility
    {
        public static int Clamp(int number, int min, int max)
        {
            return (number < min) ? min : (number > max) ? max : number;
        }
        
        public static void DrawString(string str, Point position, ConsoleColor background, ConsoleColor foreground)
        {
            if (IsWithinBuffer(position))
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.SetCursorPosition(position.X, position.Y);
            Console.WriteLine(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static bool IsWithinBuffer(Point position)
        {
            return position.X >= 0 &&
                position.Y >= 0 &&
                position.X < Console.BufferWidth &&
                position.Y < Console.BufferHeight;
        }
    }
}
