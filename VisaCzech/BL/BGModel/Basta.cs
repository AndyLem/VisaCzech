using System.Xml.Serialization;

namespace VisaCzech.BL.BGModel
{
    /// <summary>
    /// Father’s data
    /// </summary>
    public class Basta
    {
        [XmlElement(ElementName = "d_basta_row")]
        public BastaRow bastaRow = new BastaRow();
    }

    public class BastaRow
    {
        /// <summary>
        /// Surname/s (family name/s)
        /// Varchar(100) Latin capital letters only (A-Z) and spaces between  surnames. 
        /// Cyrillic could be used if father is citizen of country which uses Cyrillic as well.
        /// </summary>
        [Position(Desc = "Фамилия", ColSpan = 2, GroupName = "Отец")]
        public string dc_famil;

        /// <summary>
        /// First Names (Given names)
        /// Varchar(100) Latin capital letters only (A-Z) and spaces between names. 
        /// Cyrillic could be used if father is citizen of country which uses Cyrillic as well.
        /// </summary>
        [Position(Desc="Имена", ColSpan = 2, GroupName="Отец")]
        public string dc_imena;
    }
}
