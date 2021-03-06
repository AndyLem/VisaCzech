﻿using System;
using System.Drawing;
using System.Xml.Serialization;
using VisaCzech.BL.CognitiveScanner;
using VisaCzech.BL.ObjFramework.ObjectContainerLinker;
using VisaCzech.BL.WordFiller;
using ImageConverter = VisaCzech.BL.ScannerXmlParser.ImageConverter;

namespace VisaCzech.BL
{
    public class Person : ID
    {
        public static readonly string NOT_VALID_DATE = "01.01.1800";

        public Person()
        {
            Id = Guid.NewGuid().ToString();
            DateOfCreation = DateTime.Now;
        }

        #region Personal Info

        [String(TemplateString = "@@1_FAMILIA")] [Link(ControlName = "surname", LinkActionName = "SurnameChanged")] [Field(FieldName = "IN_SurName")] 
        public string Surname = string.Empty;

        [String(TemplateString = "@@2_FAMILIA_ROJD", ValidationFuncName = "CheckSurname2")]
        [Link(ControlName = "surname2", HideForBG = true)]
        [Field(FieldName = "IN_SurName")] 
        public string SurnameAtBirth = string.Empty;

        [String(TemplateString = "@@3_IMYA")] [Link(ControlName = "name")] [Field(FieldName = "IN_Name")] 
        public string Name = string.Empty;

        [String(TemplateString = "@@4_DATA_ROJD")] 
        [Link(ControlName = "birthDate")] 
        [Field(FieldName = "IN_BirthDate")] 
        public string BirthDate = string.Empty;

        [String(TemplateString = "@@5_PLACE_BIRTH")] [Link(ControlName = "birthPlace")] 
        public string BirthPlace = "Minsk";

        [String(TemplateString = "@@6_STRANA_ROJD")] [Link(ControlName = "birthCountry")] [Field(FieldName = "IN_CNT")] 
        public string BirthCountry = "BLR";

        [String(TemplateString = "@@7_GRAJDANSTVO")] [Link(ControlName = "citizen", LinkActionName = "CitizenChanged")] 
        public string Citizenship = "BLR";

        [String(TemplateString = "@@7__GRAJDANSTVO_ROJD", ValidationFuncName = "CheckCitizenship")] [Link(ControlName = "birthCitizen")] 
        public string BirthCitizenship = "BLR";

        [Link(ControlName = "sex", LinkActionName = "SexChanged", AllowFillComboBox = false)] [Field(FieldName = "IN_Sex")] 
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

        #endregion

        [String(TemplateString = "@@10_RODITEL")]
        public string Parent = string.Empty;

        [String(TemplateString = "@@11_ID_PASSPORT")]
        [Link(ControlName = "personalId", HideForBG = true)]
        [Field(FieldName = "IN_KOD")]
        public string PersonalId = string.Empty;

        #region Document

        [Link(ControlName = "docType", LinkActionName = "DocTypeChanged", AllowFillComboBox = false)] public string
            DocumentTypeValue = "Паспорт";

        [Enum(TemplateString = "@12_", EnumValues = 7)] public DocType DocumentType = DocType.Passport;

        [String(TemplateString = "@@12_OTHER_DOCTYPE")] [Link(ControlName = "otherDocType")] public string
            OtherDocumentType = string.Empty;

        [String(TemplateString = "@@13_SERIYA_NUMBER_PASSPORT")]
        [Link(ControlName = "docNumber", LinkActionName = "SetApplicantID")]
        [Field(FieldName = "IN_SerNum")] 
        public string DocumentNumber = string.Empty;

        [String(TemplateString = "@@14_DATA_VIDACHI")] 
        [Link(ControlName = "docIssued")] 
        //[Field(FieldName = "IssuedDate")] 
        public string DocumentIssuedDate = NOT_VALID_DATE;

        [String(TemplateString = "@@15_DEYSTVIT_DO")] 
        [Link(ControlName = "docValid")] 
        [Field(FieldName = "IN_Expiry")] 
        public string DocumentValidDate = string.Empty;

        [String(TemplateString = "@@16_VIDAN_PASS")] [Link(ControlName = "docIssuedBy")] public string DocumentIssuedBy
            = "BLR";

