using System;

namespace ConsoleEditor.Keyboard
{
    internal class Cursor
    {
        public int Row { get; set; }
        public int Column { get; set; }


        public void Display() 
        {
            Console.SetCursorPosition(Column, Row);
        }


        // Method to move the cursor down.
        public void MoveDown() 
        {
            Row++;
        }


        // Method to move the cursor left.
        public void MoveLeft() 
        {
            Column--;
        }


        // Method to move the cursor right.
        public void MoveRight() 
        {
            Column++;
        }


        // Method to move the cursor up.
        public void MoveUp() 
        {
            Row--;
        }

    }
}
