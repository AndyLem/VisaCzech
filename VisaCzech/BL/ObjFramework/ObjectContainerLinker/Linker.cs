using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace VisaCzech.BL.ObjFramework.ObjectContainerLinker
{
    public class Linker
    {
        private object _obj;
        private Control _container;
        private readonly Dictionary<object, Tuple<FieldInfo, object>> _links = new Dictionary<object, Tuple<FieldInfo, object>>();
        

        public void LinkObjectToControl(Control container, object obj)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (obj == null) throw new ArgumentNullException("obj");

            _container = container;
            _obj = obj;
            var fieldInfos = obj.GetType().GetFields();

            foreach (var info in fieldInfos)
            {
                var custAttibs = info.GetCustomAttributes(true);
                foreach (var oAttr in custAttibs)
                {
                    if (oAttr is TextBoxLinkAttribute)
                    {
                        LinkToTextBox(oAttr as TextBoxLinkAttribute, info, obj);
                    }
                    else
                    {
                        
                    }
                }
            }
        }

        private void LinkToTextBox(TextBoxLinkAttribute oAttr, FieldInfo info, object obj)
        {
            var tbxName = oAttr.TextBoxName;
            try
            {
                var ctrl = _container.Controls[tbxName];
                if (ctrl == null) return;
                if (!(ctrl is TextBox)) return;
                var tbx = ctrl as TextBox;
                tbx.TextChanged += TbxOnTextChanged;
                _links.Add(tbx, new Tuple<FieldInfo, object>(info, obj));
            }
            catch (Exception e)
            {
                
            }
        }

        private void TbxOnTextChanged(object sender, EventArgs eventArgs)
        {
            if (!_links.ContainsKey(sender)) return;
            var data = _links[sender];
            var info = data.Item1;
            var obj = data.Item2;
            var txtBox = sender as TextBox;
            if (txtBox != null) info.SetValue(obj, txtBox.Text);
        }
    }
}
