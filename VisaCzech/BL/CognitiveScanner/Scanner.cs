using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ScAPI;
using Exception = System.Exception;
using System.Drawing;
using System.Drawing.Imaging;

namespace VisaCzech.BL.CognitiveScanner
{
    public sealed class Scanner
    {
        public static Scanner Instance = new Scanner();

        private IScanDevice _scanDevice;
        private readonly IInstance _scanInstance;

        public IDocument LastScannedDocument { get; private set; }

        public IForm LastScannedForm { get; private set; }

        public bool Valid
        {
            get { return _scanInstance != null; }
        }

        public bool HasScanner
        {
            get { return _scanDevice != null; }
        }

        private Scanner()
        {
            _scanInstance = ScProxy.CreateInstance();
            if (Valid) _scanInstance.ContextFile = "scapi.ini";
        }

        public bool DoScan()
        {
            try
            {
                if (_scanDevice == null) return false;
                if (_scanInstance == null) return false;
                _scanDevice.ConfigName = "ScanInoPassportBy";

                var package = _scanInstance.CreatePackage();
                if (package == null) return false;

                var image = package.Scan(_scanDevice, 0);
                if (image == null) return false;

                var recognized = false;
                var rotations = 0;
                do
                {
                    package.Recog();
                    if ((package.Documents != null) && 
                        (package.Forms != null) && 
                        (package.Documents.Any()) && 
                        (package.Forms.Any()))
                    {
                        recognized = true;
                    }
                    else
                    {
                        if (!RotateImage(ref package)) break;
                        rotations++;
                    }

                } while (!recognized && rotations < 3);

                if (!recognized) return false;

                if (package.Forms != null && package.Forms.Length > 0)
                    LastScannedForm = package.Forms[0];

                if (package.Documents.Length > 0)
                    LastScannedDocument = package.Documents[0];
                return (LastScannedDocument != null) && (LastScannedForm != null);
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private bool RotateImage(ref IPackage package)
        {
            var image = package.Images[0];
            var fileName = Path.GetTempFileName();
            image.SaveAs(fileName+".jpg", ImageType.Jpeg, 100);
            image.Dispose();
            
            var pic = Image.FromFile(fileName + ".jpg");
            pic.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pic.Save(fileName + ".jpeg", ImageFormat.Jpeg);
            pic.Dispose();
            
            package.Dispose();
            package = null;

            var imageDevice = _scanInstance.OpenFileReader(fileName + ".jpeg");
            if (imageDevice == null) return false;
            imageDevice.ConfigName = "ScanInoPassportBy";
            package = _scanInstance.CreatePackage();
            if (package == null) return false;
            image = package.Scan(imageDevice, 0);
            return image != null;
        }

        public void InitTwain()
        {
            string sTwainDevice = null;

            if (_scanInstance.AskUserForTwainDevice(null))
            {
                sTwainDevice = _scanInstance.GetDefaultTwainDeviceName();

            //    if (sTwainDevice == null)
            //        sTwainDevice = _scanInstance.GetDefaultTwainDeviceName();

            }
            _scanDevice = sTwainDevice != null ? _scanInstance.OpenTwainDevice(sTwainDevice) : null;
        }

        public Person GetPerson()
        {
            var person = new Person();

            var fieldInfos = person.GetType().GetFields();

            foreach (var info in fieldInfos)
            {
                var custAttibs = info.GetCustomAttributes(true);
                foreach (var oAttr in custAttibs.OfType<FieldAttribute>())
                {
                    info.SetValue(person, GetFieldValue(oAttr.FieldName));
                }
            }

            var mrz = GetFieldValue("MRZ");
            return mrz.Length > 0 ? AnalyzeMRZ(person, mrz) : person;
        }

        private string GetFieldValue(string fieldName)
        {
            if (LastScannedDocument == null) return string.Empty;
            var fieldValue = LastScannedDocument.GetFieldValue(fieldName);
            return fieldValue != null ? fieldValue.Text : string.Empty;
        }

        private static Person AnalyzeMRZ(Person p, string mrz)
        {
            mrz = mrz.Replace('"', '<').Replace(" ", "");
            p.PersonalId = GetMRZField(mrz, 29, 42, false);
            var s = GetMRZField(mrz, 21, 21, false);
            switch (s)
            {
                case "M":
                    p.Sex = Sex.Male;
                    break;
                case "F":
                    p.Sex = Sex.Female;
                    break;
            }
            return p;
        }

        private static string GetMRZField(string mrz, int startIndex, int endIndex, bool firstRow = true)
        {
            if (!firstRow)
            {
                startIndex += 44;
                endIndex += 44;
            }
            return mrz.Substring(startIndex - 1, endIndex - startIndex + 1).Replace('<', ' ').Trim();
        }
    }
}
