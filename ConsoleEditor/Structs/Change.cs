using ConsoleEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEditor.Structs
{
    internal struct Change
    {
        public int Column;
        public int Row;
        public CursorOperation Operation;
        public char Character;
    }
}
