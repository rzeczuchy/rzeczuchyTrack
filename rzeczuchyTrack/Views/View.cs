using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rzeczuchyTrack.Views
{
    class View
    {
        public View() { }

        public bool End { get; set; }

        public virtual void Draw() { }

        public virtual void OnClose() { }

        public virtual void OnOpen() { }

        public virtual void Update() { }

        public virtual void UpdateInput(ConsoleKeyInfo input) { }
    }
}
