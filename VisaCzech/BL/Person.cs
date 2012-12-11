using VisaCzech.BL.ObjFramework.ObjectContainerLinker;
using System;
using VisaCzech.BL.WordFiller;
using System.Drawing;
using System.Xml.Serialization;

namespace VisaCzech.BL
{
    public class Person : ID
    {
        public Person()
        {
            Id = Guid.NewGuid().ToString();
            DateOfCreation = DateTime.Now;
        }

        [String(TemplateString = "@@1_FAMILIA")]
        [Link(ControlName = "surname", LinkActionName = "SurnameChanged")]
        public string Surname = string.Empty;
        
        [String(TemplateString = "@@2_FAMILIA_ROJD", ValidationFuncName = "CheckSurname2")]
        [Link(ControlName = "surname2")]
        public string SurnameAtBirth = string.Empty;

        [String(TemplateString = "@@3_IMYA")]
        [Link(ControlName = "name")]
        public string Name = string.Empty;

        [String(TemplateString = "@@4_DATA_ROJD")]
        [Link(ControlName = "birthDate")]
        public string BirthDate = string.Empty;

        [String(TemplateString = "@@5_PLACE_BIRTH")]
        [Link(ControlName = "birthPlace")]
        public string BirthPlace = "Minsk";

        [String(TemplateString = "@@6_STRANA_ROJD")]
        [Link(ControlName = "birthCountry")]
        public string BirthCountry = "BLR";

        [String(TemplateString = "@@7_GRAJDANSTVO")]
        [Link(ControlName = "citizen", LinkActionName = "CitizenChanged")]
        public string Citizenship = "BLR";

        [String(TemplateString = "@@7__GRAJDANSTVO_ROJD", ValidationFuncName = "CheckCitizenship")]
        [Link(ControlName = "birthCitizen")]
        public string BirthCitizenship = "BLR";

        [Link(ControlName = "sex", LinkActionName = "SexChanged", AllowFillComboBox = false)]
        public string SexValue = "Мужской";
        [Enum(TemplateString = "@8_", EnumValues = 2)]
        public Sex Sex = Sex.Male;

        [Link(ControlName = "family", LinkActionName = "FamilyChanged", AllowFillComboBox = false)]
        public string StatusValue = "Холост/не замужем";
        [Enum(TemplateString = "@9_", EnumValues = 6)]
        public Status Status = Status.Single;
        [String(TemplateString = "@@9_OTHER_STATUS")]
        [Link(ControlName = "otherFamily")]
        public string OtherStatus = string.Empty;

        [String(TemplateString = "@@10_RODITEL")]
        [Link(ControlName = "parent")]
        public string Parent = string.Empty;

        [String(TemplateString = "@@11_ID_PASSPORT")]
        [Link(ControlName = "personalId")]
        public string PersonalId = string.Empty;

        [Link(ControlName = "docType", LinkActionName = "DocTypeChanged", AllowFillComboBox = false)]
        public string DocumentTypeValue = "Паспорт";
        [Enum(TemplateString = "@12_", EnumValues = 6)]
        public DocType DocumentType = DocType.Passport;
        [String(TemplateString = "@@12_OTHER_DOCTYPE")]
        [Link(ControlName = "otherDocType")]
        public string OtherDocumentType = string.Empty;

        [String(TemplateString = "@@13_SERIYA_NUMBER_PASSPORT")]
        [Link(ControlName = "docNumber")]
        public string DocumentNumber = string.Empty;

        [String(TemplateString = "@@14_DATA_VIDACHI")]
        [Link(ControlName = "docIssued")]
        public string DocumentIssuedDate = string.Empty;

        [String(TemplateString = "@@15_DEYSTVIT_DO")]
        [Link(ControlName = "docValid")]
        public string DocumentValidDate = string.Empty;

        [String(TemplateString = "@@16_VIDAN_PASS")]
        [Link(ControlName = "docIssuedBy")]
        public string DocumentIssuedBy = "BLR";

        [String(TemplateString = "@@17_DOM_ADRES_MILO")]
        [Link(ControlName = "homeAddress")]
        public string AddressAndEmail = string.Empty;

        [String(TemplateString = "@@17_TEL_DOM")]
        [Link(ControlName = "homePhone")]
        public string PhoneNumber = string.Empty;

        [String(TemplateString = "@@19_PROF_DEYATELNOST")]
        [Link(ControlName = "profession")]
        public string Profession = string.Empty;

        [String(TemplateString = "@@20_RABOTA_SHKOLA")]
        [Link(ControlName = "work")]
        public string WorkOrSchoolAddress = string.Empty;

        [String(TemplateString = "@@25_PRODOLJIT_DNEY")]
        [Link(ControlName = "duration", InitOnlyEmpty = true)]
        public string Duration = string.Empty;

        [Bool(TemplateString = "@26_")]
        [Link(ControlName = "visa1Enabled", LinkActionName = "Visa1EnabledChanged")]
        public bool Visa1Enabled = false;
        [Link(ControlName = "visa1From")]
        [String(TemplateString = "@@26_VISA1_OT", ValidationFuncName = "IsVisa1Checked")]
        public string Visa1From = string.Empty;
        [Link(ControlName = "visa1To")]
        [String(TemplateString = "@@26_VISA1_DO", ValidationFuncName = "IsVisa1Checked")]
        public string Visa1To = string.Empty;

