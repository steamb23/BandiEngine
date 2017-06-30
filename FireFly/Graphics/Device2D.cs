// Copyright (c) 2017 SteamB23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFly.Graphics
{
    /// <summary>
    /// 2D 관련 기능을 제공합니다.
    /// </summary>
    public abstract class Device2D : Device, IModule
    {
        Device device;

        public Device Device => this.device;

        public Device2D(Device device)
        {
            if (device is Device2D)
                throw new ArgumentException(Resources.Device2D_constructor_ArgumentException, "device");
            this.device = device;
        }

        public override void Clear()
        {
            this.device.Clear();
        }

        public override void Present()
        {
            this.device.Present();
        }
    }
}
