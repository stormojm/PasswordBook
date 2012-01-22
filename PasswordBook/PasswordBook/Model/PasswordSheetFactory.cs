using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PasswordBook.Model.Encryption;
using System.IO;
using PasswordBook.Contracts;

namespace PasswordBook.Model
{
    public class PasswordSheetFactory : IPasswordSheetFactory
    {
        private string _password;
        private PasswordSheet _current;
        private object _currentLock = new object();

        private PasswordSheet CreateSheet()
        {
            string fileLocation = SheetStorage.GetFileLocation();
            if (File.Exists(fileLocation))
            {
                var decryptor = new SheetDecryptor(_password, fileLocation);
                PasswordSheet sheet = decryptor.DecryptFile();

                if (sheet != null)
                {
                    return sheet;
                }
            }

            return new PasswordSheet();
        }

        public bool LoadSheet(string password)
        {
            _password = password;

            try
            {
                lock (_currentLock)
                {
                    _current = CreateSheet();
                }

                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public IPasswordSheet Get()
        {
            if (String.IsNullOrEmpty(_password)) return null;

            lock (_currentLock)
            {
                return _current;
            }
        }


        public void SaveSheet()
        {
            if (String.IsNullOrEmpty(_password)) return;

            PasswordSheet sheet;
            lock (_currentLock)
            {
                if (_current == null) return;
                sheet = _current;
            }

            string fileLocation = SheetStorage.GetFileLocation();
            var encryptor = new SheetEncryptor(_password, fileLocation);
            encryptor.EncryptFile(sheet);
        }
    }
}
