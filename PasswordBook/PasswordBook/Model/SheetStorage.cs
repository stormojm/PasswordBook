using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace PasswordBook.Model.Encryption
{
    public class SheetStorage
    {        
        public static string GetFileLocation()
        {
            return GetOrCreateDirectory();
        }

        private static string GetOrCreateDirectory()
        {
            string directoryName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PasswordBook");

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            return directoryName;
        }

        private static string GetFileName()
        {
            string directoryName = GetOrCreateDirectory();
            return Path.Combine(directoryName, "sheet.txt");
        }
    }
}
