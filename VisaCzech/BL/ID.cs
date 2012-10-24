using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisaCzech.BL
{
    public abstract class ID
    {
        protected bool Equals(ID other)
        {
            return string.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        public string Id;

        public static bool operator ==(ID a, ID b)
        {
            if ((null == (object)a) && (null == (object)b)) return true;
            if ((null == (object)a) || (null == (object)b)) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(ID a, ID b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            if (obj is ID)
                return (obj as ID).Id == this.Id;
            return false;
        }
    }
}