        [Link(ControlName = "visa2Enabled", LinkActionName = "Visa2EnabledChanged")]
        public bool Visa2Enabled = false;
        [Link(ControlName = "visa2From")]
        [String(TemplateString = "@@26_VISA2_OT", ValidationFuncName = "IsVisa2Checked")]
        public string Visa2From = string.Empty;
        [Link(ControlName = "visa2To")]
        [String(TemplateString = "@@26_VISA2_DO", ValidationFuncName = "IsVisa2Checked")]
        public string Visa2To = string.Empty;

        [Link(ControlName = "visa3Enabled", LinkActionName = "Visa3EnabledChanged")]
        public bool Visa3Enabled = false;
        [Link(ControlName = "visa3From")]
        [String(TemplateString = "@@26_VISA3_OT", ValidationFuncName = "IsVisa3Checked")]
        public string Visa3From = string.Empty;
        [Link(ControlName = "visa3To")]
        [String(TemplateString = "@@26_VISA3_DO", ValidationFuncName = "IsVisa3Checked")]
        public string Visa3To = string.Empty;

        [String(TemplateString = "@@29_DATA_ZAEZDA")]
        [Link(ControlName = "visaStart", InitOnlyEmpty = true)]
        public string VisaStartDate = string.Empty;

        [String(TemplateString = "@@30_DATA_VIEZDA")]
        [Link(ControlName = "visaEnd", InitOnlyEmpty = true)]
        public string VisaEndDate = string.Empty;

        [String(TemplateString = "@@31_HOTEL_NAME")]
        [Link(ControlName = "hostHotel", InitOnlyEmpty = true)]
        public string HotelName = string.Empty;

        [String(TemplateString = "@@31_HOTEL_ADDRESS")]
        [Link(ControlName = "hostHotelAddress", InitOnlyEmpty = true)]
        public string HotelAddress = string.Empty;

        [String(TemplateString = "@@31_HOTEL_PHONE")]
        [Link(ControlName = "hostHotelPhone", InitOnlyEmpty = true)]
        public string HotelPhone = string.Empty;
        
        [String(TemplateString = "@@32_NAZVANIE_ADRES_COMPANY_PRIGLASHENIE")]
        [Link(ControlName = "host", InitOnlyEmpty = true)]
        public string HostNameAndAddress = string.Empty;

        [String(TemplateString = "@@32_TEL_PRIGL_COMPANY")]
        [Link(ControlName = "hostPhone", InitOnlyEmpty = true)]
        public string HostPhoneNumber = string.Empty;

        [String(TemplateString = "@@32_DANNIE_LICO_COMPANII_PRIGLASHENIE")]
        [Link(ControlName = "hostPerson", InitOnlyEmpty = true)]
        public string HostPersonNameAddressPhoneEmail = string.Empty;

        [String(TemplateString = "@@36_MESTO_SOSTAVLENIYA")]
        [Link(ControlName = "fillPlace", InitOnlyEmpty = true)]
        public string PlaceOfFilling = "Minsk";

        [String(TemplateString = "@@36_DATA_SOSTAVLENIYA")]
        [Link(ControlName = "fillDate")]
        public string DateOfFilling = string.Empty;

        public DateTime DateOfCreation = DateTime.Now;

        [XmlIgnore] 
        private Image _image;

        [XmlIgnore] 
        private Image _thumbnail;

        [XmlIgnore] 
        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                _thumbnail = _image == null ? null : ScannerXmlParser.ImageConverter.ResizeImage(_image, 32);
            }
        }

        [XmlIgnore] 
        public Image Thumbnail
        {
            get { return _thumbnail; }
        }

        public override string ToString()
        {
            return Name + " " + Surname;
        }

        public Person Merge(Person person, bool mergeId = false)
        {
            var tempId = Id;
            var fieldInfos = GetType().GetFields();
            foreach (var info in fieldInfos)
            {
                var val = info.GetValue(person);
                if (val == null) continue;
                if (string.IsNullOrEmpty(val.ToString())) continue;
                info.SetValue(this, val);
            }
            this._image = person._image;
            this._thumbnail = person._thumbnail;
            if (!mergeId)
                Id = tempId;
            return this;
        }

        public bool FilterOk(string filter)
        {
            var upperFilter = filter.ToUpper();
            return Name.ToUpper().Contains(upperFilter) || Surname.ToUpper().Contains(upperFilter) ||
                   DocumentNumber.ToUpper().Contains(upperFilter);
        }
    }

    public enum Status
    {
        Single = 0,
        Married, 
        Divorced,
        LivesAlone,
        Widow,
        Other
    }

    public enum Sex
    {
        Male = 0, 
        Female
    }

    public enum DocType
    {
        Passport = 0,
        DiplomaticPassport,
        WorkPassport,
        OfficialPassport,
        SpecialPassport,
        Other
    }
}
