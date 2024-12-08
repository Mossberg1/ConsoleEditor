using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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


        public void MoveDown() 
        {
            Row++;
        }


        public void MoveLeft() 
        {
            Column--;
        }


        public void MoveRight() 
        {
            Column++;
        }


        public void MoveUp() 
        {
            Row--;
        }

    }
}
