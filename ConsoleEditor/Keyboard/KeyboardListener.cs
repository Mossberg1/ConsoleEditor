using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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


        public KeyboardListener(List<List<char>> buffer, Cursor cursor, AutoResetEvent autoResetEvent) 
        { 
            _autoResetEvent = autoResetEvent;
            _buffer = buffer;
            _cursor = cursor;
        }


        public void Listen() 
        { 
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
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
                    default:
                    {
                        return;
                    }
                }

                if (_autoResetEvent != null)
                {
                    _autoResetEvent.Set();
                }
            }
        }


        private void MoveCursorUp() 
        { 
            if (_cursor.Row > 0)
            {
                _cursor.Row--;
            }
        }


        private void MoveCursorLeft() 
        {
            if (_cursor.Column > 0)
            {
                _cursor.Column--;
            }
        }


        private void MoveCursorRight() 
        {
            if (_cursor.Column < _buffer[_cursor.Row].Count - 1)
            {
                _cursor.Column++;
            }
        }


        private void MoveCursorDown() 
        { 
            if (_cursor.Row < _buffer.Count - 1)
            {
                _cursor.Row++;
            }
        }
    }
}
