using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace PasswordBook.Model.Encryption
{
    public class SheetEncryptor
    {
        private string _password;
        private string _fileName;

        public SheetEncryptor(string password, string fileName)
        {
            _password = password;
            _fileName = fileName;
        }

        private PasswordDeriveBytes GetPasswordBytes()
        {
            byte[] salt = Encoding.UTF8.GetBytes(_password.Length.ToString());
            return new PasswordDeriveBytes(_password, salt);
        }

        public void EncryptFile(PasswordSheet sheet)
        {
            FileStream stream = null;
            PasswordDeriveBytes secretKey = null;
            RijndaelManaged rijndaelCipher = null;
            ICryptoTransform encryptor = null;
            CryptoStream cryptoStream = null;

            try
            {
                stream = new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.Write);
                stream.SetLength(0);

                secretKey = GetPasswordBytes();

                rijndaelCipher = new RijndaelManaged();
                encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
                
                // Defines a stream that links data streams to cryptographic transformations
                cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);

                byte[] data = null;
                using (var sheetStream = new MemoryStream())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(PasswordSheet));
                    serializer.Serialize(sheetStream, sheet);

                    data = sheetStream.GetBuffer();
                }


                cryptoStream.Write(data, 0, data.Length);
                
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            finally
            {
                if (secretKey != null)
                {
                    secretKey.Dispose();
                }

                if (rijndaelCipher != null)
                {
                    rijndaelCipher.Dispose();
                }

                if (cryptoStream != null)
                {
                    cryptoStream.Dispose();
                }
            }
        }
    }
}
