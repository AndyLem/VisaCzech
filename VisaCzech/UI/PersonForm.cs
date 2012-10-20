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

namespace VisaCzech.UI
{
    public partial class PersonForm : Form
    {
        private Linker _linker;
        private VisaCzech.BL.Person _person;

        public PersonForm()
        {
            InitializeComponent();
        }

        private void PersonForm_Load(object sender, EventArgs e)
        {
            _linker = new Linker();
            _person = new Person();
            _linker.LinkObjectToControl(this.tableLayoutPanel1, _person);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_person.Surname);
        }
    }
}