        #endregion



        [String(TemplateString = "@@17_DOM_ADRES_MILO")]
        public string AddressAndEmail = string.Empty;       // Combines from Home[...] fields

        [String(TemplateString = "@@17_TEL_DOM")]
        [Link(ControlName = "homePhone", HideForBG = true)]
        public string PhoneNumber = string.Empty;

        [String(TemplateString = "@@19_PROF_DEYATELNOST")]
        [Link(ControlName = "profession", LinkActionName = "SetProfId")]
        public string Profession = "Другая";
        public int ProfessionId = 30;

        [String(TemplateString = "@@20_RABOTA_SHKOLA")]
        public string WorkOrSchoolAddress = string.Empty;

        [String(TemplateString = "@@25_PRODOLJIT_DNEY")]
        [Link(ControlName = "duration", InitOnlyEmpty = true)]
        public string Duration = string.Empty;

        #region Old visas


        [Bool(TemplateString = "@26_")] [Link(ControlName = "visa1Enabled", LinkActionName = "Visa1EnabledChanged")] public bool Visa1Enabled = false;

        [Link(ControlName = "visa1From")] [String(TemplateString = "@@26_VISA1_OT", ValidationFuncName = "IsVisa1Checked")] public string Visa1From =
            string.Empty;

        [Link(ControlName = "visa1To")] [String(TemplateString = "@@26_VISA1_DO", ValidationFuncName = "IsVisa1Checked")] public string Visa1To =
            string.Empty;

        [Link(ControlName = "visa2Enabled", LinkActionName = "Visa2EnabledChanged")] public bool Visa2Enabled = false;

        [Link(ControlName = "visa2From")] [String(TemplateString = "@@26_VISA2_OT", ValidationFuncName = "IsVisa2Checked")] public string Visa2From =
            string.Empty;

        [Link(ControlName = "visa2To")] [String(TemplateString = "@@26_VISA2_DO", ValidationFuncName = "IsVisa2Checked")] public string Visa2To =
            string.Empty;

        [Link(ControlName = "visa3Enabled", LinkActionName = "Visa3EnabledChanged")] public bool Visa3Enabled = false;

        [Link(ControlName = "visa3From")] [String(TemplateString = "@@26_VISA3_OT", ValidationFuncName = "IsVisa3Checked")] public string Visa3From =
            string.Empty;

        [Link(ControlName = "visa3To")] 
        [String(TemplateString = "@@26_VISA3_DO", ValidationFuncName = "IsVisa3Checked")] 
        public string Visa3To = string.Empty;

        [Link(ControlName = "visa1Country")]
        public string Visa1Country = string.Empty;

        [Link(ControlName = "visa1Type", LinkActionName = "SetVisa1Type")] 
        public string Visa1TypeValue = "Краткосрочная";
        public VisaType Visa1Type = VisaType.ShortStay;

        [Link(ControlName = "visa1Number")]
        public string Visa1Number = string.Empty;

        [Link(ControlName = "visa1Entries", LinkActionName = "SetVisa1Entries")]
        public string Visa1NumberOfEntriesValue = "Одно";
        public Entries Visa1NumberOfEntries = Entries.SingleEntry;

        [Link(ControlName = "visa2Country")]
        public string Visa2Country = string.Empty;

        [Link(ControlName = "visa2Type", LinkActionName = "SetVisa2Type")]
        public string Visa2TypeValue = "Краткосрочная";
        public VisaType Visa2Type = VisaType.ShortStay;

        [Link(ControlName = "visa2Number")]
        public string Visa2Number = string.Empty;

        [Link(ControlName = "visa2Entries", LinkActionName = "SetVisa2Entries")]
        public string Visa2NumberOfEntriesValue = "Одно";
        public Entries Visa2NumberOfEntries = Entries.SingleEntry;

        [Link(ControlName = "visa3Country")]
        public string Visa3Country = string.Empty;

        [Link(ControlName = "visa3Type", LinkActionName = "SetVisa3Type")]
        public string Visa3TypeValue = "Краткосрочная";
        public VisaType Visa3Type = VisaType.ShortStay;

