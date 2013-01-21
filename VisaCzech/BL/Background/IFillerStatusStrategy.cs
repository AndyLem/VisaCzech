using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VisaCzech.BL.Background
{
    public interface IBackgroundStrategy
    {
        BackgroundWorker Worker { get; }
        bool ShouldStop { get; set; }
        bool WasError { get; set; }
        void Init(BackgroundOptions options);
        bool Run();
    }
}
