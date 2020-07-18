using System;
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
        private readonly DataReaderWriter data;

        public App()
        {
            Console.Title = "rzeczuchyTrack";
            Console.CursorVisible = false;
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;
            Console.OutputEncoding = Encoding.Unicode;

            views = new Stack<View>();
            data = new DataReaderWriter();

            views.Push(new MainView(data));

            isRunning = true;
            Run();
        }

        private void Run()
        {
            DrawTopView();
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
