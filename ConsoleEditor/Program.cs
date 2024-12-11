using ConsoleEditor.FileManagement;
using ConsoleEditor.Keyboard;
using System;
using System.Threading;

namespace ConsoleEditor
{
    internal class Program
    {
        private static bool _displayBuffer = true;
        private static bool _exitSignal = false;

        static int Main(string[] args)
        {
            Console.Clear();

            var fileHandler = new FileHandler(@"C:\Users\willi\source\repos\ConsoleEditor\ConsoleEditor\test.txt");
            var autoResetEvent = new AutoResetEvent(false);

            fileHandler.Read();

            // Thread for listening for which keys the user pressed.
            var keyboardThread = new Thread(() =>
            {
                var keyboardListener = new KeyboardListener(
                    fileHandler.FileBuffer, 
                    fileHandler.GetCursor(),
                    autoResetEvent,
                    fileHandler
                );
                keyboardListener.Listen();
                _displayBuffer = false;
                _exitSignal = true;
            });
            keyboardThread.Start();


            // Thread to render the buffer to the console when a key is pressed.
            var displayThread = new Thread(() =>
            { 
                while (_displayBuffer && !_exitSignal)
                {
                    fileHandler.DisplayBuffer();
                    autoResetEvent.WaitOne();
                }
            });
            displayThread.Start();


            keyboardThread.Join();
            displayThread.Join();

            Console.Clear();

            Console.WriteLine("Closing editor...");

            return 0;
        }
    }
}