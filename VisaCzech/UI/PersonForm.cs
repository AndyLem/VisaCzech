using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisaCzech.BL;
using VisaCzech.BL.ObjFramework.ObjectContainerLinker;
using VisaCzech.DL;
using VisaCzech.BL.ScannerXmlParser;
using VisaCzech.BL.TranslitConverter;
using VisaCzech.Properties;
using System.IO;

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
            cancelBtn.Hide();
            saveBtn.DialogResult = DialogResult.OK;
        }

        public void CreatePerson(Packet templatePacket = null)
        {
            FormMode = Mode.Create;
            saveBtn.DialogResult = DialogResult.None;
            cancelBtn.Show();
            _templatePacket = templatePacket;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (toTranslit.Checked)
                ConvertAllToTranslit();

            _linker.MoveDataToObject();
            
            if (FormMode == Mode.Edit)
            {
                if (AutoSavePerson)
                    PersonStorage.Instance.Save(_person);
                return;
            }

            if (PersonCreated != null) PersonCreated(_person, EventArgs.Empty);
            var tim = new Timer { Interval = 2500 };
            saved.Show();
            tim.Tick += (o, args) =>
            {
                tim.Stop();
                saved.Hide();
            };
            tim.Start();


            _linker = null;
            CreateNewPerson();
            _linker = new Linker();
            InitActionFactory();
            if (_linker == null) return;
            _linker.LinkObjectToControl(this, _person);
            _linker.MoveDataFromObject();
            _linker.MoveDataToObject();
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
                    cb.Text = TranslitConverter.Front(txt);
                }
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            // PersonStorage.LoadPerson(ref _person);
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
    }
}
