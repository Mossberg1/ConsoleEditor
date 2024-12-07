using System.Collections.Generic;
using System;
using ConsoleEditor.FileManagement;
using System.IO;

namespace ConsoleEditor
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var fileHandler = new FileHandler(@"C:\Users\willi\source\repos\ConsoleEditor\ConsoleEditor\test.txt");

            fileHandler.Open();

            fileHandler.DisplayBuffer();

        }
    }
}