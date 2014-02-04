using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisaCzech.BL.PersonCheckers
{
    public interface IChecker
    {
        bool Check(Person person);
        string WarningMessage { get; }
        bool IsCritical { get; }
    }
}
