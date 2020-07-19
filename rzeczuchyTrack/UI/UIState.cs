using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rzeczuchyTrack.UI
{
    class UIState
    {
        public virtual void Draw() { }

        public virtual void OnClose() { }

        public virtual void OnOpen() { }
        
        public virtual void Update() { }

        public virtual void UpdateInput(ConsoleKeyInfo input) { }
    }
}
