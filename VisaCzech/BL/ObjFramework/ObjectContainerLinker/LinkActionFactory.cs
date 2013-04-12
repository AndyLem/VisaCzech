using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VisaCzech.BL.ObjFramework.ObjectContainerLinker
{
    public class LinkActionFactory
    {
        private readonly Dictionary<string, Action<object, object>> _actions;

        public LinkActionFactory()
        {
            _actions = new Dictionary<string, Action<object, object>>();
        }

        public void RegisterAction(string actionName, Action<object, object> action)
        {
            if (_actions.ContainsKey(actionName)) return;
            try
            {
                _actions.Add(actionName, action);
            }
            catch
            {
                MessageBox.Show(actionName);
            }
        }

        public Action<object, object> this[string actionName]
        {
            get
            {
                return _actions.ContainsKey(actionName) ? _actions[actionName] : null;
            }
        }
    }
}
