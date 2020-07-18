using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rzeczuchyTrack.Views;

namespace rzeczuchyTrack
{
    class App
    {
        private bool isRunning;
        private readonly Stack<View> views;

        public App()
        {
            Console.Title = "rzeczuchyTrack";

            views = new Stack<View>();

            isRunning = true;
            Run();
        }

        private void Run()
        {
            while (isRunning)
            {
                UpdateTopView();
                DrawTopView();
            }
        }

        private void UpdateTopView()
        {
            ConsoleKey input = Console.ReadKey(true).Key;
            if (input == ConsoleKey.Escape)
            {
                Exit();
            }

            if (views.Any())
            {
                if (GetTopView().End)
                {
                    CloseTopView();
                }
                else
                {
                    GetTopView().Update(input);
                }
            }
            else
            {
                Exit();
            }

        }

        private View GetTopView()
        {
            if (views.Any())
            {
                return views.Peek();
            }
            else return null;
        }

        private void Exit()
        {
            isRunning = false;
        }

        private void CloseTopView()
        {
            GetTopView().OnClose();
            views.Pop();

            if (views.Any())
            {
                GetTopView().OnOpen();
            }
        }


        private void DrawTopView()
        {
            Console.Clear();

            if (views.Any())
            {
                GetTopView().Draw();
            }
        }
    }
}
