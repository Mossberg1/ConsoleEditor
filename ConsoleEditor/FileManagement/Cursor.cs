﻿using System;
using System.Collections.Generic;

namespace ConsoleEditor.FileManagement
{
    internal class Cursor
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        private List<List<char>> _buffer;


        public Cursor(List<List<char>> buf)
        {
            _buffer = buf;
        }


        // Method to move the cursor down.
        public void MoveDown()
        {
            if (Row < _buffer.Count - 1)
            {
                Row++;
            }
        }


        // Method to move the cursor left.
        public void MoveLeft()
        {
            if (Column > 0)
            {
                Column--;
            }
        }


        // Method to move the cursor right.
        public void MoveRight()
        {
            if (Column < _buffer[Row].Count - 1)
            {
                Column++;
            }
        }


        // Method to move the cursor up.
        public void MoveUp()
        {
            if (Row > 0)
            {
                Row--;
            }
        }


        // Method to remove a char from the buffer.
        public void RemoveChar()
        {
            if (Column - 1 >= 0)
            {
                _buffer[Row].RemoveAt(Column - 1);
            }
            else if (Row - 1 >= 0)
            {
                var previousRow = _buffer[Row - 1];
                var currentRow = _buffer[Row];
                previousRow.RemoveAt(previousRow.Count - 1);
                previousRow.AddRange(currentRow);
                _buffer.RemoveAt(Row);
                MoveUp();
                Column = previousRow.Count;
            }

            MoveLeft();
        }


        // Method to set the cursors position.
        public void Set()
        {
            Console.SetCursorPosition(Column, Row);
        }


        // Method to write a char to the buffer.
        public void WriteChar(char ch)
        {
            _buffer[Row].Insert(Column, ch);
            MoveRight();
        }

    }
}