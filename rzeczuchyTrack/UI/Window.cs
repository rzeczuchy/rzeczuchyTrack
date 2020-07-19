using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rzeczuchyTrack.Utilities;

namespace rzeczuchyTrack.UI
{
    class Window : UIState
    {
        public Window(Point position, Point size)
        {
            Position = position;
            Size = size;
            BackgroundColor = ConsoleColor.DarkGray;
            ForegroundColor = ConsoleColor.White;
        }

        public Point Position { get; set; }
        public Point Size { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }

        public override void Update(ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.Escape:
                    Close();
                    break;
                default:
                    break;
            }
        }

        public override void Draw()
        {
            string top = "╔";
            string mid = "║";
            string bottom = "╚";
            for (int i = 0; i < Size.X; i++)
            {
                top += "═";
            }
            top += "╗";

            for (int i = 0; i < Size.X; i++)
            {
                mid += " ";
            }
            mid += "║";

            for (int i = 0; i < Size.X; i++)
            {
                bottom += "═";
            }
            bottom += "╝";

            Utility.DrawString(top, Position, BackgroundColor, ForegroundColor);

            for (int y = 1; y < Size.Y; y++)
            {
                Utility.DrawString(mid, new Point(Position.X, Position.Y + y), BackgroundColor, ForegroundColor);
            }

            Utility.DrawString(bottom, new Point(Position.X, Position.Y + Size.Y), BackgroundColor, ForegroundColor);
        }
    }
}
