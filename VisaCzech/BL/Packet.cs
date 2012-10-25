using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisaCzech.BL.ObjFramework.ObjectContainerLinker;

namespace VisaCzech.BL
{
    public class Packet : ID
    {
        [Link(ControlName = "packetName")]
        public string Name = string.Empty;

        public readonly List<string> Persons = new List<string>();

        public Person TemplatePerson = new Person();

        public Packet()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Packet(string name) : this()
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool AddPerson(Person p)
        {
            if (Persons.IndexOf(p.Id) == -1)
            {
                Persons.Add(p.Id);
                return true;
            }
            return false;
        }

        public bool RemovePerson(Person p)
        {
            if (Persons.IndexOf(p.Id) != -1)
            {
                Persons.Remove(p.Id);
                return true;
            }
            return false;
        }

        internal ICollection<Person> EnumPersons(IEnumerable fullPersonsList)
        {
            var list = new List<Person>();
            var personsList = fullPersonsList.Cast<Person>().ToList();
            foreach (var personId in Persons)
            {
                var p = personsList.SingleOrDefault(per => per.Id == personId);
                if (p != null)
                    list.Add(p);
            }
            return list;
            return Persons.Select(p => fullPersonsList.Cast<Person>().Single(per => per.Id == p)).Where(person => person != null).ToList();
        }

        internal int IndexOfPerson(Person p)
        {
            return Persons.IndexOf(p.Id);
        }

        internal void Merge(Packet otherPacket)
        {
            var tempId = Id;
            var fieldInfos = GetType().GetFields();
            foreach (var info in fieldInfos)
            {
                var val = info.GetValue(otherPacket);
                if (val == null) continue;

                if (info.FieldType == typeof(Person))
                {
                    var otherPerson = (Person) val;
                    var newPerson = new Person();
                    newPerson.Merge(otherPerson, true);
                    info.SetValue(this, newPerson);
                    continue;
                }

                if (string.IsNullOrEmpty(val.ToString())) continue;
                info.SetValue(this, val);
            }
            Id = tempId;
        }
    }
}
