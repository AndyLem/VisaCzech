﻿using System;

namespace VisaCzech.BL.WordFiller
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class StringAttribute : Attribute
    {
        public string TemplateString;

        public string ValidationFuncName;

        public string InvalidValue;


        public StringAttribute()
        {
            TemplateString = string.Empty;
            ValidationFuncName = string.Empty;
            InvalidValue = string.Empty;
        }
    }
}
