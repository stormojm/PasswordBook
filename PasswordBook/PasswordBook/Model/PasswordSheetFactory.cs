using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PasswordBook.Model.Encryption;
using System.IO;

namespace PasswordBook.Model
{
    public static class PasswordSheetFactory
    {
        public static PasswordSheet Get()
        {
            string fileLocation = SheetStorage.GetFileLocation();
            if (File.Exists(fileLocation))
            {
                var decryptor = new SheetDecryptor("owatonna", fileLocation);
                PasswordSheet sheet = decryptor.DecryptFile();

                if (sheet != null)
                {
                    return sheet;
                }
            }

            return new PasswordSheet();
        }
    }
}
