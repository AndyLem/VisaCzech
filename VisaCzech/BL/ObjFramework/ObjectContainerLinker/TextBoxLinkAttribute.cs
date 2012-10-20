using System;

namespace VisaCzech.BL.ObjFramework.ObjectContainerLinker
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class TextBoxLinkAttribute : Attribute
    {
        public string TextBoxName;

        public TextBoxLinkAttribute()
        {
            TextBoxName = string.Empty;
        }
    }
}
