// Copyright (c) 2017 SteamB23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFly.Graphics.DirectX
{
    public sealed class Device2D : Graphics.Device2D, IModule
    {
        Device device;

        public new Device Device => device;

        public Device2D(Device device) : base(device)
        {
            this.device = device;
        }
    }
}
