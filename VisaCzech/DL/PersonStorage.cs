using System;
using System.Drawing;
using System.Linq;
using VisaCzech.BL;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace VisaCzech.DL
{
    public sealed class PersonStorage : Storage<Person>
    {
        private static PersonStorage _instance;
        private const string ImagesDir = "Images\\";

        private readonly List<Person> _fullList = new List<Person>();

        public static PersonStorage Instance
        {
            get { return _instance ?? (_instance = new PersonStorage()); }
        }

        public List<Person> FullList
        {
            get { return _fullList; }
        }

        private PersonStorage()
        {
            _dirName = "Persons\\";
        }

        protected override bool Save(Person obj, string fileName)
        {
            var res = base.Save(obj, fileName);
            if (res)
            {
                if (obj.Image != null)
                {
                    var bmp = new Bitmap(obj.Image);
                    try
                    {
                        Directory.CreateDirectory(DefaultPath + ImagesDir);
                        bmp.Save(DefaultPath + ImagesDir + obj.Id + ".jpg");
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (_fullList.IndexOf(obj) == -1) _fullList.Add(obj);
            return res;
        }

        protected override Person Load(string fileName)
        {
            var res = base.Load(fileName);
            Directory.CreateDirectory(DefaultPath + ImagesDir);

            if (File.Exists(DefaultPath + ImagesDir + res.Id + ".jpg"))
            {
                try
                {
                    var bmp = Image.FromFile(DefaultPath + ImagesDir + res.Id + ".jpg");
                    res.Image = bmp;

                }
                catch (Exception)
                {
                    
                }
            }
            return res;
        }

        public override bool Delete(Person obj)
        {
            var res = base.Delete(obj);
            Directory.CreateDirectory(DefaultPath + ImagesDir);
            if (File.Exists(DefaultPath + ImagesDir + obj.Id + ".jpg"))
            {
                try
                {
                    File.Delete((DefaultPath + ImagesDir + obj.Id + ".jpg"));
                }
                catch
                {
                    
                }
            }
            if (_fullList.IndexOf(obj) != -1) _fullList.Remove(obj);
            return res;
        }

        public override IEnumerable<Person> LoadAll()
        {
            var res = base.LoadAll();
            _fullList.Clear();
            var loadAll = res as List<Person> ?? res.ToList();
            _fullList.AddRange(loadAll);
            return loadAll;
        }
    }
}
