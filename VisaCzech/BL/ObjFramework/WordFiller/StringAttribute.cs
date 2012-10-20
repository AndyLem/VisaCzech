using System;

namespace VisaCzech.BL.ObjFramework.WordFiller
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class StringAttribute : Attribute
    {
        public string TemplateString;

        public StringAttribute()
        {
            TemplateString = string.Empty;
        }
    }
}
