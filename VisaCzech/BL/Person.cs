using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisaCzech.BL.ObjFramework.WordFiller;
using VisaCzech.BL.ObjFramework.ObjectContainerLinker;

namespace VisaCzech.BL
{
    public class Person
    {
        [String(TemplateString = "@@FAMILIA")]
        [TextBoxLink(TextBoxName = "surnameBox")]
        public string Surname;
        
        [String(TemplateString = "@@FAMILIA_ROJD")]
        [TextBoxLink(TextBoxName = "surname2Box")]
        public string SurnameAtBirth;

        [String(TemplateString = "@@IMYA")]
        [TextBoxLink(TextBoxName = "nameBox")]
        public string Name;

        [String(TemplateString = "@@DATA_ROJD")]
        public string BirthDate;

        [String(TemplateString = "@@PLACE_BIRTH")]
        public string BirthPlace;

        [String(TemplateString = "@@STRANA_ROJD")]
        public string BirthCountry;

        [String(TemplateString = "@@GRAJDANSTVO")]
        public string Citizenship;


        public Sex Sex;
        
        
        public Status Status;

        
        public string OtherStatus;


        [String(TemplateString = "@@RODITEL")]
        public string Parent;

        [String(TemplateString = "@@ID_PASSPORT")]
        public string PersonalId;


        public DocType DocumentType;
        
        
        public string OtherDocumentType;

        [String(TemplateString = "@@SERIYA_NUMBER_PASSPORT")]
        public string DocumentNumber;

        [String(TemplateString = "@@DATA_VYDACHI")]
        public string DocumentIssuedDate;

        [String(TemplateString = "@@DEISTVIT_DO")]
        public string DocumentValidDate;

        [String(TemplateString = "@@VIDAN_PASS")]
        public string DocumentIssuedBy;


        [String(TemplateString = "@@DOM_ADRES_MILO")]
        public string AddressAndEmail;

        [String(TemplateString = "TEL_DOM")]
        public string PhoneNumber;

        [String(TemplateString = "@@PROF_DEYATELNOST")]
        public string Profession;

        [String(TemplateString = "@@RABOTA_SHKOLA")]
        public string WorkOrSchoolAddress;

        [String(TemplateString = "@@PRODOLJIT_DNEY")]
        public string Duration;

        public List<OtherVisaInfo> OtherVisas;

        [String(TemplateString = "@@DATA_ZAEZDA")]
        public string VisaStartDate;

        [String(TemplateString = "@@DATA_VIEZDA")]
        public string VisaEndDate;

        [String(TemplateString = "@@32_NAZVANIE_ADRES_COMPANY_PRIGLASHENIE")]
        public string HostNameAndAddress;

        [String(TemplateString = "@@TEL_PRIGL_COMPANY")]
        public string HostPhoneNumber;

        [String(TemplateString = "@@DANNIE_LICO_COMPANII_PRIGLASHENIE")]
        public string HostPersonNameAddressPhoneEmail;

        [String(TemplateString = "@@MESTO_SOSTAVLENIYA")]
        public string PlaceOfFilling;
        [String(TemplateString = "@@DATA_SOSTAVLENIYA")]
        public string DateOfFilling;
    }

    public class OtherVisaInfo
    {
        [String(TemplateString = "@@VISA_OT")]
        public string StartDate;

        [String(TemplateString = "@@VISA_DO")]
        public string EndDate;
    }

    public enum Status
    {
        Single,
        Married, 
        Divorced,
        Widow,
        LivesAlone,
        Other
    }

    public enum Sex
    {
        Male, 
        Fremale
    }

    public enum DocType
    {
        Passport,
        DiplomaticPassport,
        WorkPassport,
        OfficialPassport,
        SpecialPassport,
        Other
    }
}
