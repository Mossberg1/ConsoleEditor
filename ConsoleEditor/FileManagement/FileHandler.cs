﻿using ConsoleEditor.Keyboard;
using System;
using System.Collections.Generic;
using System.IO;

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

            _cursor.Set();
        }


        // Method to get a refrence to the cursor, to pass to other constructors. (Maybe remove and make cursor public instead)
        public ref Cursor GetCursor() 
        {
            return ref _cursor;
        }


        // Method to read the file into memory.
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
