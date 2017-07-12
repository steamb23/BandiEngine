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
    public sealed class Device : Graphics.Device, IDisposable
    {
        static readonly DXGI.Rational _standardRefreshRate = new DXGI.Rational(1, 60);

        WindowsPlatform platform;
        Adapter adapter;

        D3D11.Device d3dDevice;
        D3D11.DeviceContext d3dContext;

        D3D11.RenderTargetView d3dRenderTargetView;
        D3D11.DepthStencilView d3dDepthStencilView;

        DXGI.SwapChain dxgiSwapChain;

        public Device(WindowsPlatform platform, DisplayProperties displayProperties) : base(platform, displayProperties)
        {
            CreateDevice();
            CreateSwapChain(platform);
        }

        public new WindowsPlatform Platform => (WindowsPlatform)base.Platform;

        public override void Clear()
        {
            lock (d3dContext)
            {
                d3dContext.ClearDepthStencilView(d3dDepthStencilView, D3D11.DepthStencilClearFlags.Depth, 1.0f, 0);
                d3dContext.ClearRenderTargetView(d3dRenderTargetView, Color.Orange);
            }
        }

        public override void Present()
        {
            lock (d3dContext)
            {
                dxgiSwapChain.Present((int)DisplayProperties.VSync, DXGI.PresentFlags.None);
            }
        }

        void CreateDevice()
        {
            Utilities.Dispose(ref d3dDevice);
            Utilities.Dispose(ref d3dContext);

            List<D3D.FeatureLevel> featureLevelList = new List<D3D.FeatureLevel>();
            featureLevelList.Add(D3D.FeatureLevel.Level_11_1);
            featureLevelList.Add(D3D.FeatureLevel.Level_11_0);
            featureLevelList.Add(D3D.FeatureLevel.Level_10_1);
            featureLevelList.Add(D3D.FeatureLevel.Level_10_0);

            var creationFlags = D3D11.DeviceCreationFlags.BgraSupport;

            // 디바이스 생성
            d3dDevice = new D3D11.Device(D3D.DriverType.Hardware, creationFlags, featureLevelList.ToArray());
            d3dContext = d3dDevice.ImmediateContext;

            // 어댑터 가져오기
            using (var dxgiDevice = d3dDevice.QueryInterface<DXGI.Device2>())
            using (var dxgiAdapter = dxgiDevice.Adapter)
            {
                adapter = new Adapter(dxgiDevice.Adapter.QueryInterface<DXGI.Adapter1>());
                Adapter = adapter;
            }
        }

        void CreateSwapChain(WindowsPlatform platform)
        {
            Utilities.Dispose(ref dxgiSwapChain);

            var swapChainDesc = new DXGI.SwapChainDescription()
            {
                OutputHandle = platform.Handle,
                Usage = DXGI.Usage.RenderTargetOutput
            };
            // 전체화면시 설정
            if (DisplayProperties.IsFullscreen)
            {
                swapChainDesc.IsWindowed = false;
                swapChainDesc.ModeDescription = new DXGI.ModeDescription(
                    DisplayProperties.Width,
                    DisplayProperties.Height,
                    _standardRefreshRate,
                    DXGI.Format.R8G8B8A8_UNorm);
            }
            // 창모드시 설정
            else
            {
                var platformSize = platform.Size;
                swapChainDesc.IsWindowed = true;
                swapChainDesc.ModeDescription = new DXGI.ModeDescription(
                    platformSize.Width,
                    platformSize.Height,
                    _standardRefreshRate,
                    DXGI.Format.R8G8B8A8_UNorm);
            }
            // 샘플링 여부
            swapChainDesc.SampleDescription = new DXGI.SampleDescription()
            {
                Count = (int)DisplayProperties.MultiSample,
                Quality = DisplayProperties.MultiSample == MultiSampleMode.None ?
                    0 :
                    d3dDevice.CheckMultisampleQualityLevels(DXGI.Format.R8G8B8A8_UNorm, (int)DisplayProperties.MultiSample) - 1
            };
            // 교환 사슬 생성
            using (var dxgiFactory = adapter.DXGI_ADAPTER.GetParent<DXGI.Factory>())
                dxgiSwapChain = new DXGI.SwapChain(dxgiFactory, d3dDevice, swapChainDesc);
        }

        void CreateRenderTarget()
        {
            using (var backBuffer = dxgiSwapChain.GetBackBuffer<D3D11.Texture2D>(0))
            {
                d3dContext.OutputMerger.SetTargets((D3D11.DepthStencilView)null, (D3D11.RenderTargetView)null);

                Utilities.Dispose(ref d3dRenderTargetView);
                Utilities.Dispose(ref d3dDepthStencilView);

                // 백버퍼의 설정을 복사후 수정
                var depthStencilBufferDesc = backBuffer.Description;
                depthStencilBufferDesc.Format = DXGI.Format.D24_UNorm_S8_UInt;
                depthStencilBufferDesc.BindFlags = D3D11.BindFlags.DepthStencil;

                using (var depthStencilBuffer = new D3D11.Texture2D(d3dDevice, depthStencilBufferDesc))
                {
                    d3dRenderTargetView = new D3D11.RenderTargetView(d3dDevice, backBuffer);
                    d3dDepthStencilView = new D3D11.DepthStencilView(d3dDevice, depthStencilBuffer);
                    // DirectX 2D 렌더타겟 설정.
                    //using (var d2dRenderTargetSurface = backBuffer.QueryInterface<DXGI.Surface>())
                    //{
                    //    d2dRenderTarget = new D2D1.RenderTarget(
                    //        d2dFactory,
                    //        d2dRenderTargetSurface,
                    //        new D2D1.RenderTargetProperties(new D2D1.PixelFormat(DXGI.Format.B8G8R8A8_UNorm, D2D1.AlphaMode.Premultiplied)));
                    //}
                }
            }
        }

        #region DirectX Instances
        internal D3D11.Device D3D_DEVICE => d3dDevice;
        internal D3D11.DeviceContext D3D_DEVICE_CONTEXT => d3dContext;
        internal D3D11.RenderTargetView D3D_RENDER_TARGET_VIEW => d3dRenderTargetView;
        internal D3D11.DepthStencilView D3D_DEPTH_STENCIL_VIEW => d3dDepthStencilView;
        internal DXGI.SwapChain DXGI_SWAP_CHAIN => dxgiSwapChain;
        #endregion
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Utilities.Dispose(ref d3dDevice);
            Utilities.Dispose(ref d3dContext);

            Utilities.Dispose(ref d3dRenderTargetView);
            Utilities.Dispose(ref d3dDepthStencilView);

            Utilities.Dispose(ref dxgiSwapChain);
        }
    }
}
