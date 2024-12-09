using System;
using System.Collections.Generic;
using System.Threading;

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

                if (keyInfo.Modifiers != 0 && ConsoleModifiers.Control != 0)
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
                else
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.DownArrow:
                        {
                            MoveCursorDown();
                            break;
                        }
                        case ConsoleKey.LeftArrow:
                        {
                            MoveCursorLeft();
                            break;
                        }
                        case ConsoleKey.RightArrow:
                        {
                            MoveCursorRight();
                            break;
                        }
                        case ConsoleKey.UpArrow:
                        {
                            MoveCursorUp();
                            break;
                        }
                        case ConsoleKey.Backspace:
                            RemoveChar();
                            break;
                        default:
                        {
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


        // Method to move cursor up
        private void MoveCursorUp() 
        { 
            if (_cursor.Row > 0)
            {
                _cursor.MoveUp();
            }
        }


        // Method to move cursor left.
        private void MoveCursorLeft() 
        {
            if (_cursor.Column > 0)
            {
                _cursor.MoveLeft();
            }
        }


        // Method to move cursor right.
        private void MoveCursorRight() 
        {
            if (_cursor.Column < _buffer[_cursor.Row].Count - 1)
            {
                _cursor.MoveRight();
            }
        }


        // Method to move cursor down.
        private void MoveCursorDown() 
        { 
            if (_cursor.Row < _buffer.Count - 1)
            {
                _cursor.MoveDown();
            }
        }


        // Method to remove a char from the buffer.
        private void RemoveChar()
        {
            if (_cursor.Column - 1 >= 0)
            {
                _buffer[_cursor.Row].RemoveAt(_cursor.Column - 1);
            }
            else if (_cursor.Row - 1 >= 0)
            {
                //_buffer[_cursor.Row - 1].RemoveAt(_buffer[_cursor.Row - 1].Count - 1);
                var previousRow = _buffer[_cursor.Row - 1];
                var currentRow = _buffer[_cursor.Row];
                previousRow.RemoveAt(previousRow.Count - 1);
                previousRow.AddRange(currentRow);
                _buffer.RemoveAt(_cursor.Row);
                _cursor.MoveUp();
                _cursor.Column = previousRow.Count;
            }

            MoveCursorLeft();
        }
    }
}
