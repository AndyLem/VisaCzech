using System;
using System.Xml;

namespace VisaCzech.BL.ScannerXmlParser
{
    public static class ScannerXmlParser
    {
        public static ScannedPerson GetPerson(string[] xmlFileNames)
        {
            var person = new ScannedPerson();
            foreach (var fileName in xmlFileNames)
            {
                var dom = new XmlDocument();
                try
                {
                    dom.Load(fileName);
                }
                catch (Exception)
                {
                    continue;
                }
                FillPerson(person, dom);
            }
            return person;
        }

        private static void FillPerson(ScannedPerson person, XmlDocument dom)
        {
            if (dom == null) return;
            if (dom.DocumentElement == null) return;
            if (dom.DocumentElement.Name != "Pack") return;
            var pageNode = dom.SelectSingleNode(@"//Page");
            if (pageNode == null) return;
            if (pageNode.Attributes == null) return;
            var pageTypeStr = pageNode.Attributes["type"].Value;
            switch (pageTypeStr)
            {
                case "InoPas_MRZ":
                    person.Surname = TryFillField(pageNode, "IN_SurName", person.Surname);
                    person.Name = TryFillField(pageNode, "IN_Name", person.Name);
                    person.DocumentNumber = TryFillField(pageNode, "IN_SerNum", person.DocumentNumber);
                    person.BirthDate = TryFillDate(pageNode, "IN_BirthDate", person.BirthDate);
                    var sex = TryFillField(pageNode, "IN_Sex", string.Empty);
                    if (!string.IsNullOrEmpty(sex)) person.Sex = sex.ToUpper() == "M" ? Sex.Male : Sex.Female;
                    person.DocumentValidDate = TryFillDate(pageNode, "IN_Expiry", person.DocumentValidDate);

                    var imei = TryFillField(pageNode, "IN_KOD", string.Empty);
                    if (string.IsNullOrEmpty(person.PersonalId) || imei.Length > person.PersonalId.Length)
                        person.PersonalId = imei;

                    person.Citizenship = TryFillField(pageNode, "IN_CNT", person.Citizenship);

                    break;
                case "Bel2":
                    person.BirthDate = TryFillField(pageNode, "DateOfBirth", person.BirthDate);
                    var birthPlaceStr = TryFillField(pageNode, "PlaceOfBirth", string.Empty);
                    var splittedBp = birthPlaceStr.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                    if (splittedBp.Length == 2)
                    {
                        if (splittedBp[1].ToUpper().EndsWith("МИНСК"))
                            person.BirthPlace = "Minsk";
                        if (splittedBp[0].Trim().ToUpper().Contains("БЕЛАРУСЬ"))
                            person.BirthCountry = "Belarus";
                    }
                    person.PersonalId = TryFillField(pageNode, "PersonID", person.PersonalId);
                    person.DocumentValidDate = TryFillDate(pageNode, "ValidDate", person.DocumentValidDate);
                    person.DocumentIssuedDate = TryFillDate(pageNode, "IssueDate", person.DocumentValidDate);
                    break;
            }
            var imageNode = pageNode.SelectSingleNode("image");
            if (imageNode == null) return;
            try
            {
                person.Image = ImageConverter.ConvertBase64ToImage(imageNode.InnerText);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        private static string TryFillDate(XmlNode rootNode, string nodeName, string defaultValue)
        {
            var str = TryFillField(rootNode, nodeName, string.Empty);
            return !string.IsNullOrEmpty(str) ? str.Replace('.', '-') : defaultValue;
        }

        private static string TryFillField(XmlNode rootNode, string nodeName, string defaultValue)
        {
            var node = rootNode.SelectSingleNode(nodeName);
            if (defaultValue == null) defaultValue = string.Empty;
            if (node == null) return defaultValue;
            return string.IsNullOrEmpty(node.InnerText) ? 
                defaultValue : 
                (node.InnerText.Length > defaultValue.Length ? 
                    node.InnerText : 
                    defaultValue);
        }
    }
}
