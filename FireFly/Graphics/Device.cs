// Copyright (c) 2017 SteamB23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFly.Graphics
{
    /// <summary>
    /// 기본 그래픽 및 3D 관련 기능을 제공합니다.
    /// </summary>
    public abstract class Device : IModule
    {
        public static Device CreateDefault()
        {
            // DirectX 기반이 기본 계획
            return new DirectX.Device();
        }

        public virtual void Load()
        {
            // Empty method
        }

        public abstract void Clear();
        public abstract void Present();
    }
}
