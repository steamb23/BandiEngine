// Copyright (c) 2017 SteamB23
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharpDX;
using SharpDX.Windows;
using D3D = SharpDX.Direct3D;
using D3D11 = SharpDX.Direct3D11;
using D2D1 = SharpDX.Direct2D1;
using DW = SharpDX.DirectWrite;
using DXGI = SharpDX.DXGI;

namespace FireFly.Graphics
{
    public class DisplayProperties
    {
        public enum BufferMode : int
        {
            None = 0,
            SingleBuffer = 0,
            DoubleBuffer,
            TripleBuffer
        }
        public enum MultiSampleMode : int
        {
            None = 1,
            _2x = 2,
            _4x = 4,
            _8x = 8,
            _16x = 16
        }
        public enum VSync : int
        {
            None = 0,
            EveryBlank = 1,
            SecoundsBlank = 2,
            FourthBlank = 4
        }
        public int Width { get; set; } = 1280;
        public int Height { get; set; } = 720;
        public bool MultiSampleAntialias { get; set; }
        public BufferMode Buffer { get; set; }
        public MultiSampleMode Sample { get; set; } = MultiSampleMode._4x;
        public bool IsFullscreen { get; set; }
        public VSync VSyncInterval { get; set; }
    }
}
