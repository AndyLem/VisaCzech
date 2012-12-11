using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VisaCzech.Properties;

namespace VisaCzech.BL.WordFiller.FillerStatus
{
    public class BackgroundStrategy : IFillerStatusStrategy
    {
        private static BackgroundWorker _worker;

        public BackgroundWorker Worker
        {
            get { return _worker; }
        }

        public void Init(ICollection<Person> persons, WordFillerOptions options)
        {
            _worker = new BackgroundWorker { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
            _worker.ProgressChanged +=
                (o, eventArgs) => options.BackgroundProgressBar.Value = eventArgs.ProgressPercentage;
            options.BackgroundProgressBar.Value = 0;
            options.BackgroundStopButton.Text = "Прервать";
            options.BackgroundStopButton.Enabled = true;
            options.BackgroundStopButton.Click += (sender, args) =>
                {
                    options.BackgroundStopButton.Text = "Ожидаем...";
                    options.BackgroundStopButton.Enabled = false;
                    ShouldStop = true;
                };
            _worker.RunWorkerCompleted += (o, eventArgs) =>
                {
                    options.BackgroundStopButton.Text = "Завершено";
                    options.BackgroundStopButton.Enabled = false;
                };
        }

        public bool Run()
        {
            _worker.RunWorkerAsync();
            return true;
        }

        public bool ShouldStop { get; set; }

        public bool WasError { get; set; }
    }
}
