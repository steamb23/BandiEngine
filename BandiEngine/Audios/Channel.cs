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

namespace BandiEngine.Audios
{
    public enum Channel
    {
        None = 0,
        Default = 0,
        /// <summary>
        /// Mono audio channel.
        /// </summary>
        Mono = 1,
        /// <summary>
        /// Stereo audio channel.
        /// </summary>
        Stereo = 2,
        /// <summary>
        /// Stereo wtih LFE audio channel.
        /// </summary>
        StereoLFE = 3,
        /// <summary>
        /// Front Rear Stereo audio channel.
        /// </summary>
        Quadraphonic = 4,
        /// <summary>
        /// 5.0 Surround Channel.
        /// </summary>
        Surround5_0 = 5,
        /// <summary>
        /// 5.1 Surround Channel.
        /// </summary>
        Surround5_1 = 6,
        /// <summary>
        /// 6.1 Surround Channel.
        /// </summary>
        Surround6_1 = 7,
        /// <summary>
        /// 7.1 Surround Channel.
        /// </summary>
        Surround7_1 = 8
    }
}
