// Copyright (c) 2017 SteamB23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFly
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class RequiredTypeAttribute : Attribute
    {
        public Type Type { get; }
        public RequiredTypeAttribute(Type type)
        {
            this.Type = type;
        }
    }
}
