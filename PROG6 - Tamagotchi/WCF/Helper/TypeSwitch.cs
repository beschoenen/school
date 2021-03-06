﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCF.Helper
{
    public class TypeSwitch
    {
        readonly Dictionary<Type, Action<object>> _matches = new Dictionary<Type, Action<object>>();

        public TypeSwitch Case<T>(Action<T> action)
        {
            _matches.Add(typeof(T), (x) => action((T) x));
            return this;
        }

        public void Switch(object x)
        {
            _matches[x.GetType()](x);
        }
    }
}