        [Link(ControlName = "visa3Number")]
        public string Visa3Number = string.Empty;

        [Link(ControlName = "visa3Entries", LinkActionName = "SetVisa3Entries")]
        public string Visa3NumberOfEntriesValue = "Одно";
        public Entries Visa3NumberOfEntries = Entries.SingleEntry;

        #endregion
        
        [String(TemplateString = "@@36_MESTO_SOSTAVLENIYA")]
        [Link(ControlName = "fillPlace", InitOnlyEmpty = true, HideForBG = true)]
        public string PlaceOfFilling = "Minsk";

        [String(TemplateString = "@@36_DATA_SOSTAVLENIYA")]
        [Link(ControlName = "fillDate", HideForBG = true)]
        public string DateOfFilling = string.Empty;

        public DateTime DateOfCreation = DateTime.Now;

        #region Image

        [XmlIgnore] private Image _image;

        [XmlIgnore] private Image _thumbnail;

        [XmlIgnore]
        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                _thumbnail = _image == null ? null : ImageConverter.ResizeImage(_image, 32);
            }
        }

        [XmlIgnore]
        public Image Thumbnail
        {
            get { return _thumbnail; }
        }

        #endregion
        
        #region Methods

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

        public Person MergeBlanks(Person person)
        {
            var tempId = Id;
            var fieldInfos = GetType().GetFields();
            foreach (var info in fieldInfos)
            {
                if (string.IsNullOrEmpty(info.GetValue(this).ToString()))
                {
                    var val = info.GetValue(person);
                    if (val == null) continue;
                    if (string.IsNullOrEmpty(val.ToString())) continue;
                    info.SetValue(this, val);
                }
            }
            _image = person._image;
            _thumbnail = person._thumbnail;
            Id = tempId;
            return this;
        }

        public bool FilterOk(string filter)
        {
            var upperFilter = filter.ToUpper();
            return Name.ToUpper().Contains(upperFilter) || Surname.ToUpper().Contains(upperFilter) ||
                   DocumentNumber.ToUpper().Contains(upperFilter);
        }

        #endregion
        
        /*  BG XML Fields */

        #region BG Header

        [Link(ControlName = "ksCreated")] public string hdr_kscreated = "MIN";

        [Link(ControlName = "regNom")] public string hdr_regnom = string.Empty;

        [Link(ControlName = "user")] public string hdr_user = string.Empty;

        #endregion

        #region Home

        [Link(ControlName = "homeCountry", AllowFillComboBox = true, LinkActionName = "FillHomeAddress")] public string
            AddressCountry = string.Empty;

        [Link(ControlName = "homeCity", AllowFillComboBox = true, LinkActionName = "FillHomeAddress")] public string
            AddressCity = string.Empty;

        [Link(ControlName = "homeAddress", AllowFillComboBox = true, LinkActionName = "FillHomeAddress")] public string
            AddressStreet = string.Empty;

        [Link(ControlName = "homeZip", AllowFillComboBox = true, LinkActionName = "FillHomeAddress", HideForBG = true)]
        public string
            AddressZipCode = string.Empty;

        [Link(ControlName = "email", LinkActionName = "FillHomeAddress", HideForBG = true)]
        public string Email = string.Empty;

        #endregion
        
        #region Work

        [Link(ControlName = "workName", AllowFillComboBox = true, LinkActionName = "FillWorkAddress")] public string
            WorkName = string.Empty;

        [Link(ControlName = "workCity", AllowFillComboBox = true, LinkActionName = "FillWorkAddress")] public string
            WorkCity = string.Empty;

        [Link(ControlName = "workCountry", AllowFillComboBox = true, LinkActionName = "FillWorkAddress")] public string
            WorkCountry = string.Empty;

        [Link(ControlName = "workAddress", AllowFillComboBox = true, LinkActionName = "FillWorkAddress")] public string
            WorkAddress = string.Empty;

        [Link(ControlName = "workZip", AllowFillComboBox = true, LinkActionName = "FillWorkAddress", HideForBG = true)]
        public string
            WorkZip = string.Empty;

        [Link(ControlName = "workPhone", AllowFillComboBox = true, HideForBG = true)]
        public string WorkPhoneNumber = string.Empty;

        [Link(ControlName = "workFax", AllowFillComboBox = true, HideForBG = true)]
        public string WorkFaxNumber = string.Empty;

        [Link(ControlName = "workEmail", AllowFillComboBox = true, LinkActionName = "FillWorkAddress", HideForBG = true)] 
        public string WorkEmail = string.Empty;

        #endregion
        
        #region Father, mother, spouse

        [Link(ControlName = "fatherName", LinkActionName = "FillParents", HideForBG = true)]
        public string FatherName = string.Empty;

        [Link(ControlName = "fatherSurname", LinkActionName = "FillParents", HideForBG = true)]
        public string FatherSurname = string.Empty;

        [Link(ControlName = "motherName", LinkActionName = "FillParents", HideForBG = true)]
        public string MotherName = string.Empty;

        [Link(ControlName = "motherSurname", LinkActionName = "FillParents", HideForBG = true)]
        public string MotherSurname = string.Empty;

        [Link(ControlName = "spouseSurname")] public string SpouseSurname = string.Empty;

        [Link(ControlName = "spouseSurnameAtBirth")] public string SpouseSurnameAtBirth = string.Empty;

        [Link(ControlName = "spouseName")] public string SpouseName = string.Empty;

        [Link(ControlName = "spouseBirthDate")] public string SpouseBirthDate = string.Empty;

        [Link(ControlName = "spouseBirthCountry", AllowFillComboBox = true)] public string SpouseBirthCountry =
            string.Empty;

        [Link(ControlName = "spouseBirthCity", AllowFillComboBox = true)] public string SpouseBirthCity = string.Empty;

        #endregion

        #region Visa request

        [String(TemplateString = "@@29_DATA_ZAEZDA")]
        [Link(ControlName = "visaStart", InitOnlyEmpty = true)]
        public string VisaStartDate = string.Empty;

        [String(TemplateString = "@@30_DATA_VIEZDA")]
        [Link(ControlName = "visaEnd", InitOnlyEmpty = true)]
        public string VisaEndDate = string.Empty;

        [Link(ControlName = "visaType", LinkActionName = "SetVisaType")] 
        public string VisaTypeValue = "Краткосрочная";
        public VisaType VisaType;

        [Link(ControlName = "numberOfEntries", LinkActionName = "SetEntries")]
        public string NumberOfEntriesValue = "Одно";
        public Entries NumberOfEntries = Entries.SingleEntry;

        [Link(ControlName = "processingSpeed", HideForBG = true)]
        public bool ProcessingSpeed = false;

        [Link(ControlName = "multiPeriod", LinkActionName = "SetMultiPeriod")] public string MultiVisaPeriodValue =
            "До трех месяцев";

        public VisaPeriod MultiVisaPeriod = VisaPeriod.To3M;

        [Link(ControlName = "purpose", LinkActionName = "SetPurpose")] public string PurposeValue =
            "Организованный туризм";

        public Purpose Purpose = Purpose.OrganizedTourism;

        [Link(ControlName = "otherPurpose", AllowFillComboBox = true, HideForBG = true)]
        public string OtherPurpose = string.Empty;

        [Link(ControlName = "dateOfApply", HideForBG = true)]
        public string DateOfApply = string.Empty;

        [Link(ControlName = "gratis", LinkActionName = "CheckGratis", HideForBG = true)]
        public bool Gratis = false;

        [Link(ControlName = "transitEntryPermit", HideForBG = true)] 
        public bool TransitDestinationPermit = true;

        [Link(ControlName = "fee", HideForBG = true)]
        public string Fee = string.Empty;

        [Link(ControlName = "feeCurrency", HideForBG = true)]
        public string FeeCurrency = string.Empty;

        [Link(ControlName = "destinationCountry")] public string DestinationCountry = string.Empty;

        [Link(ControlName = "destinationCity")] public string DestinationCity = string.Empty;

        [Link(ControlName = "firstBorder")] public string BorderOfFirstEntry = string.Empty;

        [Link(ControlName = "borderCheckpoint")] public string BorderCheckpoint = string.Empty;

        [Link(ControlName = "transitRoute", HideForBG = true)]
        public string TransitRoute = string.Empty;

        [Link(ControlName = "additionalInfo", HideForBG = true)]
        public string VisaAdditionalInfo = string.Empty;


        #endregion

        #region Hosting
        
        [Link(ControlName = "hostType", LinkActionName = "SetHostType")] 
        public string HostTypeValue = "Гостиница/временный адрес";
        public HostType HostType = HostType.Hotel;

        [Link(ControlName = "invitationNumber")]
        public string InvitationNumber = string.Empty;

        #region Host hotel

        [String(TemplateString = "@@31_HOTEL_NAME")]
        [Link(ControlName = "hostHotelName", InitOnlyEmpty = true)]
        public string HostHotelName = string.Empty;

        [String(TemplateString = "@@31_HOTEL_ADDRESS")]
        public string HostHotelFullAddress = string.Empty;

        [String(TemplateString = "@@31_HOTEL_PHONE")]
        [Link(ControlName = "hostHotelPhone", InitOnlyEmpty = true)]
        public string HostHotelPhone = string.Empty;

        [Link(ControlName = "hostHotelCountry", LinkActionName = "SetHotelFullAddress")]
        public string HostHotelCountry = string.Empty;

        [Link(ControlName = "hostHotelCity", LinkActionName = "SetHotelFullAddress")]
        public string HostHotelCity = string.Empty;

        [Link(ControlName = "hostHotelAddress", LinkActionName = "SetHotelFullAddress")]
        public string HostHotelAddress = string.Empty;

        [Link(ControlName = "hostHotelZipCode", LinkActionName = "SetHotelFullAddress")]
        public string HostHotelZipCode = string.Empty;

        [Link(ControlName = "hostHotelFax", LinkActionName = "SetHotelFullAddress", HideForBG = true)]
        public string HostHotelFax = string.Empty;

        [Link(ControlName = "hostHotelEmail", LinkActionName = "SetHotelFullAddress", HideForBG = true)]
        public string HostHotelEmail = string.Empty;

        #endregion

        #region Host person

        [String(TemplateString = "@@32_DANNIE_LICO_COMPANII_PRIGLASHENIE")]
        public string HostPersonNameAddressPhoneEmail = string.Empty;

        [Link(ControlName = "hostPersonName", InitOnlyEmpty = true, LinkActionName = "SetHostPersonInfo", HideForBG = true)]
        public string
            HostPersonName = string.Empty;

        [Link(ControlName = "hostPersonSurname", InitOnlyEmpty = true, LinkActionName = "SetHostPersonInfo", HideForBG = true)]
        public
            string HostPersonSurname = string.Empty;

        [Link(ControlName = "hostPersonCitizenship", InitOnlyEmpty = true, LinkActionName = "SetHostPersonInfo", HideForBG = true)]
        public
            string HostPersonCitizenship = string.Empty;

        [Link(ControlName = "hostPersonID", InitOnlyEmpty = true, LinkActionName = "SetHostPersonInfo", HideForBG = true)]
        public string
            HostPersonID = string.Empty;

        [Link(ControlName = "hostPersonCountry", InitOnlyEmpty = true, LinkActionName = "SetHostPersonInfo", HideForBG = true)]
        public
            string HostPersonCountry = string.Empty;

        [Link(ControlName = "hostPersonCity", InitOnlyEmpty = true, LinkActionName = "SetHostPersonInfo", HideForBG = true)]
        public string
            HostPersonCity = string.Empty;

        [Link(ControlName = "hostPersonZipCode", InitOnlyEmpty = true, LinkActionName = "SetHostPersonInfo", HideForBG = true)]
        public
            string HostPersonZipCode = string.Empty;

        [Link(ControlName = "hostPersonAddress", InitOnlyEmpty = true, LinkActionName = "SetHostPersonInfo", HideForBG = true)]
        public
            string HostPersonAddress = string.Empty;

        [Link(ControlName = "hostPersonPhone", InitOnlyEmpty = true, LinkActionName = "SetHostPersonInfo", HideForBG = true)]
        public
            string HostPersonPhone = string.Empty;

        [Link(ControlName = "hostPersonFax", InitOnlyEmpty = true, LinkActionName = "SetHostPersonInfo", HideForBG = true)]
        public string
            HostPersonFax = string.Empty;

        [Link(ControlName = "hostPersonEmail", InitOnlyEmpty = true, LinkActionName = "SetHostPersonInfo", HideForBG = true)]
        public
            string HostPersonEmail = string.Empty;

        #endregion

        #region Host company

        [String(TemplateString = "@@32_NAZVANIE_ADRES_COMPANY_PRIGLASHENIE")]
        public string HostCompanyNameAndAddress = string.Empty;

        [String(TemplateString = "@@32_TEL_PRIGL_COMPANY")]
        [Link(ControlName = "hostCompanyPhone", InitOnlyEmpty = true)]
        public string HostCompanyPhone = string.Empty;

        [Link(ControlName = "hostCompanyID", HideForBG = true)] 
        public string HostCompanyID = string.Empty;

        [Link(ControlName = "hostCompanyName", LinkActionName = "SetHostFullAddress")] 
        public string HostCompanyName = string.Empty;

        [Link(ControlName = "hostCompanyCountry", LinkActionName = "SetHostFullAddress", HideForBG = true)] 
        public string HostCompanyCountry = string.Empty;

        [Link(ControlName = "hostCompanyCity", LinkActionName = "SetHostFullAddress", HideForBG = true)] 
        public string HostCompanyCity = string.Empty;

        [Link(ControlName = "hostCompanyAddress", LinkActionName = "SetHostFullAddress", HideForBG = true)] 
        public string HostCompanyAddress = string.Empty;

        [Link(ControlName = "hostCompanyZipCode", LinkActionName = "SetHostFullAddress", HideForBG = true)] 
        public string HostCompanyZipCode = string.Empty;

        [Link(ControlName = "hostCompanyFax", LinkActionName = "SetHostFullAddress", HideForBG = true)] 
        public string HostCompanyFax = string.Empty;

        [Link(ControlName = "hostCompanyEmail", LinkActionName = "SetHostFullAddress", HideForBG = true)] 
        public string HostCompanyEmail = string.Empty;

        #endregion
        
        #region Voucher

        [Link(ControlName = "voucherNumber")]
        public string VoucherNumber = string.Empty;

        [Link(ControlName = "voucherValidFrom")]
        public object VoucherValidFrom = string.Empty;

        [Link(ControlName = "voucherValidTo")]
        public object VoucherValidTo = string.Empty;

        [Link(ControlName = "voucherIssuedBy")]
        public string VoucherIssuedBy = string.Empty;

        #endregion

        #endregion

        #region Insurance

        [Link(ControlName = "insuranceCompany", HideForBG = true)]
        public string InsuranceCompany = string.Empty;

        [Link(ControlName = "insuranceId", HideForBG = true)]
        public string InsuranceId = string.Empty;

        [Link(ControlName = "insuranceValidDate", HideForBG = true)]
        public string InsuranceValidDateValue = string.Empty;

        #endregion

        #region Transport

        [Link(ControlName = "transport", HideForBG = true)]
        public string Transport = string.Empty;

        #endregion
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

    public enum VisaType
    {
        AirportTransit = 0,
        Transit,
        ShortStay,
        LongStay
    }

    public enum Entries
    {
        SingleEntry = 0,
        DoubleEntries,
        MultipleEntries
    }

    public enum VisaPeriod
    {
        To3M = 0,
        To6M,
        To1Y,
        To2Y,
        To3Y,
        To4Y,
        To5Y
    }

    public enum Purpose
    {
        Business = 0,
        Marriage,
        Professional,
        Diplomatic,
        Other,
        Family,
        Humanitarian,
        Cultural,
        Medical,
        Residence,
        Work,
        Commercial,
        Official,
        Sport,
        Transit,
        OrganizedTourism,
        Tourism,
        Study
    }

    public enum HostType
    {
        Person = 0,
        Company,
        Hotel
    }
}
