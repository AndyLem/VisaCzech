using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisaCzech.BL.CognitiveScanner
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class FieldAttribute : Attribute
    {
        public string FieldName = string.Empty;
    }
}
