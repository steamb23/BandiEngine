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
    /// <summary>
    /// 기본 그래픽 및 3D 관련 기능을 제공합니다.
    /// </summary>
    public abstract class Device : IDisposable
    {
        public Device(Platform platform, DisplayProperties displayProperties)
        {
            if (displayProperties == null)
            {
                displayProperties = new DisplayProperties()
                {
                    Width = platform.Size.Width,
                    Height = platform.Size.Height,
                    MultiSample = MultiSampleMode._4x,
                    VSync = VSyncMode.EveryBlank
                };
            }
            this.Platform = platform;
            this.DisplayProperties = displayProperties;
        }
        public Platform Platform { get; protected set; }
        public Adapter Adapter { get; protected set; }
        public DisplayProperties DisplayProperties { get; set; }

        public virtual void Load()
        {
            // Empty method
        }

        public abstract void Clear();
        public abstract void Present();

        #region IDisposable Support
        bool isDisposed;

        public bool IsDisposed => isDisposed;

        private void BaseDispose(bool disposing)
        {
            if (IsDisposed)
            {
                Dispose(disposing);
                GC.SuppressFinalize(this);

                isDisposed = true;
            }
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        public void Dispose()
        {
            BaseDispose(true);
        }

        ~Device()
        {
            BaseDispose(false);
        }
        #endregion
    }
}
