using System;
using VisaCzech.BL;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace VisaCzech.DL
{
    public sealed class PersonStorage : Storage<Person>
    {
        private static PersonStorage _instance;
        public static PersonStorage Instance
        {
            get { return _instance ?? (_instance = new PersonStorage()); }
        }

        private PersonStorage()
        {
            _dirName = "Persons\\";
        }
    }
}
