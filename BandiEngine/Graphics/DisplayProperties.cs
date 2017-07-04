// Copyright (c) 2017 SteamB23
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
    public struct DisplayProperties
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public BufferMode Buffer { get; set; }
        public MultiSampleMode MultiSample { get; set; }
        public bool IsFullscreen { get; set; }
        public VSyncMode VSync { get; set; }
    }

    public enum BufferMode : int
    {
        None = 0,
        SingleBuffer = 0,
        DoubleBuffer = 1,
        TripleBuffer = 2
    }

    public enum MultiSampleMode : int
    {
        None = 1,
        _2x = 2,
        _4x = 4,
        _8x = 8,
        _16x = 16
    }

    public enum VSyncMode : int
    {
        None = 0,
        EveryBlank = 1,
        SecoundsBlank = 2,
        FourthBlank = 4
    }
}
