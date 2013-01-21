namespace VisaCzech.UI
{
    partial class BackgroundProgressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.console = new System.Windows.Forms.ListBox();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.stop = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // console
            // 
            this.console.FormattingEnabled = true;
            this.console.HorizontalScrollbar = true;
            this.console.Location = new System.Drawing.Point(12, 12);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(545, 225);
            this.console.TabIndex = 0;
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(12, 243);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(545, 23);
            this.progress.TabIndex = 1;
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(482, 272);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(75, 23);
            this.stop.TabIndex = 2;
            this.stop.Text = "Прервать";
            this.stop.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.console);
            this.panel1.Controls.Add(this.stop);
            this.panel1.Controls.Add(this.progress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(569, 307);
            this.panel1.TabIndex = 3;
            // 
            // WordFillerProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 307);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WordFillerProgressForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WordFillerProgressForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ListBox console;
        public System.Windows.Forms.ProgressBar progress;
        public System.Windows.Forms.Button stop;
    }
}