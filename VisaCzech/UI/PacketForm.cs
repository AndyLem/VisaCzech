using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisaCzech.BL.ObjFramework.ObjectContainerLinker;
using VisaCzech.BL;

namespace VisaCzech.UI
{
    public partial class PacketForm : Form
    {
        readonly Linker _linker = new Linker();
        private Packet passedPacket;
        private Packet linkedPacket;

        public PacketForm()
        {
            InitializeComponent();
        }

        internal void EditPacket(BL.Packet packet)
        {
            passedPacket = packet;
            linkedPacket = new Packet();
            linkedPacket.Merge(passedPacket);
            _linker.LinkObjectToControl(this, linkedPacket);    
        }

        private void save_Click(object sender, EventArgs e)
        {
            passedPacket.Merge(linkedPacket);
            DialogResult = DialogResult.OK;
        }
    }
}
