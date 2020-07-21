using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rzeczuchyTrack.Views;
using rzeczuchyTrack.Data;

namespace rzeczuchyTrack
{
    class App
    {
        private bool isRunning;
        private readonly Stack<View> views;

        public App()
        {
            Console.Title = "rzeczuchyTrack";
            Console.CursorVisible = false;
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;
            Console.OutputEncoding = Encoding.Unicode;

            views = new Stack<View>();

            views.Push(new MainView());

            isRunning = true;
            Run();
        }

        private void Run()
        {
            DrawTopView();
            while (isRunning)
            {
                Thread.Sleep(16);
                UpdateTopView();
            }
        }

        private void UpdateTopView()
        {
            if(Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                if (views.Any())
                {
                    GetTopView().UpdateInput(input);
                    DrawTopView();
                }
            }

            if (views.Any())
            {
                if (GetTopView().End)
                {
                    CloseTopView();
                }
                else
                {
                    GetTopView().Update();
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
