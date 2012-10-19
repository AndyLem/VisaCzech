using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;

namespace VisaCzech
{
    public partial class Form1 : Form
    {
        private Word._Application app;
        private Word._Document doc;
        private object missingObj = System.Reflection.Missing.Value;
        private object trueObj = true;
        private object falseObj = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            app = new Word.Application();
            object path = @"d:\anketa.dotx";
            try
            {
                doc = app.Documents.Add(ref path, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception)
            {
                doc.Close(ref falseObj);
                app.Quit(ref falseObj);
                doc = null;
                app = null;
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object strToFindObj = textBox1.Text;
            object replaceStrObj = textBox2.Text;
            object replaceTypeObj = Word.WdReplace.wdReplaceAll;

            for (var i = 1; i <= doc.Sections.Count; i++)
            {
                var wordRange = doc.Sections[i].Range;

                var wordFindObj = wordRange.Find;
                var wordFindParameters = new object[15]
                                                  {
                                                      strToFindObj, missingObj, missingObj, missingObj, missingObj,
                                                      missingObj,
                                                      missingObj, missingObj, missingObj, replaceStrObj, replaceTypeObj,
                                                      missingObj, missingObj, missingObj, missingObj
                                                  };

                wordFindObj.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, wordFindObj,
                                                   wordFindParameters);

            }
            doc.Save();
        }
    }
}
