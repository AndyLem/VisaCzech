using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisaCzech.UI
{
    public partial class CheckResultForm : Form
    {
        public CheckResultForm()
        {
            InitializeComponent();
        }

        public void Init(IEnumerable<string> errors, bool criticalStop)
        {
            foreach (var line in errors) lbErrors.Items.Add(line);
            btnSave.Enabled = !criticalStop;
        }
    }
}
