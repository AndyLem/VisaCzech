using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisaCzech.BL.Converters;

namespace VisaCzech.BL.PersonCheckers
{
    public class PassportValidityChecker : BaseChecker
    {
        protected int _minimumMonthsToBeValid;

        public PassportValidityChecker(int minimumMonthsToBeValid = 4)
        {
            _minimumMonthsToBeValid = minimumMonthsToBeValid;
            _isCritical = false;
        }

        public override bool Check(Person person)
        {
            var now = DateTime.Now;
            var validTo = DateTimeConverter.ConvertStrToDateTime(person.DocumentValidDate);
            var limit = now.AddMonths(_minimumMonthsToBeValid);
            _warningMessage = string.Format("Срок действия паспорта истекает {0}. Осталось менее {1} месяцев!", validTo.ToShortDateString(),
                                            _minimumMonthsToBeValid);
            return validTo >= limit;
        }
    }
}
