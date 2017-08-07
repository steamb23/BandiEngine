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
using System.Diagnostics;
using SharpDX;
using XAudio2 = SharpDX.XAudio2;

namespace BandiEngine.Audios.DirectX
{
    public class AudioEngine : Audios.AudioEngine
    {
        XAudio2.XAudio2 xAudio2Device;
        XAudio2.MasteringVoice masterVoice;

        public AudioEngine(Channel audioChannel = Channel.Default)
        {
            CreateDevice();
            CreateMasterVoice(audioChannel);
        }

        void CreateDevice()
        {
            Utilities.Dispose(ref xAudio2Device);

            XAudio2.XAudio2Flags flags = RuntimeSymbol.DEBUG ? XAudio2.XAudio2Flags.DebugEngine : XAudio2.XAudio2Flags.None;
            xAudio2Device = new XAudio2.XAudio2(flags, XAudio2.ProcessorSpecifier.DefaultProcessor);
        }

        void CreateMasterVoice(Channel audioChannel)
        {
            Utilities.Dispose(ref xAudio2Device);

            masterVoice = new XAudio2.MasteringVoice(xAudio2Device, (int)audioChannel, XAudio2.XAudio2.DefaultSampleRate);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Utilities.Dispose(ref xAudio2Device);
            Utilities.Dispose(ref xAudio2Device);
        }
    }
}
