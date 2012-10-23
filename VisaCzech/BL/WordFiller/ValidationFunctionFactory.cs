using System;
using System.Collections.Generic;

namespace VisaCzech.BL.WordFiller
{
    public class ValidationFunctionFactory
    {
        private readonly Dictionary<string, Func<object, bool>> _functions;

        public ValidationFunctionFactory()
        {
            _functions = new Dictionary<string, Func<object, bool>>();
        }

        public void RegisterFunction(string funcName, Func<object, bool> func)
        {
            if (_functions.ContainsKey(funcName)) return;
            _functions.Add(funcName, func);
        }

        public Func<object, bool> this[string functionName]
        {
            get
            {
                return _functions.ContainsKey(functionName) ? _functions[functionName] : null;
            }
        }
    }
}
