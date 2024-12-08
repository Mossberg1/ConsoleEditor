﻿using System.Collections.Generic;
using System;
using ConsoleEditor.FileManagement;
using System.IO;
using ConsoleEditor.Keyboard;
using System.Threading;

namespace ConsoleEditor
{
    internal class Program
    {
        private static bool _displayBuffer = true;

        static int Main(string[] args)
        {
            Console.Clear();

            var fileHandler = new FileHandler(@"C:\Users\willi\source\repos\ConsoleEditor\ConsoleEditor\test.txt");
            var autoResetEvent = new AutoResetEvent(false);

            fileHandler.Open();

            var keyboardThread = new Thread(() =>
            {
                var keyboardListener = new KeyboardListener(
                    fileHandler.FileBuffer, 
                    fileHandler.GetCursor(),
                    autoResetEvent
                );
                keyboardListener.Listen();
                _displayBuffer = false;
            });
            keyboardThread.Start();

            var displayThread = new Thread(() =>
            { 
                while (_displayBuffer)
                {
                    fileHandler.DisplayBuffer();
                    autoResetEvent.WaitOne();
                }
            });
            displayThread.Start();

            keyboardThread.Join();
            displayThread.Join();


            return 0;
        }
    }
}