using System;
using VisaCzech.BL;
using System.IO;
using System.Xml.Serialization;

namespace VisaCzech.DL
{
    public sealed class PersonStorage
    {
        private static string _defaultPath;
        public static string DefaultPath
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultPath))
                {
                    _defaultPath = AppDomain.CurrentDomain.BaseDirectory + "Persons\\";
                }
                return _defaultPath;
            }
        }

        private static PersonStorage _instance;

        public static PersonStorage Instance
        {
            get { return _instance ?? (_instance = new PersonStorage()); }
        }

        private PersonStorage()
        {
            
        }

        public static Person LoadPerson(string fileName)
        {
            try
            {
                var fs = File.Open(DefaultPath+fileName, FileMode.Open);
                var ser = new XmlSerializer(typeof(Person));
                var person = (Person)ser.Deserialize(fs);
                fs.Close();
                return person;
            }
            catch
            {
                return null;
            }
        }

        public static bool SavePerson(Person person, string fileName)
        {
            try
            {
                var fs = File.Create(DefaultPath+fileName);
                var ser = new XmlSerializer(typeof(Person));
                ser.Serialize(fs, person);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SavePerson(Person person)
        {
            var fileName = string.Format("{0} {1}.xml", person.Surname, person.Name);
            return SavePerson(person, fileName);
        }
    }
}
