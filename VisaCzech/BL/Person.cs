using VisaCzech.BL.ObjFramework.WordFiller;
using VisaCzech.BL.ObjFramework.ObjectContainerLinker;
using System;

namespace VisaCzech.BL
{
    public class Person
    {
        public string Id;

        public Person()
        {
            Id = Guid.NewGuid().ToString();
        }

        [String(TemplateString = "@@FAMILIA")]
        [Link(ControlName = "surname")]
        public string Surname;
        
        [String(TemplateString = "@@FAMILIA_ROJD")]
        [Link(ControlName = "surname2")]
        public string SurnameAtBirth;

        [String(TemplateString = "@@IMYA")]
        [Link(ControlName = "name")]
        public string Name;

        [String(TemplateString = "@@DATA_ROJD")]
        [Link(ControlName = "birthDate")]
        public string BirthDate;

        [String(TemplateString = "@@PLACE_BIRTH")]
        [Link(ControlName = "birthPlace")]
        public string BirthPlace;

        [String(TemplateString = "@@STRANA_ROJD")]
        [Link(ControlName = "birthCountry")]
        public string BirthCountry;

        [String(TemplateString = "@@GRAJDANSTVO")]
        [Link(ControlName = "Citizen")]
        public string Citizenship;

        [Link(ControlName = "sex", LinkActionName = "SexChanged", AllowFillComboBox = false)]
        public string SexValue = "Мужской";
        [Enum(TemplateString = "@@POL", EnumValues = 2)]
        public Sex Sex;

        [Link(ControlName = "family", LinkActionName = "FamilyChanged", AllowFillComboBox = false)]
        public string StatusValue;
        [Enum(TemplateString = "@@STATUS", EnumValues = 6)]
        public Status Status;
        [String(TemplateString = "@@OTHER_STATUS")]
        [Link(ControlName = "otherFamily")]
        public string OtherStatus;


        [String(TemplateString = "@@RODITEL")]
        [Link(ControlName = "parent")]
        public string Parent;

        [String(TemplateString = "@@ID_PASSPORT")]
        [Link(ControlName = "personalId")]
        public string PersonalId;

        [Link(ControlName = "docType", LinkActionName = "DocTypeChanged", AllowFillComboBox = false)]
        public string DocumentTypeValue = "Паспорт";
        [Enum(TemplateString = "@@DOCTYPE", EnumValues = 6)]
        public DocType DocumentType;
        [String(TemplateString = "@@OTHER_DOCTYPE")]
        [Link(ControlName = "otherDocType")]
        public string OtherDocumentType;

        [String(TemplateString = "@@SERIYA_NUMBER_PASSPORT")]
        [Link(ControlName = "docNumber")]
        public string DocumentNumber;

        [String(TemplateString = "@@DATA_VYDACHI")]
        [Link(ControlName = "docIssued")]
        public string DocumentIssuedDate;

        [String(TemplateString = "@@DEISTVIT_DO")]
        [Link(ControlName = "docValid")]
        public string DocumentValidDate;

        [String(TemplateString = "@@VIDAN_PASS")]
        [Link(ControlName = "docIssuedBy")]
        public string DocumentIssuedBy;

        [String(TemplateString = "@@DOM_ADRES_MILO")]
        [Link(ControlName = "homeAddress")]
        public string AddressAndEmail;

        [String(TemplateString = "TEL_DOM")]
        [Link(ControlName = "homePhone")]
        public string PhoneNumber;

        [String(TemplateString = "@@PROF_DEYATELNOST")]
        [Link(ControlName = "profession")]
        public string Profession;

        [String(TemplateString = "@@RABOTA_SHKOLA")]
        [Link(ControlName = "work")]
        public string WorkOrSchoolAddress;

        [String(TemplateString = "@@PRODOLJIT_DNEY")]
        [Link(ControlName = "duration")]
        public string Duration;

        [Link(ControlName = "visa1Enabled", LinkActionName = "Visa1EnabledChanged")]
        public bool Visa1Enabled = false;
        [Link(ControlName = "visa1From")]
        [String(TemplateString = "@@VISA1_FROM")]
        public string Visa1From;
        [Link(ControlName = "visa1To")]
        [String(TemplateString = "@@VISA1_TO")]
        public string Visa1To;

        [Link(ControlName = "visa2Enabled", LinkActionName = "Visa2EnabledChanged")]
        public bool Visa2Enabled = false;
        [Link(ControlName = "visa2From")]
        [String(TemplateString = "@@VISA2_FROM")]
        public string Visa2From;
        [Link(ControlName = "visa2To")]
        [String(TemplateString = "@@VISA2_TO")]
        public string Visa2To;

        [Link(ControlName = "visa3Enabled", LinkActionName = "Visa3EnabledChanged")]
        public bool Visa3Enabled = false;
        [Link(ControlName = "visa3From")]
        [String(TemplateString = "@@VISA3_FROM")]
        public string Visa3From;
        [Link(ControlName = "visa3To")]
        [String(TemplateString = "@@VISA3_TO")]
        public string Visa3To;

        [String(TemplateString = "@@DATA_ZAEZDA")]
        [Link(ControlName = "visaStart")]
        public string VisaStartDate;

        [String(TemplateString = "@@DATA_VIEZDA")]
        [Link(ControlName = "visaEnd")]
        public string VisaEndDate;

        [String(TemplateString = "@@32_NAZVANIE_ADRES_COMPANY_PRIGLASHENIE")]
        [Link(ControlName = "host")]
        public string HostNameAndAddress;

        [String(TemplateString = "@@TEL_PRIGL_COMPANY")]
        [Link(ControlName = "hostPhone")]
        public string HostPhoneNumber;

        [String(TemplateString = "@@DANNIE_LICO_COMPANII_PRIGLASHENIE")]
        [Link(ControlName = "hostPerson")]
        public string HostPersonNameAddressPhoneEmail;

        [String(TemplateString = "@@MESTO_SOSTAVLENIYA")]
        [Link(ControlName = "fillPlace")]
        public string PlaceOfFilling;
        [String(TemplateString = "@@DATA_SOSTAVLENIYA")]
        [Link(ControlName = "fillDate")]
        public string DateOfFilling;
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
