using System;
using System.Xml;

namespace VisaCzech.BL.ScannerXmlParser
{
    public static class ScannerXmlParser
    {
        public static Person GetPerson(string[] xmlFileNames)
        {
            var person = new Person();
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

        private static void FillPerson(Person person, XmlDocument dom)
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
                    var mrz = TryFillField(pageNode, "MRZ", string.Empty);
                    if (!string.IsNullOrEmpty(mrz))
                    {
                        var mrzData = ParseMrz(mrz);
                        if (mrzData != null)
                        {
                            if (mrzData.Name.Length > person.Name.Length)
                                person.Name = mrzData.Name;
                            if (mrzData.Surname.Length > person.Surname.Length)
                                person.Surname = mrzData.Surname;
                            if (mrzData.DateOfBirth.Length >= person.BirthDate.Length)
                                person.BirthDate = mrzData.DateOfBirth;
                            if (mrzData.DocNumber.Length > person.DocumentNumber.Length)
                                person.DocumentNumber = mrzData.DocNumber;
                            if (mrzData.PersonalId.Length > person.PersonalId.Length)
                                person.PersonalId = mrzData.PersonalId;
                            person.Sex = mrzData.Sex == "M" ? Sex.Male : Sex.Female;

                            //TODO: Продумать, как обойтись без констант
                            switch (person.Sex)
                            {
                                case Sex.Male:
                                    person.SexValue = "Мужской";
                                    break;
                                case Sex.Female:
                                    person.SexValue = "Женский";
                                    break;
                            }
                        }
                    }

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
            var photoNode = pageNode.SelectSingleNode("IN_FOTO");
            if ((photoNode != null)  && (imageNode != null)) 
            try
            {
                person.Image = ImageConverter.ConvertBase64ToImage(imageNode.InnerText);
                if (photoNode.Attributes != null && photoNode.Attributes["ImgRect"] != null)
                {
                    var rectStr = photoNode.Attributes["ImgRect"].Value;
                    rectStr = rectStr.Trim().Remove(rectStr.Length - 1).Remove(0, 1);
                    var w = rectStr.Split(',');
                    if (w.Length == 4)
                    {
                        try
                        {
                            var rect = new System.Drawing.Rectangle(int.Parse(w[0]), int.Parse(w[1]), int.Parse(w[2]), int.Parse(w[3]));
                            rect.Height /= 2;
                            person.Image = ImageConverter.CropImage(person.Image, rect);
                        }
                        catch (Exception)
                        {

                        }
                        
                    }
                }
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        private static MrzData ParseMrz(string mrzStr)
        {
            var words = mrzStr.Split(new[] {'"'}, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length != 4)
                return null;
            for (var i = 0; i < words.Length; i++) words[i] = words[i].Trim();
            if (!words[1].StartsWith("BLR")) return null;       // не белорусский паспорт, MRZ зона неизвестна
            try
            {
                var bd = words[3].Substring(13, 6);
                var yearStr = bd.Substring(0, 2);
                var monthStr = bd.Substring(2, 2);
                var dayStr = bd.Substring(4, 2);
                var year = int.Parse(yearStr);
                if (year > 20) yearStr = "19" + yearStr;
                else yearStr = "20" + yearStr;                
                var data = new MrzData
                               {
                                   Surname = words[1].Remove(0, 3),
                                   Name = words[2],
                                   DocNumber = words[3].Substring(0, 9),
                                   Sex = words[3].Substring(20, 1),
                                   PersonalId = words[3].Substring(28, 14),
                                   DateOfBirth = dayStr + monthStr + yearStr
                               };
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string TryFillDate(XmlNode rootNode, string nodeName, string defaultValue)
        {
            var str = TryFillField(rootNode, nodeName, string.Empty);
            return !string.IsNullOrEmpty(str) ? 
                str.Replace(".", "") :
                defaultValue;
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
