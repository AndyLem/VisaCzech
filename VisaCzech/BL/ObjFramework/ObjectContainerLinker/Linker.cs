using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace VisaCzech.BL.ObjFramework.ObjectContainerLinker
{
    public class Linker
    {
        private object _obj;
        private Control _container;
        private readonly Dictionary<object, Tuple<FieldInfo, LinkAttribute>> _links = new Dictionary<object, Tuple<FieldInfo, LinkAttribute>>();
        public readonly LinkActionFactory ActionFactory = new LinkActionFactory();

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
                foreach (var oAttr in custAttibs.OfType<LinkAttribute>())
                {
                    LinkToControl(oAttr, info);
                }
            }
        }

        public void MoveDataToObject()
        {
            foreach (var ctrl in _links.Keys)
            {
                if (ctrl is TextBox) TbxOnTextChanged(ctrl, EventArgs.Empty);
                else if (ctrl is ComboBox) CbbOnTextChanged(ctrl, EventArgs.Empty);
                else if (ctrl is DateTimePicker) DtpOnValueChanged(ctrl, EventArgs.Empty);
                else if (ctrl is CheckBox) ChbCheckedChanged(ctrl, EventArgs.Empty);
            }
        }

        public void MoveDataFromObject()
        {
            foreach (var ctrl in _links.Keys)
            {
                var info = _links[ctrl].Item1;
                var attr = _links[ctrl].Item2;
                string curCtrlValue;
                if (ctrl is TextBox) curCtrlValue = (ctrl as TextBox).Text;
                else if (ctrl is ComboBox) curCtrlValue = (ctrl as ComboBox).Text;
                else curCtrlValue = string.Empty;

                if (attr.InitOnlyEmpty && !string.IsNullOrEmpty(curCtrlValue)) continue;

                var val = info.GetValue(_obj) ?? string.Empty;
                if (ctrl is TextBox) (ctrl as TextBox).Text = val.ToString();
                else if (ctrl is ComboBox)
                {
                    if ((ctrl as ComboBox).DropDownStyle == ComboBoxStyle.DropDownList)
                        (ctrl as ComboBox).SelectedItem = val.ToString();
                    else (ctrl as ComboBox).Text = val.ToString();
                }
                else if (ctrl is DateTimePicker) (ctrl as DateTimePicker).Value = ConvertStrToDateTime(val);
                else if (ctrl is CheckBox) (ctrl as CheckBox).Checked = (bool) val;
            }
        }

        private static DateTime ConvertStrToDateTime(object val)
        {
            var str = val.ToString().Replace('-','.');
            DateTime dt;
            try
            {
                dt = Convert.ToDateTime(str);
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
            return dt;
        }

        public static void FillComboBoxes(Control container, object obj)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (obj == null) throw new ArgumentNullException("obj");

            var fieldInfos = obj.GetType().GetFields();

            foreach (var info in fieldInfos)
            {
                var val = info.GetValue(obj);
                if (val == null) continue;
                var sVal = val.ToString();
                if (string.IsNullOrEmpty(sVal)) continue;

                var custAttibs = info.GetCustomAttributes(true);
                foreach (var oAttr in custAttibs.OfType<LinkAttribute>())
                {
                    if (!oAttr.AllowFillComboBox) continue;
                    var ctrlName = oAttr.ControlName;
                    var ctrl = FindControl(container, ctrlName);
                    if (ctrl == null) continue;
                    if (!(ctrl is ComboBox)) continue;
                    var cbb = (ComboBox) ctrl;
                    if (cbb.Items.IndexOf(sVal) == -1)
                        cbb.Items.Add(sVal);
                }
            }
        }

        private void LinkToControl(LinkAttribute attr, FieldInfo info)
        {
            var ctrlName = attr.ControlName;

            var ctrl = FindControl(_container, ctrlName);
            if (ctrl == null) return;
            var val = info.GetValue(_obj);
            
            if (ctrl is TextBox)
            {
                (ctrl as TextBox).TextChanged += TbxOnTextChanged;
                if (!attr.InitOnlyEmpty || string.IsNullOrEmpty((ctrl as TextBox).Text))
                    if (val != null) (ctrl as TextBox).Text = val.ToString();
            }
            else if (ctrl is DateTimePicker)
            {
                (ctrl as DateTimePicker).ValueChanged += DtpOnValueChanged;
                if (val != null) (ctrl as DateTimePicker).Value = ConvertStrToDateTime(val);
            }
            else if (ctrl is ComboBox)
            {
                (ctrl as ComboBox).TextChanged += CbbOnTextChanged;
                if ((ctrl as ComboBox).DropDownStyle == ComboBoxStyle.DropDown)
                {
                    if (!attr.InitOnlyEmpty || string.IsNullOrEmpty((ctrl as ComboBox).Text))
                        if (val != null) (ctrl as ComboBox).Text = val.ToString();
                }
                else
                {
                    if (val != null)
                        (ctrl as ComboBox).SelectedIndex = (ctrl as ComboBox).Items.IndexOf(val.ToString());
                }
            }
            else if (ctrl is CheckBox)
            {
                (ctrl as CheckBox).CheckedChanged += ChbCheckedChanged;
                if (val != null) (ctrl as CheckBox).Checked = (bool) val;
            }
            else return;
            _links.Add(ctrl, new Tuple<FieldInfo, LinkAttribute>(info, attr));
            DoLinkAction(ctrl, attr);
        }

        private static Control FindControl(Control container, string ctrlName)
        {
            var ctrl = container.Controls[ctrlName];
            return ctrl ??
                (from Control c in container.Controls select FindControl(c, ctrlName)).FirstOrDefault(ct => ct != null);
        }

        private void ChbCheckedChanged(object sender, EventArgs eventArgs)
        {
            if (!_links.ContainsKey(sender)) return;
            var info = _links[sender].Item1;
            var attr = _links[sender].Item2;
            var chBox = sender as CheckBox;
            if (chBox == null) return;
            info.SetValue(_obj, chBox.Checked);
            DoLinkAction(sender, attr);
        }

        private void CbbOnTextChanged(object sender, EventArgs eventArgs)
        {
            if (!_links.ContainsKey(sender)) return;
            var info = _links[sender].Item1;
            var attr = _links[sender].Item2;
            var cbBox = sender as ComboBox;
            if (cbBox == null) return;
            info.SetValue(_obj, cbBox.Text);
            DoLinkAction(sender, attr);
        }

        private void DoLinkAction(object sender, LinkAttribute attr)
        {
            if (string.IsNullOrEmpty(attr.LinkActionName)) return;
            var action = ActionFactory[attr.LinkActionName];
            if (action != null) action(sender, _obj);
        }

        private void DtpOnValueChanged(object sender, EventArgs eventArgs)
        {
            if (!_links.ContainsKey(sender)) return;
            var info = _links[sender].Item1;
            var attr = _links[sender].Item2; 
            var dtp = sender as DateTimePicker;
            if (dtp != null) info.SetValue(_obj, ConvertDateToText(dtp.Value));
            DoLinkAction(sender, attr);
        }

        private static string ConvertDateToText(DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy");
        }
        
        private void TbxOnTextChanged(object sender, EventArgs eventArgs)
        {
            if (!_links.ContainsKey(sender)) return;
            var info = _links[sender].Item1;
            var attr = _links[sender].Item2; 
            var txtBox = sender as TextBox;
            if (txtBox != null) info.SetValue(_obj, txtBox.Text);
            DoLinkAction(sender, attr);
        }
    }
}
