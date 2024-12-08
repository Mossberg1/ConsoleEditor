using ConsoleEditor.Keyboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEditor.FileManagement
{
    internal class FileHandler
    {

        private Cursor _cursor;

        public List<List<char>> FileBuffer;
        public string FilePath;


        public FileHandler(string fp)
        {
            _cursor = new Cursor();
            FileBuffer = new List<List<char>>();
            FilePath = fp;
        }


        public void DisplayBuffer() 
        {
            for (int row = 0; row < FileBuffer.Count; row++)
            {
                for (int col = 0; col < FileBuffer[row].Count; col++)
                {
                    Console.SetCursorPosition(col, row);
                    Console.Write(FileBuffer[row][col]);
                }
            }

            _cursor.Display();
        }


        public ref Cursor GetCursor() 
        {
            return ref _cursor;
        }


        public int Open() 
        {
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    var row = new List<char>(line);
                    row.Add('\n'); 
                    FileBuffer.Add(row);
                }
            }

            return 0;
        }
    }
}
