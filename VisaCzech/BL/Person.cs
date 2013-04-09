﻿using System;
using System.Drawing;
using System.Xml.Serialization;
using VisaCzech.BL.CognitiveScanner;
using VisaCzech.BL.ObjFramework.ObjectContainerLinker;
using VisaCzech.BL.WordFiller;

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
        [Field(FieldName = "IN_SurName")]
        public string Surname = string.Empty;
        
        [String(TemplateString = "@@2_FAMILIA_ROJD", ValidationFuncName = "CheckSurname2")]
        [Link(ControlName = "surname2")]
        [Field(FieldName = "IN_SurName")]
        public string SurnameAtBirth = string.Empty;

        [String(TemplateString = "@@3_IMYA")]
        [Link(ControlName = "name")]
        [Field(FieldName = "IN_Name")]
        public string Name = string.Empty;

        [String(TemplateString = "@@4_DATA_ROJD")]
        [Link(ControlName = "birthDate")]
        [Field(FieldName = "IN_BirthDate")]
        public string BirthDate = string.Empty;

        [String(TemplateString = "@@5_PLACE_BIRTH")]
        [Link(ControlName = "birthPlace")]
        public string BirthPlace = "Minsk";

        [String(TemplateString = "@@6_STRANA_ROJD")]
        [Link(ControlName = "birthCountry")]
        [Field(FieldName = "IN_CNT")]
        public string BirthCountry = "BLR";

        [String(TemplateString = "@@7_GRAJDANSTVO")]
        [Link(ControlName = "citizen", LinkActionName = "CitizenChanged")]
        public string Citizenship = "BLR";

        [String(TemplateString = "@@7__GRAJDANSTVO_ROJD", ValidationFuncName = "CheckCitizenship")]
        [Link(ControlName = "birthCitizen")]
        public string BirthCitizenship = "BLR";

        [Link(ControlName = "sex", LinkActionName = "SexChanged", AllowFillComboBox = false)]
        [Field(FieldName = "IN_Sex")]
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
        [Field(FieldName = "IN_KOD")]
        public string PersonalId = string.Empty;

        [Link(ControlName = "docType", LinkActionName = "DocTypeChanged", AllowFillComboBox = false)]
        public string DocumentTypeValue = "Паспорт";
        [Enum(TemplateString = "@12_", EnumValues = 7)]
        public DocType DocumentType = DocType.Passport;
        [String(TemplateString = "@@12_OTHER_DOCTYPE")]
        [Link(ControlName = "otherDocType")]
        public string OtherDocumentType = string.Empty;

        [String(TemplateString = "@@13_SERIYA_NUMBER_PASSPORT")]
        [Link(ControlName = "docNumber")]
        [Field(FieldName = "IN_SerNum")]
        public string DocumentNumber = string.Empty;

        [String(TemplateString = "@@14_DATA_VIDACHI")]
        [Link(ControlName = "docIssued")]
        [Field(FieldName = "IssuedDate")]
        public string DocumentIssuedDate = string.Empty;

        [String(TemplateString = "@@15_DEYSTVIT_DO")]
        [Link(ControlName = "docValid")]
        [Field(FieldName = "IN_Expiry")]
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
            _image = person._image;
            _thumbnail = person._thumbnail;
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

        /*  BG XML Fields */


        public string hdr_kscreated { get; set; }

        public string hdr_regnom { get; set; }

        public string hdr_user { get; set; }

        public string AddressCountry { get; set; }

        public string AddressCity { get; set; }

        public string AddressStreet { get; set; }

        public string AddressZipCode { get; set; }

        public string Email { get; set; }

        public string WorkName { get; set; }

        public string WorkCity { get; set; }

        public string WorkCountry { get; set; }

        public string WorkAddress { get; set; }

        public string WorkZip { get; set; }

        public string WorkPhoneNumber { get; set; }

        public string WorkFaxNumber { get; set; }

        public string WorkEmail { get; set; }

        public string FatherName { get; set; }

        public string FatherSurname { get; set; }

        public string MotherName { get; set; }

        public string MotherSurname { get; set; }

        public string SpouseSurname { get; set; }

        public string SpouseSurnameAtBirth { get; set; }

        public string SpouseName { get; set; }

        public string SpouseBirthDate { get; set; }

        public string SpouseBirthCountry { get; set; }

        public string SpouseBirthCity { get; set; }

        public string VisaType { get; set; }

        public string NumberOfEntries { get; set; }

        public string ProcessingSpeed { get; set; }

        public string MultiVisaPeriod { get; set; }

        public object Purpose { get; set; }

        public string OtherPurpose { get; set; }

        public object DateOfApply { get; set; }

        public bool Gratis { get; set; }

        public string TransitDestinationPermit { get; set; }

        public string Fee { get; set; }

        public string FeeCurrency { get; set; }

        public string DestinationCountry { get; set; }

        public string DestinationCity { get; set; }

        public string BorderOfFirstEntry { get; set; }

        public string BorderCheckpoint { get; set; }

        public string TransitRoute { get; set; }

        public string VisaAdditionalInfo { get; set; }

        public object HostType { get; set; }

        public string InvitationNumber { get; set; }

        public string HostPersonName { get; set; }

        public string HostPersonSurname { get; set; }

        public string HostPersonCitizenship { get; set; }

        public string HostPersonID { get; set; }

        public string HostPersonCountry { get; set; }

        public string HostPersonCity { get; set; }

        public string HostPersonZipCode { get; set; }

        public string HostPersonAddress { get; set; }

        public string HostPersonPhone { get; set; }

        public string HostPersonFax { get; set; }

        public string HostPersonEmail { get; set; }

        public string HostCompanyID { get; set; }

        public string HostCompanyCountry { get; set; }

        public string HostCompanyCity { get; set; }

        public string HostCompanyZipCode { get; set; }

        public string HostCompanyAddress { get; set; }

        public string HotelFax { get; set; }

        public string HostCompanyEmail { get; set; }

        public bool OrganizedTourism { get; set; }

        public string VoucherNumber { get; set; }

        public object VoucherValidFrom { get; set; }

        public object VoucherValidTo { get; set; }

        public string VoucherIssuedBy { get; set; }

        public string HostCompanyName { get; set; }

        public string HostCompanyPhone { get; set; }

        public string Visa1Country { get; set; }

        public string Visa1Type { get; set; }

        public string Visa1Number { get; set; }

        public string Visa1NumberOfEntries { get; set; }


        public string Visa2Country { get; set; }

        public string Visa2Type { get; set; }

        public string Visa2Number { get; set; }

        public string Visa2NumberOfEntries { get; set; }

        public string Visa3Country { get; set; }

        public string Visa3Type { get; set; }

        public string Visa3Number { get; set; }

        public string Visa3NumberOfEntries { get; set; }
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
        ForeignerPassport,
        DiplomaticPassport,
        WorkPassport,
        OfficialPassport,
        SpecialPassport,
        Other
    }
}
