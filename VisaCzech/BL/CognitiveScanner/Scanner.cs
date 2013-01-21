using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScAPI;
using Exception = System.Exception;

namespace VisaCzech.BL.CognitiveScanner
{
    public sealed class Scanner
    {
        public static Scanner Instance = new Scanner();

        private IScanDevice _scanDevce;
        private readonly IInstance _scanInstance;

        public IDocument LastScannedDocument { get; private set; }

        public IForm LastScannedForm { get; private set; }

        public bool Valid
        {
            get { return _scanInstance != null; }
        }

        public bool HasScanner
        {
            get { return _scanDevce != null; }
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
                if (_scanDevce == null) return false;
                if (_scanInstance == null) return false;
                _scanDevce.ConfigName = "ScanInoPassportBy";

                var package = _scanInstance.CreatePackage();
                if (package == null) return false;

                var image = package.Scan(_scanDevce, 0);
                if (image == null) return false;

                package.Recog();

                if (package.Documents == null) return false;
                if (package.Forms == null) return false;

                if (package.Forms.Length > 0)
                    LastScannedForm = package.Forms[0];

                if (package.Documents.Length > 0)
                    LastScannedDocument = package.Documents[0];
                return (LastScannedDocument != null) && (LastScannedForm != null);
            }
            catch 
            {
                return false;
            }

        }

        public void InitTwain()
        {
            string sTwainDevice = null;

            //if (_scanInstance.AskUserForTwainDevice(null))
            //{
                sTwainDevice = _scanInstance.GetDefaultTwainDeviceName();

            //    if (sTwainDevice == null)
            //        sTwainDevice = _scanInstance.GetDefaultTwainDeviceName();

            //}
            _scanDevce = sTwainDevice != null ? _scanInstance.OpenTwainDevice(sTwainDevice) : null;
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

            return person;
        }

        private string GetFieldValue(string fieldName)
        {
            if (LastScannedDocument == null) return string.Empty;
            var fieldValue = LastScannedDocument.GetFieldValue(fieldName);
            return fieldValue != null ? fieldValue.Text : string.Empty;
        }
    }
}
