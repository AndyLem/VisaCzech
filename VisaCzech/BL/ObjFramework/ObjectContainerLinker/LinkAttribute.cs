using System;
using System.Windows.Forms;

namespace VisaCzech.BL.ObjFramework.ObjectContainerLinker
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class LinkAttribute : Attribute
    {
        public string ControlName;

        public string LinkActionName;

        public bool AllowFillComboBox;

        public LinkAttribute()
        {
            ControlName = string.Empty;
            LinkActionName = string.Empty;
            AllowFillComboBox = true;
        }

    }
}
