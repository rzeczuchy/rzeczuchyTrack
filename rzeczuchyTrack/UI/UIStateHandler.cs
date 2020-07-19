using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rzeczuchyTrack.UI
{
    class UIStateHandler
    {
        private readonly Stack<UIState> states;

        public UIStateHandler()
        {
            states = new Stack<UIState>();
        }

        public void AddState(UIState item)
        {
            states.Push(item);
        }

        public void Update()
        {
            if (states.Any())
            {
                if (GetTopState().End)
                {
                    CloseTopState();
                }
                else
                {
                    GetTopState().Update();
                }
            }
        }

        public void UpdateInput(ConsoleKey input)
        {
            if (states.Any())
            {
                GetTopState().Update(input);
            }
        }

        public void Draw()
        {
            List<UIState> toDraw = states.Where(i => !i.End).ToList();
            toDraw.Reverse();
            for (int i = 0; i < toDraw.Count; i++)
            {
                toDraw[i].Draw();
            }
        }

        public UIState GetTopState()
        {
            if (states.Any())
            {
                return states.Peek();
            }
            else return null;
        }

        private void CloseTopState()
        {
            GetTopState().OnClose();
            states.Pop();

            if (states.Any())
            {
                GetTopState().OnOpen();
            }
        }
    }
}
