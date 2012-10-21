using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisaCzech.Properties;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using VisaCzech.DL;
using VisaCzech.BL;

namespace VisaCzech.UI
{
    public partial class MainForm : Form
    {
        private Word._Application app;
        private Word._Document doc;
        private object missingObj = System.Reflection.Missing.Value;
        private object trueObj = true;
        private object falseObj = false;

        private List<Person> _packet;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _packet = new List<Person>();
            personsList.Items.AddRange(PersonStorage.LoadAllPersons().ToArray());
            RefreshTemplates();
            //app = new Word.Application();
            //object path = @"d:\anketa.dotx";
            //try
            //{
            //    doc = app.Documents.Add(ref path, ref missingObj, ref missingObj, ref missingObj);
            //}
            //catch (Exception)
            //{
            //    doc.Close(ref falseObj);
            //    app.Quit(ref falseObj);
            //    doc = null;
            //    app = null;
            //    throw;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //object strToFindObj = textBox1.Text;
            //object replaceStrObj = textBox2.Text;
            //object replaceTypeObj = Word.WdReplace.wdReplaceAll;

            //for (var i = 1; i <= doc.Sections.Count; i++)
            //{
            //    var wordRange = doc.Sections[i].Range;

            //    var wordFindObj = wordRange.Find;
            //    var wordFindParameters = new object[15]
            //                                      {
            //                                          strToFindObj, missingObj, missingObj, missingObj, missingObj,
            //                                          missingObj,
            //                                          missingObj, missingObj, missingObj, replaceStrObj, replaceTypeObj,
            //                                          missingObj, missingObj, missingObj, missingObj
            //                                      };

            //    wordFindObj.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, wordFindObj,
            //                                       wordFindParameters);

            //}
            //doc.Save();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Person person in personsList.Items)
                PersonStorage.SavePerson(person);
        }

        private void createPerson_Click(object sender, EventArgs e)
        {
            var form = new PersonForm();
            var allPersons = personsList.Items.Cast<Person>().ToList();
            form.InitCombos(allPersons);
            form.PersonCreated += (o, args) => personsList.Items.Add(o);
            form.CreatePerson();
            form.ShowDialog();
        }

        private void personsList_DoubleClick(object sender, EventArgs e)
        {
            if (personsList.SelectedItem == null) return;
            var form = new PersonForm();
            var person = personsList.SelectedItem as Person;
            var allPersons = personsList.Items.Cast<Person>().ToList();
            form.InitCombos(allPersons);
            form.EditPerson(person);
            if (form.ShowDialog() != DialogResult.OK) return;
            var index = personsList.SelectedIndex;
            personsList.Items.RemoveAt(index);
            personsList.Items.Insert(index, person);
            personsList.SelectedIndex = index;
        }

        private void deletePersons_Click(object sender, EventArgs e)
        {
            if (personsList.SelectedItem == null) return;
            var person = personsList.SelectedItem as Person;
            var index = personsList.SelectedIndex;
            if (person == null ||
                MessageBox.Show(string.Format("Удалить {0}", person.ToString()), Resources.DeleteAnketa,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            personsList.Items.RemoveAt(index);
            PersonStorage.DeletePerson(person);
        }

        private void newPacket_Click(object sender, EventArgs e)
        {
            _packet = new List<Person>();
            packetList.Items.Clear();
        }

        private void addToPacket_Click(object sender, EventArgs e)
        {
            foreach (var p in from Person p in personsList.SelectedItems where packetList.Items.IndexOf(p) == -1 select p)
                packetList.Items.Add(p);
        }

        private void removeFromPacket_Click(object sender, EventArgs e)
        {
            var indices = new int[packetList.SelectedIndices.Count];
            packetList.SelectedIndices.CopyTo(indices, 0);
            for (var i = indices.Length - 1; i >= 0; i--)
            {
                packetList.Items.RemoveAt(indices[i]);
            }
        }

        private void refreshTemplates_Click(object sender, EventArgs e)
        {
            RefreshTemplates();
        }

        private void RefreshTemplates()
        {
            templates.Items.Clear();
            templates.Items.AddRange(TemplateStorage.LoadTemplates().ToArray());
        }
    }
}
