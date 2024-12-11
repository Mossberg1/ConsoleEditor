using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleEditor.FileManagement
{
    internal class FileHandler
    {

        private Cursor _cursor;

        public List<List<char>> FileBuffer;
        public string FilePath;


        public FileHandler(string fp)
        {
            FileBuffer = new List<List<char>>();
            _cursor = new Cursor(FileBuffer);
            FilePath = fp;
        }


        // Method to render the buffer to the console.
        public void DisplayBuffer() 
        {
            Console.Clear();
            for (int row = 0; row < FileBuffer.Count; row++)
            {
                for (int col = 0; col < FileBuffer[row].Count; col++)
                {
                    Console.Write(FileBuffer[row][col]);
                }
            }

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nCTRL + Q = Close | CTRL + W = Write | CTRL + Z = Undo");
            Console.ResetColor();

            _cursor.Set();
        }


        // Method to get a refrence to the cursor, to pass to other constructors. (Maybe remove and make cursor public instead)
        public ref Cursor GetCursor() 
        {
            return ref _cursor;
        }


        // Method to read the file into memory.
        public int Read() 
        {
            try
            {
                using (var sr = new StreamReader(FilePath))
                {
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var row = new List<char>(line);
                        row.Add('\n');
                        FileBuffer.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return 0;
        }


        // Method to write file to disk.
        public int Write() 
        {
            try
            {
                using (var sw = new StreamWriter(FilePath))
                {
                    foreach (var row in FileBuffer)
                    {
                        foreach (var col in row)
                        {
                            sw.Write(col);
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                throw;
            }  

            return 0;
        }
    }
}
