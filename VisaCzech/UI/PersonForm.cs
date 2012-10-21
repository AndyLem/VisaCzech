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

namespace VisaCzech.UI
{
    public partial class PersonForm : Form
    {
        private Linker _linker;
        private VisaCzech.BL.Person _person;

        private enum Mode
        {
            Edit,
            Create
        }

        public EventHandler PersonCreated;

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
                                                                       var person = (Person) linkedObject;
                                                                       var cbb = (ComboBox) control;
                                                                       person.Sex = (Sex) cbb.SelectedIndex;
                                                                   });
            _linker.ActionFactory.RegisterAction("FamilyChanged", (control, linkedObject) =>
                                                                      {
                                                                          var person = (Person) linkedObject;
                                                                          var cbb = (ComboBox) control;
                                                                          person.Status = (Status) cbb.SelectedIndex;
                                                                      });
            _linker.ActionFactory.RegisterAction("DocTypeChanged", (control, linkedObject) =>
                                                                       {
                                                                           var person = (Person) linkedObject;
                                                                           var cbb = (ComboBox) control;
                                                                           person.DocumentType =
                                                                               (DocType) cbb.SelectedIndex;
                                                                       });
            _linker.ActionFactory.RegisterAction("Visa1EnabledChanged", (control, linkedObject) =>
                                                                            {
                                                                                var person = (Person) linkedObject;
                                                                                this.visa1To.Enabled =
                                                                                    this.visa1From.Enabled =
                                                                                    person.Visa1Enabled;
                                                                            });
            _linker.ActionFactory.RegisterAction("Visa2EnabledChanged", (control, linkedObject) =>
                                                                            {
                                                                                var person = (Person) linkedObject;
                                                                                this.visa2To.Enabled =
                                                                                    this.visa2From.Enabled =
                                                                                    person.Visa2Enabled;
                                                                            });
            _linker.ActionFactory.RegisterAction("Visa3EnabledChanged", (control, linkedObject) =>
                                                                            {
                                                                                var person = (Person) linkedObject;
                                                                                this.visa3To.Enabled =
                                                                                    this.visa3From.Enabled =
                                                                                    person.Visa3Enabled;
                                                                            });
        }

        private void PersonForm_Load(object sender, EventArgs e)
        {
            _linker = new Linker();
            InitActionFactory();
            if (FormMode == Mode.Create)
            {
                _person = new Person();
            }
            if (_person != null) _linker.LinkObjectToControl(this, _person);
        }

        public void EditPerson(Person person)
        {
            _person = person;
            FormMode = Mode.Edit;
            cancelBtn.Hide();
            saveBtn.DialogResult = DialogResult.OK;
        }

        public void CreatePerson()
        {
            FormMode = Mode.Create;
            saveBtn.DialogResult = DialogResult.None;
            cancelBtn.Show();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            _linker.MoveDataToObject();
            PersonStorage.SavePerson(_person);
            if (FormMode == Mode.Edit) return;

            if (PersonCreated != null) PersonCreated(_person, EventArgs.Empty);
            _linker = null;
            _person = new Person();
            _linker = new Linker();
            InitActionFactory();
            if (_linker != null) _linker.LinkObjectToControl(this, _person);
            var tim = new Timer {Interval = 2500};
            saved.Show();
            tim.Tick += (o, args) =>
                            {
                                tim.Stop();
                                saved.Hide();
                            };
            tim.Start();
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
    }
}
