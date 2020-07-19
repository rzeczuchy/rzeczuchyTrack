using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rzeczuchyTrack.UI
{
    class UIStateHandler
    {
        private readonly List<UIState> states;

        public UIStateHandler()
        {
            states = new List<UIState>();
        }

        public UIState Focused { get; set; }

        public void AddState(UIState item)
        {
            states.Add(item);
        }

        public void Update()
        {
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].End)
                {
                    CloseState(states[i]);
                }
                states[i].Update();
            }
        }

        public void UpdateInput(ConsoleKeyInfo input)
        {
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i] == Focused)
                {
                    states[i].UpdateInput(input);
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < states.Count; i++)
            {
                states[i].Draw();
            }
        }

        private void CloseState(UIState state)
        {
            state.OnClose();
            states.Remove(state);
        }
    }
}
