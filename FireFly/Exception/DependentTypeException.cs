// Copyright (c) 2017 SteamB23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFly
{
    public class DependentTypeException : Exception
    {
        public Type[] DependentTypes { get; }
        public Type TargetType { get; }
        public DependentTypeException(Type targetType, params Type[] dependentTypes)
        {
            this.DependentTypes = dependentTypes;
            this.TargetType = targetType;
        }

        public override string Message
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(Resources.DependentTypeExeption_Message, TargetType);
                foreach (var dependentType in DependentTypes)
                {
                    sb.AppendLine();
                    sb.Append(dependentType);
                }
                return sb.ToString();
            }
        }
    }
}
