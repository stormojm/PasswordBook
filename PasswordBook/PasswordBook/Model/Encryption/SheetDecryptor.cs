using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace PasswordBook.Model.Encryption
{
    public class SheetDecryptor
    {
        private string _password;
        private string _fileName;

        public SheetDecryptor(string password, string fileName)
        {
            _password = password;
            _fileName = fileName;
        }

        private PasswordDeriveBytes GetPasswordBytes()
        {
            byte[] salt = Encoding.UTF8.GetBytes(_password.Length.ToString());
            return new PasswordDeriveBytes(_password, salt);
        }

        public PasswordSheet DecryptFile()
        {
            PasswordSheet decryptedData = null;
            FileStream stream = null;
            PasswordDeriveBytes secretKey = null;
            RijndaelManaged rijndaelCipher = null;
            ICryptoTransform decryptor = null;
            CryptoStream cryptoStream = null;

            try
            {
                rijndaelCipher = new RijndaelManaged();

                // Making of the key for decryption
                secretKey = GetPasswordBytes();

                // Creates a symmetric Rijndael decryptor object.
                decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
                stream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
                
                // Defines the cryptographics stream for decryption.THe stream contains decrpted data
                cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read);

                XmlSerializer serializer = new XmlSerializer(typeof(PasswordSheet));
                decryptedData = (PasswordSheet)serializer.Deserialize(cryptoStream);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }

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

            return decryptedData;
        }
    }
}
