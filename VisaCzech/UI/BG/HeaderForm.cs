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

namespace VisaCzech.UI.BG
{
    public partial class HeaderForm : Form
    {

        private Linker _linker;
        private VisaCzech.BL.Person _person;

        public HeaderForm()
        {
            InitializeComponent();
        }

        private void HeaderForm_Load(object sender, EventArgs e)
        {
            _linker = new Linker();
            if (_person != null) _linker.LinkObjectToControl(this, _person);

        }

        public void EditPerson(Person person)
        {
            _person = person;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            _linker.MoveDataToObject();
        }
    }
}
