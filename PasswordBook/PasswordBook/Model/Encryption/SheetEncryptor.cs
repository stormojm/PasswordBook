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
            CryptoStream crStream = null;
            RijndaelManaged cryptic = null;
            MemoryStream movieStream = null;
            PasswordDeriveBytes secretKey = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PasswordSheet));
                stream = new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.Write);

                cryptic = new RijndaelManaged();

                secretKey = GetPasswordBytes();

                ICryptoTransform encryptor = cryptic.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
                crStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);

                movieStream = new MemoryStream();
                serializer.Serialize(movieStream, sheet);

                byte[] data = movieStream.GetBuffer();
                crStream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (secretKey != null)
                {
                    secretKey.Dispose();
                }

                if (movieStream != null)
                {
                    movieStream.Dispose();
                }

                if (cryptic != null)
                {
                    cryptic.Dispose();
                }

                if (crStream != null)
                {
                    crStream.Dispose();
                }

                if (stream != null)
                {
                    stream.Dispose();
                }
            }
        }
    }
}
