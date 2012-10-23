using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisaCzech.BL.WordFiller
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class BoolAttribute : Attribute
    {
        public string TemplateString;

        public BoolAttribute()
        {
            TemplateString = string.Empty;
        }
    }
}
