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
            FileStream stream = null;
            StreamReader crStreamReader = null;
            RijndaelManaged cryptic = null;
            PasswordDeriveBytes secretKey = null;

            try
            {
                stream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);

                cryptic = new RijndaelManaged();

                secretKey = GetPasswordBytes();

                ICryptoTransform decryptor = cryptic.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
                var crStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read);
                crStreamReader = new StreamReader(crStream);

                XmlSerializer serializer = new XmlSerializer(typeof(PasswordSheet));
                return (PasswordSheet)serializer.Deserialize(crStream);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (secretKey != null)
                {
                    secretKey.Dispose();
                }

                if (cryptic != null)
                {
                    cryptic.Dispose();
                }

                if (crStreamReader != null)
                {
                    crStreamReader.Dispose();
                }

                if (stream != null)
                {
                    stream.Dispose();
                }
            }

            return null;
        }
    }
}
