using System;
using System.Collections.Generic;

namespace VisaCzech.BL.ObjFramework.ObjectContainerLinker
{
    public sealed class LinkActionFactory
    {
        private static LinkActionFactory _instance;
        private readonly Dictionary<string, Action<object, object>> _actions;

        public static LinkActionFactory Instance
        {
            get { return _instance ?? (_instance = new LinkActionFactory()); }
        }

        private LinkActionFactory()
        {
            _actions = new Dictionary<string, Action<object, object>>();
        }

        public void RegisterAction(string actionName, Action<object, object> action)
        {
            if (_actions.ContainsKey(actionName)) return;
            _actions.Add(actionName, action);
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
