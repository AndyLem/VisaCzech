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
        private bool ShowImages = false;

        private readonly List<Person> _notFilteredPersons = new List<Person>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _notFilteredPersons.Clear();
            _notFilteredPersons.AddRange(PersonStorage.Instance.LoadAll());
            FillPersonsListBox(_notFilteredPersons);
            packetsList.Items.AddRange(PacketStorage.Instance.LoadAll().ToArray());
            if (packetsList.Items.Count == 0)
            {
                CreateNewPacket();    
            }
            packetsList.SelectedIndex = packetsList.Items.Count - 1;

            //ScAPI.IInstance instance = null;
            //try
            //{
            //    instance = ScAPI.ScProxy.CreateInstance();
            //}
            //catch
            //{}
            //    if (instance != null) MessageBox.Show(instance.GetScanifyAPIVersion());
        }

        private void FillPersonsListBox(IEnumerable<Person> list)
        {
            personsList.Items.Clear(); 
            if (list != null) personsList.Items.AddRange(list.ToArray());
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
                                          AddPersonToLists(newPerson);
                                          PersonStorage.Instance.Save(newPerson);
                                          if (!createToPacket.Checked) return;
                                          if (_currentPacket == null) CreateNewPacket();
                                          if (_currentPacket == null) return;
                                          _currentPacket.AddPerson(newPerson);
                                          currentPacketList.Items.Add(newPerson);
                                          PacketStorage.Instance.Save(_currentPacket);
                                      };
            form.CreatePerson(_currentPacket);
            form.ShowDialog();
        }

        private void AddPersonToLists(Person p)
        {
            _notFilteredPersons.Add(p);
            if (!PersonInFilter(p)) return;
            personsList.Items.Add(p);
            personsList.SelectedItem = p;
        }

        private bool PersonInFilter(Person p)
        {
            var filter = personFilter.Text;
            return string.IsNullOrEmpty(filter) || p.FilterOk(filter);
        }

        private void personsList_DoubleClick(object sender, EventArgs e)
        {
            if (personsList.SelectedItem == null) return;
            var form = new PersonForm();
            var person = personsList.SelectedItem as Person;
            form.InitCombos(_notFilteredPersons);
            form.EditPerson(person);
            if (form.ShowDialog() != DialogResult.OK) return;
            var index = personsList.SelectedIndex;
            personsList.Items.RemoveAt(index);
            if (person != null) personsList.Items.Insert(index, person);
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
            _notFilteredPersons.Remove(person);
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
            if (_currentPacket == null) return;
            var lst = from Person p in personsList.SelectedItems where _currentPacket.IndexOfPerson(p) == -1 select p;
            foreach (var p in lst)
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
                return;
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
            WordFillerOptions options;
            if (PrepareWordFillerOptions(out options)) return;
            var persons = _currentPacket.EnumPersons(personsList.Items);

            // Клонируем массив, чтобы не поиметь проблем с многозадачностью
            persons = Clone(persons);
            WordFiller.FillTemplate(persons, options);
        }

        /// <summary>
        /// Клонирует персонов, чтобы передать в поток заполнения шаблонов
        /// </summary>
        /// <param name="persons"></param>
        /// <returns></returns>
        private static ICollection<Person> Clone(IEnumerable<Person> persons)
        {
            return persons.Select(p => new Person().Merge(p, true)).ToList();
        }

        private bool PrepareWordFillerOptions(out WordFillerOptions options)
        {
            options = new WordFillerOptions();
            if (_currentPacket == null) return true;
            options.PacketName = _currentPacket.Name;
            options.BackgroundProgressBar = this.backgroundProgress;
            options.BackgroundStopButton = this.backgroundStop;
            var form = new WordFillerForm();
            form.Link(options);
            if (form.ShowDialog() != DialogResult.OK) return true;
            options.TemplateName = TemplateStorage.GetFullTemplateName(options.TemplateName);
            return false;
        }

        private void personsList_DrawItemText(object sender, TouchListBox.DrawTextEventArgs e)
        {
            var listBox = sender as ListBox;
            Person p;
            if (listBox == null) return;
            p = (Person) listBox.Items[e.DrawItemArgs.Index];
            if (p == null) return;
            var height = e.DrawItemArgs.Bounds.Height;

            var leftTextPos = e.DrawItemArgs.Bounds.Left;
            if (ShowImages)
            {
                leftTextPos += (int)(height*1.5);
                if (p.Thumbnail != null)
                {
                    e.DrawItemArgs.Graphics.DrawImage(p.Thumbnail, e.DrawItemArgs.Bounds.Left, e.DrawItemArgs.Bounds.Top,
                                                      height,
                                                      height);
                }
            }
            using (var boldFont = new Font("Helvetica", e.DrawItemArgs.Font.Size+1, FontStyle.Bold))
            {
                e.DrawItemArgs.Graphics.DrawString(string.Format("{0} {1}", p.Surname, p.Name), boldFont,
                                                   Brushes.Black, (float) leftTextPos, e.DrawItemArgs.Bounds.Top);
                e.DrawItemArgs.Graphics.DrawString(p.DocumentNumber, e.DrawItemArgs.Font, Brushes.Black,
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
            var range = _currentPacket.EnumPersons(personsList.Items).ToArray();
            if (range.Any())
                currentPacketList.Items.AddRange(range);
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
            if (!EditPacket(packet)) return;
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
            EditPacket(_currentPacket);
            var index = packetsList.SelectedIndex;
            packetsList.Items.RemoveAt(index);
            packetsList.Items.Insert(index, _currentPacket);
            packetsList.SelectedIndex = index;
        }

        private bool EditPacket(Packet packet)
        {
            if (packet == null) throw new ArgumentNullException("packet");
            var form = new PacketForm();
            form.EditPacket(packet);
            if (form.ShowDialog() != DialogResult.OK) return false;
            PacketStorage.Instance.Save(packet);
            return true;
        }

        private void fillSelectedAnketas_Click(object sender, EventArgs e)
        {
            if (_currentPacket == null) return;
            WordFillerOptions options;
            if (PrepareWordFillerOptions(out options)) return;
            var persons = currentPacketList.SelectedItems.Cast<Person>().ToList();
            WordFiller.FillTemplate(persons, options);
        }

        private void personsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var touchListBox = sender as TouchListBox;
            if (touchListBox != null) touchListBox.VisitItem((sender as ListBox).SelectedItem);
        }

        private void personFilter_TextChanged(object sender, EventArgs e)
        {
            var filteredPersons = _notFilteredPersons.Where(PersonInFilter).ToList();
            FillPersonsListBox(filteredPersons);
        }
    }
}
