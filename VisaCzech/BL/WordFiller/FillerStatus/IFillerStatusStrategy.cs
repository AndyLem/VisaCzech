using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VisaCzech.BL.WordFiller.FillerStatus
{
    public interface IFillerStatusStrategy
    {
        BackgroundWorker Worker { get; }
        bool ShouldStop { get; set; }
        bool WasError { get; set; }
        void Init(ICollection<Person> persons, WordFillerOptions options);
        bool Run();
    }
}
