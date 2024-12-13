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


        // Method to listen for keys, and make decisions based on what key was pressed.
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
                        // CTRL + Q quits the application.
                        case ConsoleKey.Q:
                        {
                            runListener = false;
                            break;
                        }
                        // CTRL + W writes the file to disk.
                        case ConsoleKey.W:
                        {
                            _fileHandler.Write();
                            Console.Clear();
                            Console.WriteLine("Wrote the file to disk. Press any key to go back to the file...");
                            Console.ReadKey(true);
                            break;
                        }
                        // CTRL + U undo most recent change, not using Z to be compatible with linux.
                        case ConsoleKey.U:
                            _cursor.Undo();
                            break;
                        default:
                        {
                            break;
                        }
                    }
                }
                // Handle every other key presses.
                else
                {
                    switch (keyInfo.Key)
                    {
                        // Backspace key removes a char before the cursor.
                        case ConsoleKey.Backspace:
                        {
                            _cursor.RemoveChar();
                            break;
                        }      
                        // Moves the cursor down to the next line.
                        case ConsoleKey.DownArrow:
                        {
                            _cursor.MoveDown();
                            break;
                        }
                        // Enter key make a new line, and moves the cursor to the beginning of that new line.
                        case ConsoleKey.Enter:
                        {
                            _cursor.NewLine();
                            break;
                        }
                        // Moves the cursor to the left.
                        case ConsoleKey.LeftArrow:
                        {
                            _cursor.MoveLeft();
                            break;
                        }
                        // Moves the cursor to the right.
                        case ConsoleKey.RightArrow:
                        {
                            _cursor.MoveRight();
                            break;
                        }
                        // Moves the cursor up one line.
                        case ConsoleKey.UpArrow:
                        {
                            _cursor.MoveUp();
                            break;
                        }
                        // Writes a space char to the buffer.
                        case ConsoleKey.Spacebar:
                        {
                            _cursor.WriteChar(' ');
                            break;
                        }
                        // Writes a tab (4 spaces) to the buffer
                        case ConsoleKey.Tab:
                        {
                            _cursor.WriteTab();
                            break; 
                        }
                        // Default write the char to the buffer if the key is a letter.
                        default:
                        {
                            if (Char.IsLetter(keyInfo.KeyChar) || Char.IsDigit(keyInfo.KeyChar) || Char.IsPunctuation(keyInfo.KeyChar))
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
