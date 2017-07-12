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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Windows;
using D3D = SharpDX.Direct3D;
using D3D11 = SharpDX.Direct3D11;
using D2D1 = SharpDX.Direct2D1;
using DWrite = SharpDX.DirectWrite;
using DXGI = SharpDX.DXGI;

using static SharpDX.Utilities;

namespace BandiEngine.Graphics.DirectX
{
    public sealed class Adapter : Graphics.Adapter
    {
        public static ReadOnlyCollection<Adapter> Adapters
        {
            get
            {
                using (var dxgiFactory = new DXGI.Factory1())
                {
                    var dxgiAdapters = dxgiFactory.Adapters1;
                    var adapters = new Adapter[dxgiAdapters.Length];
                    for (var i = 0; i < dxgiAdapters.Length; i++)
                    {
                        adapters[i] = new Adapter(dxgiAdapters[i]);
                    }
                    return new ReadOnlyCollection<Adapter>(adapters);
                }
            }
        }

        public static Adapter GetAdapter(int index)
        {
            using (var dxgiFactory = new DXGI.Factory1())
            {
                return new Adapter(dxgiFactory.GetAdapter1(index));
            }
        }
        public static int GetAdapterIndex()
        {
            using (var dxgiFactory = new DXGI.Factory1())
            {
                return dxgiFactory.GetAdapterCount1();
            }
        }

        DXGI.Adapter1 dxgiAdapter;

        internal Adapter(DXGI.Adapter1 dxgiAdapter)
        {
            this.dxgiAdapter = dxgiAdapter;
        }

        public override Graphics.AdapterDescription Description => DirectX.AdapterDescription.CreateFrom(dxgiAdapter.Description);

        public override ReadOnlyCollection<Graphics.Output> Outputs
        {
            get
            {
                var dxgiOutputs = dxgiAdapter.Outputs;
                var outputs = new Output[dxgiOutputs.Length];
                for (int i = 0; i< outputs.Length; i++)
                {
                    var dxgiOutput = dxgiOutputs[i];
                    outputs[i] = new Output(dxgiOutput);
                }
                return new ReadOnlyCollection<Graphics.Output>(outputs);
            }
        }
        public Output GetOutput(int index)
        {
            return new Output(dxgiAdapter.GetOutput(index));
        }
        
        public int GetOutputCount()
        {
            return dxgiAdapter.GetOutputCount();
        }

        #region DirectX Instances
        internal DXGI.Adapter1 DXGI_ADAPTER => dxgiAdapter;
        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Utilities.Dispose(ref dxgiAdapter);
        }
    }
}
