using System;
using System.Drawing;
using VisaCzech.BL;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace VisaCzech.DL
{
    public sealed class PersonStorage : Storage<Person>
    {
        private static PersonStorage _instance;
        private static string _imagesDir = "Images\\";

        public static PersonStorage Instance
        {
            get { return _instance ?? (_instance = new PersonStorage()); }
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
                        Directory.CreateDirectory(DefaultPath + _imagesDir);
                        bmp.Save(DefaultPath + _imagesDir + obj.Id + ".jpg");
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return res;
        }

        protected override Person Load(string fileName)
        {
            var res = base.Load(fileName);
            Directory.CreateDirectory(DefaultPath + _imagesDir);

            if (File.Exists(DefaultPath + _imagesDir + res.Id + ".jpg"))
            {
                try
                {
                    var bmp = Image.FromFile(DefaultPath + _imagesDir + res.Id + ".jpg");
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
            Directory.CreateDirectory(DefaultPath + _imagesDir);
            if (File.Exists(DefaultPath + _imagesDir + obj.Id + ".jpg"))
            {
                try
                {
                    File.Delete((DefaultPath + _imagesDir + obj.Id + ".jpg"));
                }
                catch
                {
                    
                }
            }
            return res;
        }
    }
}
