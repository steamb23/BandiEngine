﻿// Copyright (c) 2017 SteamB23
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandiEngine.Graphics
{
    /// <summary>
    /// 2D 관련 기능을 제공합니다.
    /// </summary>
    public abstract class Device2D : Device
    {
        Device device;

        public Device Device => this.device;

        public Device2D(Device device) : base(device.Platform, device.DisplayProperties)
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            device.Dispose();
            device = null;
        }
    }
}
