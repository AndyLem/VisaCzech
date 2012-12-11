using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VisaCzech.Properties;
using VisaCzech.UI;

namespace VisaCzech.BL.WordFiller.FillerStatus
{
    public class FormStrategy : IFillerStatusStrategy
    {
        private static BackgroundWorker _worker;
        private static WordFillerProgressForm _form;

        public FormStrategy()
        {
            ShouldStop = false;
        }

        public BackgroundWorker Worker
        {
            get { return _worker; }
        }

        public void Init(ICollection<Person> persons, WordFillerOptions options)
        {
            _form = new WordFillerProgressForm();
            _worker = new BackgroundWorker { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
            _worker.ProgressChanged += (o, eventArgs) =>
            {
                _form.progress.Value = eventArgs.ProgressPercentage;
                _form.console.Items.Add(
                    eventArgs.UserState.ToString());
            };
            _worker.RunWorkerCompleted += (o, eventArgs) =>
            {
                _form.stop.Text = Resources.WordFiller_FillTemplate_CloseForm;
                _form.stop.Click +=
                    (sender1, args1) => _form.Close();
            };

            _form.stop.Click += (sender, args) =>
            {
                _form.console.Items.Add("Ожидается завершение текущей операции");
                ShouldStop = true;
            };
            _form.Load += (sender, args) => _worker.RunWorkerAsync();
        }

        public bool Run()
        {
            _form.ShowDialog();
            return true;
        }


        public bool ShouldStop { get; set; }
        
        public bool WasError { get; set; }
    }
}
