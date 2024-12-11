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
        private FileHandler _fileHandler;


        public KeyboardListener(List<List<char>> buffer, Cursor cursor, FileHandler fileHandler)  
        {
            _buffer = buffer;
            _cursor = cursor;
            _fileHandler = fileHandler;
        }


        /* Use this constructor if using a separate thread for listening for key presses. 
         * Pass AutoResetEvent to communicate with other threads. */
        public KeyboardListener(List<List<char>> buffer, Cursor cursor, AutoResetEvent autoResetEvent, FileHandler fileHandler) 
        { 
            _autoResetEvent = autoResetEvent;
            _buffer = buffer;
            _cursor = cursor;
            _fileHandler = fileHandler;
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
                        case ConsoleKey.Q:
                        {
                            runListener = false;
                            break;
                        }
                        case ConsoleKey.W:
                        {
                            _fileHandler.Write();
                            Console.Clear();
                            Console.WriteLine("Wrote the file to disk. Press any key to go back to the file...");
                            Console.ReadKey(true);
                            break;
                        }
                        case ConsoleKey.Z:
                            _cursor.Undo();
                            break;
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
                        default:
                        {
                            if (Char.IsLetter(keyInfo.KeyChar))
                            {
                                _cursor.WriteChar(Char.ToUpper(keyInfo.KeyChar));
                            }
                            break;
                        }
                    }
                }
                // Handle every other key presses.
                else
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Backspace:
                        {
                            _cursor.RemoveChar();
                            break;
                        }                            
                        case ConsoleKey.DownArrow:
                        {
                            _cursor.MoveDown();
                            break;
                        }
                        case ConsoleKey.Enter:
                        {
                            _cursor.NewLine();
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
                        case ConsoleKey.Spacebar:
                        {
                            _cursor.WriteChar(' ');
                            break;
                        }
                        case ConsoleKey.Tab:
                        {
                            _cursor.WriteTab();
                            break; 
                        }
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
