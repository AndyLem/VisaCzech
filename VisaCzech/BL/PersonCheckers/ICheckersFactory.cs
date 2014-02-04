using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisaCzech.BL.PersonCheckers
{
    public interface ICheckersFactory
    {
        IEnumerable<IChecker> EnumCheckers();
    }
}
