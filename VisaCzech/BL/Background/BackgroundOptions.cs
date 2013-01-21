using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisaCzech.BL.Background
{
    public class BackgroundOptions
    {
        public bool IsBackground = true;
        public ProgressBar BackgroundProgressBar;
        public Button BackgroundStopButton;
        public bool IsAutoClose = false;
        public int AutoCloseDelay = 2000;
    }
}
