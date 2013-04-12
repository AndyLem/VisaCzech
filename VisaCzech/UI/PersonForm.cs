using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisaCzech.BL;
using VisaCzech.BL.Background;
using VisaCzech.BL.ObjFramework.ObjectContainerLinker;
using VisaCzech.DL;
using VisaCzech.BL.ScannerXmlParser;
using VisaCzech.BL.TranslitConverter;
using VisaCzech.Properties;
using System.IO;
using VisaCzech.BL.CognitiveScanner;
using VisaCzech.UI.BG;

namespace VisaCzech.UI
{
    public partial class PersonForm : Form
    {
        private Linker _linker;
        private VisaCzech.BL.Person _person;
        private Packet _templatePacket;

        private enum Mode
        {
            Edit,
            Create
        }

        public EventHandler PersonCreated;

        public bool AutoSavePerson = true;

        private Mode FormMode;
        private DialogResult saveDialogResult;

        public PersonForm()
        {
            InitializeComponent();
        }

        private void InitActionFactory()
        {
            if (_linker == null) return;
            _linker.ActionFactory.RegisterAction("SexChanged", (control, linkedObject) =>
                                                                   {
                                                                       var person = (Person)linkedObject;
                                                                       var cbb = (ComboBox)control;
                                                                       person.Sex = (Sex)cbb.SelectedIndex;
                                                                   });
            _linker.ActionFactory.RegisterAction("FamilyChanged", (control, linkedObject) =>
                                                                      {
                                                                          var person = (Person)linkedObject;
                                                                          var cbb = (ComboBox)control;
                                                                          person.Status = (Status)cbb.SelectedIndex;
                                                                      });
            _linker.ActionFactory.RegisterAction("DocTypeChanged", (control, linkedObject) =>
                                                                       {
                                                                           var person = (Person)linkedObject;
                                                                           var cbb = (ComboBox)control;
                                                                           person.DocumentType =
                                                                               (DocType)cbb.SelectedIndex;
                                                                       });
            _linker.ActionFactory.RegisterAction("Visa1EnabledChanged", (control, linkedObject) =>
                                                                            {
                                                                                var person = (Person)linkedObject;
                                                                                this.visa1To.Enabled =
                                                                                    this.visa1From.Enabled =
                                                                                    visa2Enabled.Enabled =
                                                                                    person.Visa1Enabled;
                                                                                if (!visa2Enabled.Enabled)
                                                                                    visa2Enabled.Checked = false;
                                                                                
                                                                            });
            _linker.ActionFactory.RegisterAction("Visa2EnabledChanged", (control, linkedObject) =>
                                                                            {
                                                                                var person = (Person)linkedObject;
                                                                                this.visa2To.Enabled =
                                                                                    this.visa2From.Enabled =
                                                                                    visa3Enabled.Enabled =
                                                                                    person.Visa2Enabled;
                                                                                if (!visa3Enabled.Enabled)
                                                                                    visa3Enabled.Checked = false;
                                                                            });
            _linker.ActionFactory.RegisterAction("Visa3EnabledChanged", (control, linkedObject) =>
                {
                    var person = (Person)linkedObject;
                    this.visa3To.Enabled =
                        this.visa3From.Enabled =
                        person.Visa3Enabled;
                });
            _linker.ActionFactory.RegisterAction("SurnameChanged", (control, linkedObject) =>
                {
                    var person = (Person)linkedObject;
                    if (person.Surname != null)
                        if (person.SurnameAtBirth == null || (person.Surname.StartsWith(person.SurnameAtBirth)))
                            surname2.Text = person.Surname;
                });

            _linker.ActionFactory.RegisterAction("CitizenChanged", (control, linkedObject) =>
                {
                    var person = (Person)linkedObject;
                    if (person.Citizenship != null)
                        if (person.BirthCitizenship == null || (person.Citizenship.StartsWith(person.BirthCitizenship)))
                            birthCitizen.Text = person.Citizenship;
                });
            _linker.ActionFactory.RegisterAction("FillHomeAddress", (control, linkedObject) =>
                {
                    var person = (Person)linkedObject;
                    person.AddressAndEmail = string.Format("{0}, {1}, {2}, {3}. {4}", person.AddressZipCode,
                                                           person.AddressStreet, person.AddressCity,
                                                           person.AddressCountry, person.Email);
                });
            _linker.ActionFactory.RegisterAction("FillWorkAddress", (control, linkedObject) =>
            {
                var person = (Person)linkedObject;
                person.WorkOrSchoolAddress = string.Format("{0}, {1}, {2}, {3}. {4}", person.WorkZip,
                                                       person.WorkAddress, person.WorkCity,
                                                       person.WorkCountry, person.WorkEmail);
            });
            _linker.ActionFactory.RegisterAction("FillParents", (control, linkedObject) =>
            {
                var person = (Person)linkedObject;
                person.Parent = string.Format("{0} {1}, {2} {3}", person.FatherSurname, person.FatherName, person.MotherSurname, person.MotherName).Trim(' ', ','); 
            });
            _linker.ActionFactory.RegisterAction("SetVisaType", (control, linkedObject) =>
            {
                var person = (Person)linkedObject;
                person.VisaType = (VisaType) (((ComboBox)control).SelectedIndex);
            });
            _linker.ActionFactory.RegisterAction("SetEntries", (control, linkedObject) =>
            {
                var person = (Person)linkedObject;
                person.NumberOfEntries = (Entries)(((ComboBox)control).SelectedIndex);
            });
            _linker.ActionFactory.RegisterAction("SetMultiPeriod", (control, linkedObject) =>
            {
                var person = (Person)linkedObject;
                person.MultiVisaPeriod = (VisaPeriod)(((ComboBox)control).SelectedIndex);
            });
            _linker.ActionFactory.RegisterAction("SetPurpose", (control, linkedObject) =>
            {
                var person = (Person)linkedObject;
                person.Purpose = (Purpose)(((ComboBox)control).SelectedIndex);
            });
            _linker.ActionFactory.RegisterAction("CheckGratis", (control, linkedObject) =>
            {
                fee.Enabled =
                    feeCurrency.Enabled = !gratis.Checked;
            });
            _linker.ActionFactory.RegisterAction("SetHostType", (control, linkedObject) =>
            {
                var person = (Person)linkedObject;
                person.HostType = (HostType)(((ComboBox)control).SelectedIndex);
                // Enable/disable controls here
            });
            _linker.ActionFactory.RegisterAction("SetHostPersonInfo", (control, linkedObject) =>
            {
                var person = (Person)linkedObject;
                person.HostPersonNameAddressPhoneEmail =
                    string.Format("{0} {1}, {2}, {3}, {4}, {5}. tel. {6}, email {7}",
                                  person.HostPersonName, person.HostPersonSurname, 
                                  person.HostPersonZipCode, person.HostPersonAddress,
                                  person.HostPersonCity, person.HostPersonCountry, 
                                  person.HostPersonPhone, person.HostPersonEmail);
            });
            _linker.ActionFactory.RegisterAction("SetHostFullAddress", (control, linkedObject) =>
            {
                var person = (Person)linkedObject;
                person.HostCompanyNameAndAddress =
                    string.Format("{0}, {1}, {2}, {3}. tel. {4}, email {5}",
                                  person.HostCompanyZipCode, person.HostCompanyAddress,
                                  person.HostCompanyCity, person.HostCompanyCountry,
                                  person.HostCompanyPhone, person.HostCompanyEmail);
            });
            _linker.ActionFactory.RegisterAction("SetHotelFullAddress", (control, linkedObject) =>
            {
                var person = (Person)linkedObject;
                person.HostHotelFullAddress =
                    string.Format("{0}, {1}, {2}, {3}. tel. {4}, email {5}",
                                  person.HostHotelZipCode, person.HostHotelAddress,
                                  person.HostHotelCity, person.HostHotelCountry,
                                  person.HostHotelPhone, person.HostHotelEmail);
            });
        }

