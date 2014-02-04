using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisaCzech.BL.PersonCheckers
{
    public class CheckersFactory : ICheckersFactory
    {
        private CheckersFactory()
        {
            
        }

        private static CheckersFactory _instance;

        public static CheckersFactory Instance
        {
            get { return _instance ?? (_instance = new CheckersFactory()); }
        }

        protected PassportValidityChecker _passportValidityChecker = new PassportValidityChecker();

        public PassportValidityChecker PassportValidityChecker { get { return _passportValidityChecker; } }

        public IEnumerable<IChecker> EnumCheckers()
        {
            var list = new List<IChecker> {_passportValidityChecker};
            return list;
        }
    }
}
