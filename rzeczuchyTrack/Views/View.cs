using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rzeczuchyTrack.Views
{
    abstract class View
    {
        public View()
        {

        }

        public bool End { get; set; }

        public abstract void Draw();

        public abstract void OnClose();

        public abstract void OnOpen();

        public abstract void Update(ConsoleKey input);
    }
}