        private void PersonForm_Load(object sender, EventArgs e)
        {
            _linker = new Linker();
            InitActionFactory();
            if (FormMode == Mode.Create)
            {
                CreateNewPerson();
            }
            if (_person != null) _linker.LinkObjectToControl(this, _person);
        }

        private void CreateNewPerson()
        {
            _person = new Person();
            if (_templatePacket != null)
                if (_templatePacket.TemplatePerson != null)
                    _person.Merge(_templatePacket.TemplatePerson);
        }

        public void EditPerson(Person person)
        {
            _person = person;
            FormMode = Mode.Edit;
            btnClose.Visible = false;
            saveDialogResult = DialogResult.OK;
        }

        public void CreatePerson(Packet templatePacket = null)
        {
            FormMode = Mode.Create;
            saveDialogResult = DialogResult.None;
            btnClose.Visible = true;
            _templatePacket = templatePacket;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            DialogResult = saveDialogResult;

            if (NeedTranslit())
                ConvertAllToTranslit();

            _linker.MoveDataToObject();
            
            if (FormMode == Mode.Edit)
            {
                if (AutoSavePerson)
                    PersonStorage.Instance.Save(_person);
                ShowSavedLabel();
                if (saveDialogResult != DialogResult.None)
                    Close(); 
                return;
            }

            if (PersonCreated != null) PersonCreated(_person, EventArgs.Empty);
            ShowSavedLabel();

            _linker = null;
            CreateNewPerson();
            _linker = new Linker();
            InitActionFactory();
            if (_linker == null) return;
            _linker.LinkObjectToControl(this, _person);
            _linker.MoveDataFromObject();
            _linker.MoveDataToObject();

            if (saveDialogResult != DialogResult.None)
                Close();
        }

