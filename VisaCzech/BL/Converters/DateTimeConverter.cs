using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisaCzech.BL.Converters
{
    public static class DateTimeConverter
    {
        public static DateTime ConvertStrToDateTime(object val)
        {
            var valStr = val.ToString();
            string dateStr;
            if (string.IsNullOrEmpty(valStr)) return DateTime.Now;
            if (valStr.Length == 8)
                dateStr = valStr.Substring(0, 2) + "." + valStr.Substring(2, 2) + "." + valStr.Substring(4);
            else if (valStr.IndexOfAny(new[] { '-', '.' }) != -1)
                dateStr = val.ToString().Replace('-', '.');
            else throw new Exception("Непонятный формат даты в значении " + valStr);
            DateTime dt;
            try
            {
                dt = Convert.ToDateTime(dateStr);
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
            return dt;
        }

        public static string ConvertDateToText(DateTime dateTime, string format = null)
        {
            return dateTime.ToString(string.IsNullOrEmpty(format) ? "dd.MM.yyyy" : format);
        }
    }
}
