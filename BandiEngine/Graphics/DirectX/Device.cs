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

        DXGI.SwapChain dxgiSwapChain;

        public Device(WindowsPlatform platform, DisplayProperties displayProperties) : base(platform, displayProperties)
        {
            CreateDevice();
            CreateSwapChain(platform);
        }

        public new WindowsPlatform Platform => (WindowsPlatform)base.Platform;

        public override void Clear()
        {
        }

        public override void Present()
        {

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
                swapChainDesc.ModeDescription = new DXGI.ModeDescription(DisplayProperties.Width, DisplayProperties.Height, _standardRefreshRate, DXGI.Format.R8G8B8A8_UNorm);
            }
            // 창모드시 설정
            else
            {
                var platformSize = platform.Size;
                swapChainDesc.IsWindowed = true;
                swapChainDesc.ModeDescription = new DXGI.ModeDescription(
                    platformSize.Width,
                    platformSize.Height,
                    new DXGI.Rational(1, 60),
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

        }

        #region DirectX Instances
        internal D3D11.Device D3D_DEVICE => d3dDevice;
        internal D3D11.DeviceContext D3D_DEVICE_CONTEXT => d3dContext;
        internal DXGI.SwapChain DXGI_SWAP_CHAIN => dxgiSwapChain;
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }
                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                Utilities.Dispose(ref d3dDevice);
                Utilities.Dispose(ref d3dContext);
                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        ~Device()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(false);
        }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
