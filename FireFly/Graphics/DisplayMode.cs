// Copyright (c) 2017 SteamB23
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using D3D = SharpDX.Direct3D;
using D3D11 = SharpDX.Direct3D11;
using DXGI = SharpDX.DXGI;

namespace FireFly.Graphics
{
    public struct DisplayMode
    {
        DXGI.Format format;
        int width;
        int height;
        DXGI.Rational refreshRate;
        public DisplayMode(DXGI.Format format, int width, int height, DXGI.Rational refreshRate)
        {
            this.format = format;
            this.width = width;
            this.height = height;
            this.refreshRate = refreshRate;
        }

        public DXGI.Format Format => format;
        public int Width => width;
        public int Height => height;
        public float AspectRatio
        {
            get
            {
                if (width == 0 || height == 0)
                {
                    return 0f;
                }
                return (float)width / height;
            }
        }
        public DXGI.Rational RefreshRate => refreshRate;
        public float RefreshPerSecound => refreshRate.Numerator / refreshRate.Denominator;

        public static implicit operator DisplayMode(DXGI.ModeDescription dxgiMode)
        {
            return new DisplayMode(dxgiMode.Format, dxgiMode.Width, dxgiMode.Height, dxgiMode.RefreshRate);
        }
        public static explicit operator DXGI.ModeDescription(DisplayMode displayMode)
        {
            return new DXGI.ModeDescription(displayMode.width, displayMode.height, displayMode.refreshRate, displayMode.format);
        }
    }
}
