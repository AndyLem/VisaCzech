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
            this.label1 = new System.Windows.Forms.Label();
            this.createPerson = new System.Windows.Forms.Button();
            this.deletePersons = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.personFilter = new System.Windows.Forms.TextBox();
            this.personsList = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.packetList = new System.Windows.Forms.ListBox();
            this.removeFromPacket = new System.Windows.Forms.Button();
            this.newPacket = new System.Windows.Forms.Button();
            this.addToPacket = new System.Windows.Forms.Button();
            this.fillAnketas = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.templates = new System.Windows.Forms.ComboBox();
            this.refreshTemplates = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.addToPacket);
            this.panel1.Controls.Add(this.personsList);
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
            // deletePersons
            // 
            this.deletePersons.Location = new System.Drawing.Point(87, 59);
            this.deletePersons.Name = "deletePersons";
            this.deletePersons.Size = new System.Drawing.Size(75, 23);
            this.deletePersons.TabIndex = 2;
            this.deletePersons.Text = "Удалить";
            this.deletePersons.UseVisualStyleBackColor = true;
            this.deletePersons.Click += new System.EventHandler(this.deletePersons_Click);
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
            // personFilter
            // 
            this.personFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.personFilter.Enabled = false;
            this.personFilter.Location = new System.Drawing.Point(56, 26);
            this.personFilter.Name = "personFilter";
            this.personFilter.Size = new System.Drawing.Size(264, 20);
            this.personFilter.TabIndex = 4;
            // 
            // personsList
            // 
            this.personsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.personsList.FormattingEnabled = true;
            this.personsList.HorizontalScrollbar = true;
            this.personsList.IntegralHeight = false;
            this.personsList.Location = new System.Drawing.Point(6, 88);
            this.personsList.Name = "personsList";
            this.personsList.ScrollAlwaysVisible = true;
            this.personsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.personsList.Size = new System.Drawing.Size(314, 509);
            this.personsList.TabIndex = 5;
            this.personsList.DoubleClick += new System.EventHandler(this.personsList_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.refreshTemplates);
            this.panel2.Controls.Add(this.templates);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.fillAnketas);
            this.panel2.Controls.Add(this.newPacket);
            this.panel2.Controls.Add(this.removeFromPacket);
            this.panel2.Controls.Add(this.packetList);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(338, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(473, 600);
            this.panel2.TabIndex = 1;
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
            // packetList
            // 
            this.packetList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.packetList.FormattingEnabled = true;
            this.packetList.HorizontalScrollbar = true;
            this.packetList.IntegralHeight = false;
            this.packetList.Location = new System.Drawing.Point(6, 88);
            this.packetList.Name = "packetList";
            this.packetList.ScrollAlwaysVisible = true;
            this.packetList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.packetList.Size = new System.Drawing.Size(464, 509);
            this.packetList.TabIndex = 6;
            // 
            // removeFromPacket
            // 
            this.removeFromPacket.Location = new System.Drawing.Point(6, 59);
            this.removeFromPacket.Name = "removeFromPacket";
            this.removeFromPacket.Size = new System.Drawing.Size(122, 23);
            this.removeFromPacket.TabIndex = 7;
            this.removeFromPacket.Text = "Удалить из пакета";
            this.removeFromPacket.UseVisualStyleBackColor = true;
            this.removeFromPacket.Click += new System.EventHandler(this.removeFromPacket_Click);
            // 
            // newPacket
            // 
            this.newPacket.Location = new System.Drawing.Point(134, 59);
            this.newPacket.Name = "newPacket";
            this.newPacket.Size = new System.Drawing.Size(102, 23);
            this.newPacket.TabIndex = 8;
            this.newPacket.Text = "Новый пакет";
            this.newPacket.UseVisualStyleBackColor = true;
            this.newPacket.Click += new System.EventHandler(this.newPacket_Click);
            // 
            // addToPacket
            // 
            this.addToPacket.Location = new System.Drawing.Point(199, 59);
            this.addToPacket.Name = "addToPacket";
            this.addToPacket.Size = new System.Drawing.Size(121, 23);
            this.addToPacket.TabIndex = 6;
            this.addToPacket.Text = "Добавить в пакет >>";
            this.addToPacket.UseVisualStyleBackColor = true;
            this.addToPacket.Click += new System.EventHandler(this.addToPacket_Click);
            // 
            // fillAnketas
            // 
            this.fillAnketas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fillAnketas.Location = new System.Drawing.Point(339, 59);
            this.fillAnketas.Name = "fillAnketas";
            this.fillAnketas.Size = new System.Drawing.Size(131, 23);
            this.fillAnketas.TabIndex = 9;
            this.fillAnketas.Text = "Заполнить анкеты";
            this.fillAnketas.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Шаблоны анкет";
            // 
            // templates
            // 
            this.templates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.templates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.templates.FormattingEnabled = true;
            this.templates.Location = new System.Drawing.Point(256, 26);
            this.templates.Name = "templates";
            this.templates.Size = new System.Drawing.Size(133, 21);
            this.templates.TabIndex = 11;
            // 
            // refreshTemplates
            // 
            this.refreshTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshTemplates.Location = new System.Drawing.Point(395, 24);
            this.refreshTemplates.Name = "refreshTemplates";
            this.refreshTemplates.Size = new System.Drawing.Size(75, 23);
            this.refreshTemplates.TabIndex = 12;
            this.refreshTemplates.Text = "Обновить";
            this.refreshTemplates.UseVisualStyleBackColor = true;
            this.refreshTemplates.Click += new System.EventHandler(this.refreshTemplates_Click);
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
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox personsList;
        private System.Windows.Forms.TextBox personFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button deletePersons;
        private System.Windows.Forms.Button createPerson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addToPacket;
        private System.Windows.Forms.Button refreshTemplates;
        private System.Windows.Forms.ComboBox templates;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button fillAnketas;
        private System.Windows.Forms.Button newPacket;
        private System.Windows.Forms.Button removeFromPacket;
        private System.Windows.Forms.ListBox packetList;

    }
}

