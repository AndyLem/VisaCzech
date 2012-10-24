using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VisaCzech.BL.WordFiller;
using VisaCzech.Properties;
using VisaCzech.DL;
using VisaCzech.BL;

namespace VisaCzech.UI
{
    public partial class MainForm : Form
    {
        private Packet _currentPacket;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            personsList.Items.AddRange(PersonStorage.Instance.LoadAll().ToArray());
            packetsList.Items.AddRange(PacketStorage.Instance.LoadAll().ToArray());
            if (packetsList.Items.Count == 0)
            {
                CreateNewPacket();    
            }
            packetsList.SelectedIndex = packetsList.Items.Count - 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //foreach (Person person in personsList.Items)
            //    PersonStorage.SavePerson(person);
        }

        private void createPerson_Click(object sender, EventArgs e)
        {
            var form = new PersonForm();
            var allPersons = personsList.Items.Cast<Person>().ToList();
            form.InitCombos(allPersons);
            form.PersonCreated += (o, args) =>
                                      {
                                          var newPerson = new Person();
                                          newPerson.Merge(o as Person);
                                          personsList.Items.Add(newPerson);
                                          PersonStorage.Instance.Save(newPerson);
                                      };
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
            PersonStorage.Instance.Delete(person);
        }

        private void newPacket_Click(object sender, EventArgs e)
        {
            CreateNewPacket();
            
        }

        private void addToPacket_Click(object sender, EventArgs e)
        {
            if (_currentPacket == null)
                CreateNewPacket();
            if (_currentPacket == null) throw new Exception("Новый пакет не создался");
            foreach (var p in from Person p in personsList.SelectedItems where _currentPacket.IndexOfPerson(p) == -1 select p)
            {
                _currentPacket.AddPerson(p);
                currentPacketList.Items.Add(p);
            }
            PacketStorage.Instance.Save(_currentPacket);
        }

        private void removeFromPacket_Click(object sender, EventArgs e)
        {
            if (_currentPacket == null)
                CreateNewPacket();
            if (_currentPacket == null)
                throw new Exception("Новый пакет не создался");
            var indices = new int[currentPacketList.SelectedIndices.Count];
            currentPacketList.SelectedIndices.CopyTo(indices, 0);
            for (var i = indices.Length - 1; i >= 0; i--)
            {
                _currentPacket.Persons.RemoveAt(indices[i]);
                currentPacketList.Items.RemoveAt(indices[i]);
            }
            PacketStorage.Instance.Save(_currentPacket);
        }
        
        private void fillAnketas_Click(object sender, EventArgs e)
        {
            if (_currentPacket == null) return;
            var form = new WordFillerForm();
            var options = new WordFillerOptions {PacketName = _currentPacket.Name};
            form.Link(options);
            if (form.ShowDialog() != DialogResult.OK) return;
            options.TemplateName = TemplateStorage.GetFullTemplateName(options.TemplateName);
            var persons = _currentPacket.EnumPersons(personsList.Items);

            WordFiller.FillTemplate(persons, options);
        }

        private void personsList_DrawItemText(object sender, TouchListBox.DrawTextEventArgs e)
        {
            var listBox = sender as ListBox;
            Person p;
            if (listBox == null) return;
            p = (Person) listBox.Items[e.DrawItemArgs.Index];
            if (p == null) return;
            var height = e.DrawItemArgs.Bounds.Height;
            var leftTextPos = e.DrawItemArgs.Bounds.Left;// +height * 1.5;
            //if (p.Image != null)
            //{
            //    e.DrawItemArgs.Graphics.DrawImage(p.Image, e.DrawItemArgs.Bounds.Left, e.DrawItemArgs.Bounds.Top, height,
            //                                      height);
            //}
            using (var boldFont = new Font("Helvetica", e.DrawItemArgs.Font.Size+1, FontStyle.Bold))
            {
                e.DrawItemArgs.Graphics.DrawString(string.Format("{0} {1}", p.Surname, p.Name), boldFont,
                                                   Brushes.Black, (float) leftTextPos, e.DrawItemArgs.Bounds.Top);
                e.DrawItemArgs.Graphics.DrawString(p.PersonalId, e.DrawItemArgs.Font, Brushes.Black,
                                                   (float)leftTextPos, e.DrawItemArgs.Bounds.Top+ height / 2);

                if ((sender as Control).Name != "personsList") return;
                var dateOfCreation = p.DateOfCreation.ToShortDateString();
                var measure = e.DrawItemArgs.Graphics.MeasureString(dateOfCreation, boldFont);
                e.DrawItemArgs.Graphics.DrawString(dateOfCreation, boldFont, Brushes.Black,
                                                   e.DrawItemArgs.Bounds.Right - measure.Width,
                                                   (e.DrawItemArgs.Bounds.Top + measure.Height/2));
            }
        }

        private void packetsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentPacket = packetsList.SelectedItem as Packet;
            currentPacketList.Items.Clear();
            if (_currentPacket == null)
            {
                CreateNewPacket();
                return;
            }
            currentPacketList.Items.AddRange(_currentPacket.EnumPersons(personsList.Items).ToArray());
        }

        private void CreateNewPacket()
        {
            var newPacketNameTemplate = "Пакет №";
            var n = 1;
            string newPacketName;
            do
            {
                newPacketName = newPacketNameTemplate + n;
                n++;
            } while (PacketStorage.Instance.PacketExists(newPacketName));
            var packet = new Packet(newPacketName);
            PacketStorage.Instance.Save(packet);
            packetsList.Items.Add(packet);
            packetsList.SelectedItem = packet;
        }

        private void deletePacket_Click(object sender, EventArgs e)
        {
            if (_currentPacket == null) return;
            if (MessageBox.Show(string.Format("Удалить пакет {0}?", _currentPacket.Name), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            if (!PacketStorage.Instance.Delete(_currentPacket)) return;
            packetsList.Items.Remove(_currentPacket);
            if (packetsList.Items.Count == 0)
                CreateNewPacket();
            packetsList.SelectedIndex = packetsList.Items.Count - 1;
        }

        private void renamePacket_Click(object sender, EventArgs e)
        {
            if (_currentPacket == null) return;
            var form = new PacketForm();
            form.EditPacket(_currentPacket);
            if (form.ShowDialog() != DialogResult.OK) return;
            PacketStorage.Instance.Save(_currentPacket);
            var index = packetsList.SelectedIndex;
            packetsList.Items.RemoveAt(index);
            packetsList.Items.Insert(index, _currentPacket);
            packetsList.SelectedIndex = index;
        }
    }
}
