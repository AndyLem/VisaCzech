using System;

namespace VisaCzech.BL.WordFiller
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class EnumAttribute : Attribute
    {
        public string TemplateString;
        public int EnumValues;

        public EnumAttribute()
        {
            EnumValues = 0;
            TemplateString = string.Empty;
        }
    }
}
