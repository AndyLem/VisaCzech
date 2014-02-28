using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisaCzech.BL;
using System.IO;

namespace VisaCzech.DL
{
    public class PersonsListExporter
    {
        public static void Export(IEnumerable<Person> persons, string path)
        {
            var i = 1;
            using (var fs = File.CreateText(Path.Combine(path, "list.txt")))
            {
                foreach (var p in persons)
                    fs.WriteLine("{0}\t{1} {2}\t{3}\t{4}\t{5} {6}", i++, p.Surname, p.Name, p.BirthDate, p.DocumentNumber, p.AddressCity, p.AddressStreet);
                fs.Close();
            }
        }
    }
}
