using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rzeczuchyTrack.UI
{
    class UIState
    {
        public bool End { get; set; }

        public void Close()
        {
            End = true;
        }

        public virtual void Draw() { }

        public virtual void OnClose() { }

        public virtual void OnOpen() { }
        
        public virtual void Update() { }

        public virtual void UpdateInput(ConsoleKey input) { }
    }
}
