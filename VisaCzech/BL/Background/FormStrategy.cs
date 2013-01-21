using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VisaCzech.Properties;
using VisaCzech.UI;
using System.Timers;

namespace VisaCzech.BL.Background
{
    public class FormStrategy : IBackgroundStrategy
    {
        private static BackgroundWorker _worker;
        private static BackgroundProgressForm _form;

        public FormStrategy()
        {
            ShouldStop = false;
        }

        public BackgroundWorker Worker
        {
            get { return _worker; }
        }

        public void Init(BackgroundOptions options)
        {
            _form = new BackgroundProgressForm();
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
                    (sender, args1) => _form.Close();
                if (!options.IsAutoClose) return;
                var tim = new Timer(500);
                var elapsed = 0.0d;
                tim.Elapsed += (sender, args) =>
                                   {
                                       elapsed += tim.Interval;
                                       _form.stop.Text = string.Format("{0} ({1})", Resources.WordFiller_FillTemplate_CloseForm,(int)((options.AutoCloseDelay - elapsed)/1000));
                                       if (elapsed < options.AutoCloseDelay) return;
                                       tim.Stop();
                                       _form.Close();
                                   };
                _form.stop.Click += (sender, args) => tim.Stop();
                tim.Start();
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
