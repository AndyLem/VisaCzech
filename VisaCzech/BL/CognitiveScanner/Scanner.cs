using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using ScAPI;
using VisaCzech.BL.Background;
using Exception = System.Exception;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace VisaCzech.BL.CognitiveScanner
{
    public sealed class Scanner
    {
        public static Scanner Instance = new Scanner();

        private IScanDevice _scanDevice;
        private readonly IInstance _scanInstance;

        public IDocument LastScannedDocument { get; private set; }
        private IPackage _package;
        public IForm LastScannedForm { get; private set; }

        public bool Success { get; private set; }
        
        private IBackgroundStrategy _backgroundStrategy;

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

        public void Init(BackgroundOptions ops)
        {
            _backgroundStrategy = StrategyFactory.CreateStrategy(false);
            _backgroundStrategy.Init(ops);
            _backgroundStrategy.Worker.DoWork += (o, eventArgs) => DoRecognize();
            DoScan();
            _backgroundStrategy.Run();
        }

        private bool DoRecognize()
        {
            try
            {
                var recognized = false;
                var rotations = 0;
                var percent = 0;
                do
                {
                    _backgroundStrategy.Worker.ReportProgress(percent, "Попытка распознавания");
                    _package.Recog();
                    if ((_package.Documents != null) &&
                        (_package.Forms != null) &&
                        (_package.Documents.Any()) &&
                        (_package.Forms.Any()))
                    {
                        _backgroundStrategy.Worker.ReportProgress(100, "Изображение распознано");
                        recognized = true;
                    }
                    else
                    {
                        _backgroundStrategy.Worker.ReportProgress(percent, "Изображение не распознано. Поворачиваем на 90 градусов");
                        if (!RotateImage(ref _package))
                        {
                            _backgroundStrategy.Worker.ReportProgress(100, "Не удалось повернуть изображение");
                            break;
                        }
                        rotations++;
                        percent += 20;
                    }

                } while (!recognized && rotations < 3);

                if (!recognized)
                {
                    _backgroundStrategy.Worker.ReportProgress(100, "Не удалось распознать изображение");
                    return false;
                }

                if (_package.Forms != null && _package.Forms.Length > 0)
                    LastScannedForm = _package.Forms[0];

                if (_package.Documents.Length > 0)
                    LastScannedDocument = _package.Documents[0];
                _backgroundStrategy.Worker.ReportProgress(100, "Операция завершена");
                Success = (LastScannedDocument != null) && (LastScannedForm != null);
                return Success;
            }
            catch (Exception ex)
            {
                _backgroundStrategy.Worker.ReportProgress(100, "Возникла ошибка "+ex.Message);
                return false;
            }

        }

        private void DoScan()
        {
            Success = false;
            LastScannedDocument = null;
            LastScannedForm = null;
            if (_scanDevice == null) return;
            if (_scanInstance == null) return;
            _scanDevice.ConfigName = "ScanInoPassportBy";
            _package = _scanInstance.CreatePackage();
            if (_package == null) return;
            _package.Scan(_scanDevice, 0);
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
