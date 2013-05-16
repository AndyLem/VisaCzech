using System;
using System.Windows.Forms;
using VisaCzech.BL;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace VisaCzech.DL
{
    public class Storage<T> where T : ID
    {
        private string _defaultPath;
        protected string _dirName = "Storage\\";

        public string DefaultPath
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultPath))
                {
                    _defaultPath = AppDomain.CurrentDomain.BaseDirectory + _dirName;
                }
                return _defaultPath;
            }
        }

        protected virtual T Load(string fileName)
        {
            try
            {
                Directory.CreateDirectory(DefaultPath);
                var fName = fileName.IndexOf(':') != -1 ? fileName : DefaultPath + fileName;
                var fs = File.Open(fName, FileMode.Open);
                var ser = new XmlSerializer(typeof(T));
                var obj = (T)ser.Deserialize(fs);
                fs.Close();
                return obj;
            }
            catch
            {
                return null;
            }
        }

        public T Load(T obj)
        {
            return Load(obj.Id + ".xml");
        }

        protected virtual bool Save(T obj, string fileName)
        {
            try
            {
                Directory.CreateDirectory(DefaultPath);
                var fName = fileName.IndexOf(':') != -1 ? fileName : DefaultPath + fileName;
                var fs = File.Create(fName);
                var ser = new XmlSerializer(typeof(T));
                ser.Serialize(fs, obj);
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Save(T obj)
        {
            var fileName = string.Format("{0}.xml", obj.Id);
            return Save(obj, fileName);
        }

        public virtual IEnumerable<T> LoadAll()
        {
            IEnumerable<string> files;
            var objs = new List<T>();
            try
            {
                files = Directory.EnumerateFiles(DefaultPath, "*.xml", SearchOption.TopDirectoryOnly);
            }
            catch
            {
                return objs;
            }
            foreach (var file in files)
            {
                T obj;
                try
                {
                    obj = Load(file);
                }
                catch (Exception)
                {
                    continue;
                }
                if (obj != null) objs.Add(obj);
            }
            return objs;
        }

        private bool Delete(string fileName)
        {
            try
            {
                Directory.CreateDirectory(DefaultPath);
                var fName = fileName.IndexOf(':') != -1 ? fileName : DefaultPath + fileName;
                File.Delete(fName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Delete(T obj)
        {
            var fileName = string.Format("{0}.xml", obj.Id);
            return Delete(fileName);
        }
    }
}
