using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rzeczuchyTrack.Utilities;
using rzeczuchyTrack.TimeEntries;

namespace rzeczuchyTrack.UI
{
    class TimerWindow : UIState
    {
        private readonly Window window;

        public TimerWindow(Point position, Point size)
        {
            Position = position;
            Size = size;
            window = new Window(position, size);
            Timer = new DateTime(1, 1, 1, 0, 0, 0);
        }

        public Point Position { get; set; }
        public Point Size { get; set; }
        public DateTime Timer { get; set; }

        public override void Draw()
        {
            window.Draw();
            Utility.DrawString("New entry:", new Point(Position.X + 1, Position.Y + 1), window.BackgroundColor, window.ForegroundColor);
            Utility.DrawString(Timer.ToString("hh:mm:ss"), new Point(Position.X + Size.X - 10, Position.Y + 1), window.BackgroundColor, window.ForegroundColor);
        }
    }
}
