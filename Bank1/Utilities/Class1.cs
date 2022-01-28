﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank1
{
    public static class FileLogger
    {
        internal static string folderName = @"Logs/";
        internal static string fileName = @"logfile.txt";
        internal static string fullName = Path.Combine(folderName, fileName);
        
        public static void WriteToLog(string logMessage)
        {
            string msg = DateTime.Now.ToString() + " " + logMessage;
            string[] msgArr = { msg };
            
            File.AppendAllLinesAsync(fileName, msgArr);

           
        }

        public static string ReadFromLog()
        {
            if (File.Exists(fileName))
            {
                return File.ReadAllText(fileName);
            }
            else { return "No log entries found."; }
        }
    }
}
