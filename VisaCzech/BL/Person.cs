using VisaCzech.BL.ObjFramework.ObjectContainerLinker;
using System;
using VisaCzech.BL.WordFiller;

namespace VisaCzech.BL
{
    public class Person
    {
        public string Id;

        public Person()
        {
            Id = Guid.NewGuid().ToString();
        }

        [String(TemplateString = "@@1_FAMILIA")]
        [Link(ControlName = "surname")]
        public string Surname;
        
        [String(TemplateString = "@@2_FAMILIA_ROJD")]
        [Link(ControlName = "surname2")]
        public string SurnameAtBirth;

        [String(TemplateString = "@@3_IMYA")]
        [Link(ControlName = "name")]
        public string Name;

        [String(TemplateString = "@@4_DATA_ROJD")]
        [Link(ControlName = "birthDate")]
        public string BirthDate;

        [String(TemplateString = "@@5_PLACE_BIRTH")]
        [Link(ControlName = "birthPlace")]
        public string BirthPlace;

        [String(TemplateString = "@@6_STRANA_ROJD")]
        [Link(ControlName = "birthCountry")]
        public string BirthCountry;

        [String(TemplateString = "@@7_GRAJDANSTVO")]
        [Link(ControlName = "citizen")]
        public string Citizenship;

        [String(TemplateString = "@@7__GRAJDANSTVO_ROJD")]
        [Link(ControlName = "birthCitizen")]
        public string BirthCitizenship;

        [Link(ControlName = "sex", LinkActionName = "SexChanged", AllowFillComboBox = false)]
        public string SexValue = "Мужской";
        [Enum(TemplateString = "@8_", EnumValues = 2)]
        public Sex Sex;

        [Link(ControlName = "family", LinkActionName = "FamilyChanged", AllowFillComboBox = false)]
        public string StatusValue;
        [Enum(TemplateString = "@9_", EnumValues = 6)]
        public Status Status;
        [String(TemplateString = "@@9_OTHER_STATUS")]
        [Link(ControlName = "otherFamily")]
        public string OtherStatus;


        [String(TemplateString = "@@10_RODITEL")]
        [Link(ControlName = "parent")]
        public string Parent;

        [String(TemplateString = "@@11_ID_PASSPORT")]
        [Link(ControlName = "personalId")]
        public string PersonalId;

        [Link(ControlName = "docType", LinkActionName = "DocTypeChanged", AllowFillComboBox = false)]
        public string DocumentTypeValue = "Паспорт";
        [Enum(TemplateString = "@12_", EnumValues = 6)]
        public DocType DocumentType;
        [String(TemplateString = "@@12_OTHER_DOCTYPE")]
        [Link(ControlName = "otherDocType")]
        public string OtherDocumentType;

        [String(TemplateString = "@@13_SERIYA_NUMBER_PASSPORT")]
        [Link(ControlName = "docNumber")]
        public string DocumentNumber;

        [String(TemplateString = "@@14_DATA_VIDACHI")]
        [Link(ControlName = "docIssued")]
        public string DocumentIssuedDate;

        [String(TemplateString = "@@15_DEYSTVIT_DO")]
        [Link(ControlName = "docValid")]
        public string DocumentValidDate;

        [String(TemplateString = "@@16_VIDAN_PASS")]
        [Link(ControlName = "docIssuedBy")]
        public string DocumentIssuedBy;

        [String(TemplateString = "@@17_DOM_ADRES_MILO")]
        [Link(ControlName = "homeAddress")]
        public string AddressAndEmail;

        [String(TemplateString = "@@17_TEL_DOM")]
        [Link(ControlName = "homePhone")]
        public string PhoneNumber;

        [String(TemplateString = "@@19_PROF_DEYATELNOST")]
        [Link(ControlName = "profession")]
        public string Profession;

        [String(TemplateString = "@@20_RABOTA_SHKOLA")]
        [Link(ControlName = "work")]
        public string WorkOrSchoolAddress;

        [String(TemplateString = "@@25_PRODOLJIT_DNEY")]
        [Link(ControlName = "duration")]
        public string Duration;

        [Link(ControlName = "visa1Enabled", LinkActionName = "Visa1EnabledChanged")]
        public bool Visa1Enabled = false;
        [Link(ControlName = "visa1From")]
        [String(TemplateString = "@@26_VISA1_FROM")]
        public string Visa1From;
        [Link(ControlName = "visa1To")]
        [String(TemplateString = "@@26_VISA1_TO")]
        public string Visa1To;

        [Link(ControlName = "visa2Enabled", LinkActionName = "Visa2EnabledChanged")]
        public bool Visa2Enabled = false;
        [Link(ControlName = "visa2From")]
        [String(TemplateString = "@@26_VISA2_FROM")]
        public string Visa2From;
        [Link(ControlName = "visa2To")]
        [String(TemplateString = "@@26_VISA2_TO")]
        public string Visa2To;

        [Link(ControlName = "visa3Enabled", LinkActionName = "Visa3EnabledChanged")]
        public bool Visa3Enabled = false;
        [Link(ControlName = "visa3From")]
        [String(TemplateString = "@@26_VISA3_FROM")]
        public string Visa3From;
        [Link(ControlName = "visa3To")]
        [String(TemplateString = "@@26_VISA3_TO")]
        public string Visa3To;

        [String(TemplateString = "@@29_DATA_ZAEZDA")]
        [Link(ControlName = "visaStart")]
        public string VisaStartDate;

        [String(TemplateString = "@@30_DATA_VIEZDA")]
        [Link(ControlName = "visaEnd")]
        public string VisaEndDate;
        
        [String(TemplateString = "@@32_NAZVANIE_ADRES_COMPANY_PRIGLASHENIE")]
        [Link(ControlName = "host")]
        public string HostNameAndAddress;

        [String(TemplateString = "@@32_TEL_PRIGL_COMPANY")]
        [Link(ControlName = "hostPhone")]
        public string HostPhoneNumber;

        [String(TemplateString = "@@32_DANNIE_LICO_COMPANII_PRIGLASHENIE")]
        [Link(ControlName = "hostPerson")]
        public string HostPersonNameAddressPhoneEmail;

        [String(TemplateString = "@@36_MESTO_SOSTAVLENIYA")]
        [Link(ControlName = "fillPlace")]
        public string PlaceOfFilling;
        [String(TemplateString = "@@36_DATA_SOSTAVLENIYA")]
        [Link(ControlName = "fillDate")]
        public string DateOfFilling;

        public override string ToString()
        {
            return Name + " " + Surname;
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
