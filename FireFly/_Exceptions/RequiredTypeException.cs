// Copyright (c) 2017 SteamB23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFly
{
    public class RequiredTypeException : Exception
    {
        public Type[] RequiredTypes { get; }
        public Type TargetType { get; }
        public RequiredTypeException(Type targetType, params Type[] requiredTypes)
        {
            this.RequiredTypes = requiredTypes;
            this.TargetType = targetType;
        }

        public override string Message
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(Resources.RequiredTypeException_Message, TargetType);
                foreach (var requiredType in RequiredTypes)
                {
                    sb.AppendLine();
                    sb.Append(requiredType);
                }
                return sb.ToString();
            }
        }
    }
}
