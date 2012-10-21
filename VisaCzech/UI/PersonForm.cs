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

        public PersonForm()
        {
            InitializeComponent();
            LinkActionFactory.Instance.RegisterAction("SexChanged", (control, linkedObject) =>
                {
                    var person = (Person) linkedObject;
                    var cbb = (ComboBox) control;
                    person.Sex = (Sex)cbb.SelectedIndex;
                });
            LinkActionFactory.Instance.RegisterAction("FamilyChanged", (control, linkedObject) =>
                {
                    var person = (Person)linkedObject;
                    var cbb = (ComboBox)control;
                    person.Status = (Status)cbb.SelectedIndex;
                });
            LinkActionFactory.Instance.RegisterAction("DocTypeChanged", (control, linkedObject) =>
                {
                    var person = (Person)linkedObject;
                    var cbb = (ComboBox)control;
                    person.DocumentType = (DocType)cbb.SelectedIndex;
                });
            LinkActionFactory.Instance.RegisterAction("Visa1EnabledChanged", (control, linkedObject) =>
                {
                    var person = (Person)linkedObject;
                    this.visa1To.Enabled = this.visa1From.Enabled = person.Visa1Enabled;
                });
            LinkActionFactory.Instance.RegisterAction("Visa2EnabledChanged", (control, linkedObject) =>
                {
                    var person = (Person)linkedObject;
                    this.visa2To.Enabled = this.visa2From.Enabled = person.Visa2Enabled;
                });
            LinkActionFactory.Instance.RegisterAction("Visa3EnabledChanged", (control, linkedObject) =>
                {
                    var person = (Person)linkedObject;
                    this.visa3To.Enabled = this.visa3From.Enabled = person.Visa3Enabled;
                });
        }

        private void PersonForm_Load(object sender, EventArgs e)
        {
            _linker = new Linker();
            _person = new Person();
            _linker.LinkObjectToControl(this.tableLayoutPanel1, _person);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            _linker.MoveDataToObject();
            PersonStorage.SavePerson(_person);
        }
    }
}