        private void ShowSavedLabel()
        {
            var tim = new Timer {Interval = 2500};
            lblSaved.Visible = true;
            tim.Tick += (o, args) =>
                            {
                                tim.Stop();
                                lblSaved.Visible = false;
                            };
            tim.Start();
        }

        private bool NeedTranslit()
        {
            return btnConvertMenu.Text == btnConvert.Text;
        }

        private void ConvertAllToTranslit()
        {
            var affectedControls = _linker.EnumLinkedControls();
            foreach (var ctrl in affectedControls)
            {
                if (ctrl is TextBox)
                {
                    // Временный костыль!!!!! TODO: Заменить на поле атрибута!
                    if (ctrl.Name == "personalId") continue;
                    var tb = ctrl as TextBox;
                    var txt = tb.Text;
                    tb.Text = TranslitConverter.Front(txt);
                } else if (ctrl is ComboBox)
                {
                    var cb = ctrl as ComboBox;
                    if (cb.DropDownStyle == ComboBoxStyle.DropDownList) continue;
                    var txt = cb.Text;
                    var conv = TranslitConverter.Front(txt);
                    cb.Text = conv;
                }
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; 
            Close(); // PersonStorage.LoadPerson(ref _person);
        }

        internal void InitCombos(IEnumerable<Person> allPersons)
        {
            foreach (var p in allPersons)
                Linker.FillComboBoxes(this, p);
        }

        private void loadFromScanner_Click(object sender, EventArgs e)
        {
            var pathObj = Settings.Default["ImportPath"];
            var path = string.Empty;
            if (pathObj == null)
                path = AppDomain.CurrentDomain.BaseDirectory;

            var dlg = new OpenFileDialog 
            {
// ReSharper disable LocalizableElement
                Filter = "*.xml|*.xml", 
// ReSharper restore LocalizableElement
                InitialDirectory = path, 
                CheckFileExists = true, 
                Multiselect = true
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            var fileName = Path.GetFileName(path);
            var newPath = path;
            if (fileName != null)
            {
                newPath = path.Substring(0, path.Length - fileName.Length);
            }

            Settings.Default["ImportPath"] = newPath;

            var files = dlg.FileNames;
            var scannedPerson = ScannerXmlParser.GetPerson(files);
            _person.Merge(scannedPerson);
            _linker.MoveDataFromObject();
        }

        private void scan_Click(object sender, EventArgs e)
        {
            
            if (Scanner.Instance.Valid)
            {
                if (!Scanner.Instance.HasScanner)
                    Scanner.Instance.InitTwain();
                if (!Scanner.Instance.HasScanner)
                {
                    MessageBox.Show(Resources.NoScanners);
                    return;
                }
                var ops = new BackgroundOptions
                              {
                                  IsBackground = true,
                                  IsAutoClose = true,
                                  AutoCloseDelay = 10000
                              };
                
                Scanner.Instance.Init(ops);
                if (Scanner.Instance.Success)
                {
                    var scannedPerson = Scanner.Instance.GetPerson();
                    if (scannedPerson != null)
                    {
                        _person.Merge(scannedPerson);
                        _linker.MoveDataFromObject();
                    }
                }
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            btnConvertMenu.Text = btnConvert.Text;
        }

        private void btnNotConvert_Click(object sender, EventArgs e)
        {
            btnConvertMenu.Text = btnNotConvert.Text;
        }

        private void btnBGHeader_Click(object sender, EventArgs e)
        {
            var form = new HeaderForm();
            form.EditPerson(_person);
            form.ShowDialog();
        }

        private void btnHost_Click(object sender, EventArgs e)
        {
            //var form = new HostForm();
            //form.EditPerson(_person);
            //form.ShowDialog();
        }
    }
}
