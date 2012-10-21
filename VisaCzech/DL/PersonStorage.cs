using System;
using VisaCzech.BL;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

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

        private static Person LoadPerson(string fileName)
        {
            try
            {
                Directory.CreateDirectory(DefaultPath);
                var fName = fileName.IndexOf(':') != -1 ? fileName : DefaultPath + fileName;
                var fs = File.Open(fName, FileMode.Open);
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

        public static Person LoadPerson(ref Person person)
        {
            return person = LoadPerson(person.Id + ".xml");
        }

        private static bool SavePerson(Person person, string fileName)
        {
            try
            {
                Directory.CreateDirectory(DefaultPath);
                var fName = fileName.IndexOf(':') != -1 ? fileName : DefaultPath + fileName;
                var fs = File.Create(fName);
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
            var fileName = string.Format("{0}.xml", person.Id);
            return SavePerson(person, fileName);
        }

        public static IEnumerable<Person> LoadAllPersons()
        {
            IEnumerable<string> files;
            try
            {
                files = Directory.EnumerateFiles(DefaultPath, "*.xml", SearchOption.TopDirectoryOnly);
            }
            catch
            {
                yield break;
            }
            foreach (var file in files)
            {
                Person person;
                try
                {
                    person = LoadPerson(file);
                }
                catch (Exception)
                {
                    continue;
                }
                yield return person;
            }
        }

        public static void DeletePerson(string fileName)
        {
            try
            {
                Directory.CreateDirectory(DefaultPath);
                var fName = fileName.IndexOf(':') != -1 ? fileName : DefaultPath + fileName;
                File.Delete(fName);
            }
            catch 
            {
                
            }
        }

        public static void DeletePerson(Person person)
        {
            var fileName = string.Format("{0}.xml", person.Id);
            DeletePerson(fileName);
        }
    }
}
