namespace VisaCzech.UI
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.createToPacket = new System.Windows.Forms.CheckBox();
            this.addToPacket = new System.Windows.Forms.Button();
            this.personFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.deletePersons = new System.Windows.Forms.Button();
            this.createPerson = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSaveBGXml = new System.Windows.Forms.Button();
            this.backgroundProgress = new System.Windows.Forms.ProgressBar();
            this.backgroundStop = new System.Windows.Forms.Button();
            this.fillSelectedAnketas = new System.Windows.Forms.Button();
            this.renamePacket = new System.Windows.Forms.Button();
            this.deletePacket = new System.Windows.Forms.Button();
            this.packetsList = new System.Windows.Forms.ComboBox();
            this.fillAnketas = new System.Windows.Forms.Button();
            this.newPacket = new System.Windows.Forms.Button();
            this.removeFromPacket = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.currentPacketList = new VisaCzech.UI.TouchListBox();
            this.personsList = new VisaCzech.UI.TouchListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.createToPacket);
            this.panel1.Controls.Add(this.personsList);
            this.panel1.Controls.Add(this.addToPacket);
            this.panel1.Controls.Add(this.personFilter);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.deletePersons);
            this.panel1.Controls.Add(this.createPerson);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(323, 600);
            this.panel1.TabIndex = 0;
            // 
            // createToPacket
            // 
            this.createToPacket.AutoSize = true;
            this.createToPacket.Checked = true;
            this.createToPacket.CheckState = System.Windows.Forms.CheckState.Checked;
            this.createToPacket.Location = new System.Drawing.Point(87, 63);
            this.createToPacket.Name = "createToPacket";
            this.createToPacket.Size = new System.Drawing.Size(123, 17);
            this.createToPacket.TabIndex = 8;
            this.createToPacket.Text = "и добавить в пакет";
            this.createToPacket.UseVisualStyleBackColor = true;
            // 
            // addToPacket
            // 
            this.addToPacket.Location = new System.Drawing.Point(287, 59);
            this.addToPacket.Name = "addToPacket";
            this.addToPacket.Size = new System.Drawing.Size(33, 23);
            this.addToPacket.TabIndex = 6;
            this.addToPacket.Text = ">>";
            this.addToPacket.UseVisualStyleBackColor = true;
            this.addToPacket.Click += new System.EventHandler(this.addToPacket_Click);
            // 
            // personFilter
            // 
            this.personFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.personFilter.Location = new System.Drawing.Point(56, 26);
            this.personFilter.Name = "personFilter";
            this.personFilter.Size = new System.Drawing.Size(264, 20);
            this.personFilter.TabIndex = 4;
            this.personFilter.TextChanged += new System.EventHandler(this.personFilter_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Фильтр";
            // 
            // deletePersons
            // 
            this.deletePersons.Location = new System.Drawing.Point(216, 59);
            this.deletePersons.Name = "deletePersons";
            this.deletePersons.Size = new System.Drawing.Size(65, 23);
            this.deletePersons.TabIndex = 2;
            this.deletePersons.Text = "Удалить";
            this.deletePersons.UseVisualStyleBackColor = true;
            this.deletePersons.Click += new System.EventHandler(this.deletePersons_Click);
            // 
            // createPerson
            // 
            this.createPerson.Location = new System.Drawing.Point(6, 59);
            this.createPerson.Name = "createPerson";
            this.createPerson.Size = new System.Drawing.Size(75, 23);
            this.createPerson.TabIndex = 1;
            this.createPerson.Text = "Создать";
            this.createPerson.UseVisualStyleBackColor = true;
            this.createPerson.Click += new System.EventHandler(this.createPerson_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Анкеты";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnSaveBGXml);
            this.panel2.Controls.Add(this.backgroundProgress);
            this.panel2.Controls.Add(this.backgroundStop);
            this.panel2.Controls.Add(this.fillSelectedAnketas);
            this.panel2.Controls.Add(this.renamePacket);
            this.panel2.Controls.Add(this.deletePacket);
            this.panel2.Controls.Add(this.packetsList);
            this.panel2.Controls.Add(this.currentPacketList);
            this.panel2.Controls.Add(this.fillAnketas);
            this.panel2.Controls.Add(this.newPacket);
            this.panel2.Controls.Add(this.removeFromPacket);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(338, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(473, 600);
            this.panel2.TabIndex = 1;
            // 
            // btnSaveBGXml
            // 
            this.btnSaveBGXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveBGXml.Location = new System.Drawing.Point(65, 59);
            this.btnSaveBGXml.Name = "btnSaveBGXml";
            this.btnSaveBGXml.Size = new System.Drawing.Size(143, 23);
            this.btnSaveBGXml.TabIndex = 20;
            this.btnSaveBGXml.Text = "Сохранить XML";
            this.btnSaveBGXml.UseVisualStyleBackColor = true;
            this.btnSaveBGXml.Click += new System.EventHandler(this.btnSaveBGXml_Click);
            // 
            // backgroundProgress
            // 
            this.backgroundProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.backgroundProgress.Location = new System.Drawing.Point(6, 574);
            this.backgroundProgress.Name = "backgroundProgress";
            this.backgroundProgress.Size = new System.Drawing.Size(383, 23);
            this.backgroundProgress.TabIndex = 19;
            // 
            // backgroundStop
            // 
            this.backgroundStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.backgroundStop.Enabled = false;
            this.backgroundStop.Location = new System.Drawing.Point(395, 574);
            this.backgroundStop.Name = "backgroundStop";
            this.backgroundStop.Size = new System.Drawing.Size(75, 23);
            this.backgroundStop.TabIndex = 18;
            this.backgroundStop.Text = "Прервать";
            this.backgroundStop.UseVisualStyleBackColor = true;
            // 
            // fillSelectedAnketas
            // 
            this.fillSelectedAnketas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fillSelectedAnketas.Location = new System.Drawing.Point(214, 59);
            this.fillSelectedAnketas.Name = "fillSelectedAnketas";
            this.fillSelectedAnketas.Size = new System.Drawing.Size(141, 23);
            this.fillSelectedAnketas.TabIndex = 17;
            this.fillSelectedAnketas.Text = "Заполнить выделенные";
            this.fillSelectedAnketas.UseVisualStyleBackColor = true;
            this.fillSelectedAnketas.Click += new System.EventHandler(this.fillSelectedAnketas_Click);
            // 
            // renamePacket
            // 
            this.renamePacket.Location = new System.Drawing.Point(283, 24);
            this.renamePacket.Name = "renamePacket";
            this.renamePacket.Size = new System.Drawing.Size(106, 23);
            this.renamePacket.TabIndex = 16;
            this.renamePacket.Text = "Редактировать";
            this.renamePacket.UseVisualStyleBackColor = true;
            this.renamePacket.Click += new System.EventHandler(this.renamePacket_Click);
            // 
            // deletePacket
            // 
            this.deletePacket.Location = new System.Drawing.Point(395, 24);
            this.deletePacket.Name = "deletePacket";
            this.deletePacket.Size = new System.Drawing.Size(75, 23);
            this.deletePacket.TabIndex = 15;
            this.deletePacket.Text = "Удалить";
            this.deletePacket.UseVisualStyleBackColor = true;
            this.deletePacket.Click += new System.EventHandler(this.deletePacket_Click);
            // 
            // packetsList
            // 
            this.packetsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.packetsList.FormattingEnabled = true;
            this.packetsList.Location = new System.Drawing.Point(6, 26);
            this.packetsList.Name = "packetsList";
            this.packetsList.Size = new System.Drawing.Size(202, 21);
            this.packetsList.Sorted = true;
            this.packetsList.TabIndex = 14;
            this.packetsList.SelectedIndexChanged += new System.EventHandler(this.packetsList_SelectedIndexChanged);
            // 
            // fillAnketas
            // 
            this.fillAnketas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fillAnketas.Location = new System.Drawing.Point(361, 59);
            this.fillAnketas.Name = "fillAnketas";
            this.fillAnketas.Size = new System.Drawing.Size(109, 23);
            this.fillAnketas.TabIndex = 9;
            this.fillAnketas.Text = "Заполнить все";
            this.fillAnketas.UseVisualStyleBackColor = true;
            this.fillAnketas.Click += new System.EventHandler(this.fillAnketas_Click);
            // 
            // newPacket
            // 
            this.newPacket.Location = new System.Drawing.Point(214, 24);
            this.newPacket.Name = "newPacket";
            this.newPacket.Size = new System.Drawing.Size(63, 23);
            this.newPacket.TabIndex = 8;
            this.newPacket.Text = "Новый";
            this.newPacket.UseVisualStyleBackColor = true;
            this.newPacket.Click += new System.EventHandler(this.newPacket_Click);
            // 
            // removeFromPacket
            // 
            this.removeFromPacket.Location = new System.Drawing.Point(6, 59);
            this.removeFromPacket.Name = "removeFromPacket";
            this.removeFromPacket.Size = new System.Drawing.Size(33, 23);
            this.removeFromPacket.TabIndex = 7;
            this.removeFromPacket.Text = "<<";
            this.removeFromPacket.UseVisualStyleBackColor = true;
            this.removeFromPacket.Click += new System.EventHandler(this.removeFromPacket_Click);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(473, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Пакет";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // currentPacketList
            // 
            this.currentPacketList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentPacketList.ArrowColor = System.Drawing.SystemColors.Control;
            this.currentPacketList.ArrowSize = 20;
            this.currentPacketList.ArrowSizeMode = VisaCzech.UI.TouchListBox.ArrowSizeModes.Pixels;
            this.currentPacketList.ArrowSizePercents = 70;
            this.currentPacketList.DefaultSelectedIndex = -1;
            this.currentPacketList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.currentPacketList.FormattingEnabled = true;
            this.currentPacketList.FrameColor = System.Drawing.Color.White;
            this.currentPacketList.IntegralHeight = false;
            this.currentPacketList.ItemHeight = 30;
            this.currentPacketList.Location = new System.Drawing.Point(6, 88);
            this.currentPacketList.MarkVisitedItems = true;
            this.currentPacketList.Name = "currentPacketList";
            this.currentPacketList.NotVisitedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.currentPacketList.ScrollAlwaysVisible = true;
            this.currentPacketList.SelectedItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.currentPacketList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.currentPacketList.ShowArrow = false;
            this.currentPacketList.Size = new System.Drawing.Size(464, 480);
            this.currentPacketList.TabIndex = 13;
            this.currentPacketList.VisitedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.currentPacketList.DrawItemText += new VisaCzech.UI.TouchListBox.OnDrawText(this.personsList_DrawItemText);
            // 
            // personsList
            // 
            this.personsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.personsList.ArrowColor = System.Drawing.SystemColors.Control;
            this.personsList.ArrowSize = 20;
            this.personsList.ArrowSizeMode = VisaCzech.UI.TouchListBox.ArrowSizeModes.Pixels;
            this.personsList.ArrowSizePercents = 70;
            this.personsList.DefaultSelectedIndex = -1;
            this.personsList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.personsList.FormattingEnabled = true;
            this.personsList.FrameColor = System.Drawing.Color.White;
            this.personsList.HorizontalScrollbar = true;
            this.personsList.IntegralHeight = false;
            this.personsList.ItemHeight = 30;
            this.personsList.Location = new System.Drawing.Point(6, 88);
            this.personsList.MarkVisitedItems = true;
            this.personsList.Name = "personsList";
            this.personsList.NotVisitedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.personsList.SelectedItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.personsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.personsList.ShowArrow = false;
            this.personsList.Size = new System.Drawing.Size(314, 509);
            this.personsList.TabIndex = 7;
            this.personsList.VisitedColor = System.Drawing.Color.White;
            this.personsList.DrawItemText += new VisaCzech.UI.TouchListBox.OnDrawText(this.personsList_DrawItemText);
            this.personsList.SelectedIndexChanged += new System.EventHandler(this.personsList_SelectedIndexChanged);
            this.personsList.DoubleClick += new System.EventHandler(this.personsList_DoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 624);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EuroAnketa";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox personFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button deletePersons;
        private System.Windows.Forms.Button createPerson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addToPacket;
        private System.Windows.Forms.Button fillAnketas;
        private System.Windows.Forms.Button newPacket;
        private System.Windows.Forms.Button removeFromPacket;
        private TouchListBox personsList;
        private TouchListBox currentPacketList;
        private System.Windows.Forms.ComboBox packetsList;
        private System.Windows.Forms.Button renamePacket;
        private System.Windows.Forms.Button deletePacket;
        private System.Windows.Forms.Button fillSelectedAnketas;
        private System.Windows.Forms.CheckBox createToPacket;
        private System.Windows.Forms.ProgressBar backgroundProgress;
        private System.Windows.Forms.Button backgroundStop;
        private System.Windows.Forms.Button btnSaveBGXml;

    }
}

