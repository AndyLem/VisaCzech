using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisaCzech.BL.PersonCheckers
{
    public abstract class BaseChecker : IChecker
    {
        protected string _warningMessage;
        protected bool _isCritical;

        public abstract bool Check(Person person);

        public string WarningMessage
        {
            get { return _warningMessage; }
        }

        public bool IsCritical
        {
            get { return _isCritical; }
        }
    }
}
