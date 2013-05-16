using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VisaCzech.DL
{
    public class CDContent
    {
        public string CDName;
        public int NumberOfFiles;

        [XmlArrayItem("OSUniqueID")]
        public List<string> ListOfFiles;
    }
}
