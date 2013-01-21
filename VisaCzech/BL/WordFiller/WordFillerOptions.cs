using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisaCzech.BL.Background;
using VisaCzech.BL.ObjFramework.ObjectContainerLinker;
using System.Windows.Forms;

namespace VisaCzech.BL.WordFiller
{
    public class WordFillerOptions
    {
        [Link(ControlName = "savePath", InitOnlyEmpty = true)]
        public string SavePath = string.Empty;

        [Link(ControlName = "templates", AllowFillComboBox = false)]
        public string TemplateName = string.Empty;

        public string PacketName;

        [Link(ControlName = "isBackground")]
        public bool IsBackground = true;

        public BackgroundOptions BackgroundOps;
    }
}
