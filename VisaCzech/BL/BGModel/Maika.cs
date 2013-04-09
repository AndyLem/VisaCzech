using System.Xml.Serialization;

namespace VisaCzech.BL.BGModel
{
    /// <summary>
    /// Mother’s  data
    /// </summary>
    public class Maika
    {
        [XmlElement(ElementName = "d_maika_row")]
        public MaikaRow maikaRow = new MaikaRow();
    }

    public class MaikaRow
    {
        /// <summary>
        /// Surname/s (family name/s)
        /// Varchar(100) Latin capital letters only (A-Z) and spaces between  surnames. 
        /// Cyrillic could be used if father is citizen of country which uses Cyrillic as well.
        /// </summary>
        [Position(GroupName = "Мать", ColSpan = 2, Desc = "Фамилия")]
        public string dc_famil;

        /// <summary>
        /// First Names (Given names)
        /// Varchar(100) Latin capital letters only (A-Z) and spaces between names. 
        /// Cyrillic could be used if father is citizen of country which uses Cyrillic as well.
        /// </summary>
        [Position(GroupName = "Мать", ColSpan = 2, Desc = "Имена")]
        public string dc_imena;
    }
}
