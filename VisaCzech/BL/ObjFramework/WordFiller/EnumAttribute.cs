using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisaCzech.BL.ObjFramework.WordFiller
{
    public class EnumAttribute : StringAttribute
    {
        public int EnumValues;

        public EnumAttribute()
        {
            EnumValues = 0;
        }
    }
}
