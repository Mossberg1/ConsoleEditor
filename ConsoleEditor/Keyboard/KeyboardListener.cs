using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleEditor.FileManagement;

namespace ConsoleEditor.Keyboard
{
    internal class KeyboardListener
    {

        private AutoResetEvent? _autoResetEvent;
        private List<List<char>> _buffer;
        private Cursor _cursor;


        public KeyboardListener(List<List<char>> buffer, Cursor cursor)  
        {
            _buffer = buffer;
            _cursor = cursor;
        }


        /* Use this constructor if using a separate thread for listening for key presses. 
         * Pass AutoResetEvent to communicate with other threads. */
        public KeyboardListener(List<List<char>> buffer, Cursor cursor, AutoResetEvent autoResetEvent) 
        { 
            _autoResetEvent = autoResetEvent;
            _buffer = buffer;
            _cursor = cursor;
        }


        // Method to listen for keys, and make descisions based on what key was pressed.
        public void Listen() 
        {
            bool runListener = true;

            while (runListener)
            {
                var keyInfo = Console.ReadKey(true);

                // Handle key presses when CTRL is pressed.
                if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.X:
                        {
                            runListener = false;
                            break;
                        }
                        default:
                        {
                            break;
                        }
                    }
                }
                // Handle key presses when shift is pressed.
                else if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift))
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.D1:
                        {
                            _cursor.WriteChar('!');
                            break;
                        }
                    }
                }
                // Handle every other key presses.
                else
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.DownArrow:
                        {
                            _cursor.MoveDown();
                            break;
                        }
                        case ConsoleKey.LeftArrow:
                        {
                            _cursor.MoveLeft();
                            break;
                        }
                        case ConsoleKey.RightArrow:
                        {
                            _cursor.MoveRight();
                            break;
                        }
                        case ConsoleKey.UpArrow:
                        {
                            _cursor.MoveUp();
                            break;
                        }
                        case ConsoleKey.Backspace:
                            _cursor.RemoveChar();
                            break;
                        default:
                        {
                            if (Char.IsLetter(keyInfo.KeyChar))
                            {
                                _cursor.WriteChar(keyInfo.KeyChar);
                            }
                            break;
                        }
                    }
                }

                if (_autoResetEvent != null)
                {
                    _autoResetEvent.Set();
                }
            }
        }
    }
}
