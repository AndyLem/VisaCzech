using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisaCzech.BL.WordFiller;
using VisaCzech.BL.ObjFramework.ObjectContainerLinker;
using VisaCzech.DL;

namespace VisaCzech.UI
{
    public partial class WordFillerForm : Form
    {
        private Linker _linker;
        private string _packetName;

        public WordFillerForm()
        {
            InitializeComponent();
        }

        public void Link(WordFillerOptions options)
        {
            _packetName = options.PacketName;
            _linker = new Linker();
            _linker.LinkObjectToControl(this, options);
        }

        private void browse_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog { SelectedPath = AppDomain.CurrentDomain.BaseDirectory };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            savePath.Text = dlg.SelectedPath;
        }

        private void WordFillerForm_Load(object sender, EventArgs e)
        {
            savePath.Text = string.Format("{0}Anketa\\{1}\\", AppDomain.CurrentDomain.BaseDirectory, _packetName);
            templates.Items.AddRange(TemplateStorage.LoadTemplates().ToArray());
            templates.SelectedIndex = templates.Items.Count > 0 ? 0 : -1;
            okBtn.Enabled = templates.Items.Count > 0;
        }
    }
}
