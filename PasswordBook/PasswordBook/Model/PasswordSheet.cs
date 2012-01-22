using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PasswordBook.Contracts;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace PasswordBook.Model
{
    public class PasswordSheet : IPasswordSheet, IXmlSerializable
    {
        private object _syncLock = new object();
        private Dictionary<Guid, PasswordEntry> _dictionary;

        public PasswordSheet()
        {
            _dictionary = new Dictionary<Guid, PasswordEntry>();
        }

        public void Create(PasswordEntry entry)
        {
            lock (_syncLock)
            {
                _dictionary.Add(entry.Id, entry);
            }
        }

        public PasswordEntry Get(Guid id)
        {
            lock (_syncLock)
            {
                PasswordEntry entry;
                if (_dictionary.TryGetValue(id, out entry))
                {
                    return new PasswordEntry(entry);
                }
            }

            return null;
        }

        public IEnumerable<PasswordEntry> GetAll()
        {
            lock (_syncLock)
            {
                List<PasswordEntry> entries = new List<PasswordEntry>();
                foreach (PasswordEntry entry in _dictionary.Values)
                {
                    entries.Add(new PasswordEntry(entry));
                }

                return entries;
            }
        }

        public void Update(Guid id, PasswordEntry newData)
        {
            lock (_syncLock)
            {
                PasswordEntry entry;
                if (_dictionary.TryGetValue(id, out entry))
                {
                    entry.Title = newData.Title;
                    entry.UserName = newData.UserName;
                    entry.Password = newData.Password;
                }
            }
        }

        public void Delete(Guid id)
        {
            lock (_syncLock)
            {
                _dictionary.Remove(id);
            }
        }

        #region XML Serialization

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;
            if (wasEmpty) return;
            reader.Read();

            var passwordEntrySerialization = new XmlSerializer(typeof(PasswordEntry));

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                var entry = (PasswordEntry)passwordEntrySerialization.Deserialize(reader);

                _dictionary.Add(entry.Id, entry);
                reader.MoveToContent();
            }

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            var passwordEntrySerialization = new XmlSerializer(typeof(PasswordEntry));

            foreach (PasswordEntry entry in _dictionary.Values)
            {
                passwordEntrySerialization.Serialize(writer, entry);
            }
        }

        #endregion
    }
}
