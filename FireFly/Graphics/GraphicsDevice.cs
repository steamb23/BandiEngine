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

using static FireFly.Interop;

namespace FireFly.Graphics
{
    /*
     * 그래픽 디바이스 생성시 선행되어야할 사항:
     * 어댑터 생성 (화면 정보)
     * RenderForm 생성 (화면 출력)
     */
    public class GraphicsDevice : IDisposable
    {
        // 기본 어댑터
        GraphicsAdapter adapter;

        D3D11.Device d3dDevice;
        D3D11.DeviceContext d3dContext;
        D3D11.RenderTargetView d3dRenderTargetView;
        D3D11.DepthStencilView d3dDepthStencilView;
        ViewportF viewport;

        D2D1.Device1 d2dDevice;
        D2D1.DeviceContext1 d2dContext;
        D2D1.Factory2 d2dFactory;
        D2D1.RenderTarget d2dRenderTarget;

        DXGI.SwapChain dxgiSwapChain;

        DisplayProperties displayProperties = new DisplayProperties();
        public DisplayProperties DisplayProperties => displayProperties;

        public GraphicsDevice(IntPtr windowHandle)
        {
            Create3DRenderDevice();
            Create2DRenderDevice(d3dDevice);
            CreateRenderTargetResource(windowHandle);
        }

        void Create3DRenderDevice()
        {
            Utilities.Dispose(ref d3dDevice);
            Utilities.Dispose(ref d3dContext);

            var creationFlags = D3D11.DeviceCreationFlags.BgraSupport;

            D3D.FeatureLevel[] featureLevels =
            {
                D3D.FeatureLevel.Level_11_1,
                D3D.FeatureLevel.Level_11_0,
                D3D.FeatureLevel.Level_10_1,
                D3D.FeatureLevel.Level_10_0,
                D3D.FeatureLevel.Level_9_3,
                D3D.FeatureLevel.Level_9_2,
                D3D.FeatureLevel.Level_9_1,
            };
            // 디바이스 생성
            d3dDevice = new D3D11.Device(D3D.DriverType.Hardware, creationFlags, featureLevels);

            d3dContext = d3dDevice.ImmediateContext;

            // 어댑터 가져오기
            using (var dxgiDevice = d3dDevice.QueryInterface<DXGI.Device2>())
            using (var dxgiAdapter = (DXGI.Adapter1)dxgiDevice.Adapter)
            {
                adapter = new GraphicsAdapter(dxgiAdapter);
            }
        }

        void Create2DRenderDevice(D3D11.Device d3dDevice)
        {
            d2dFactory = new D2D1.Factory2();

            using (var dxgiDevice = d3dDevice.QueryInterface<DXGI.Device>())
                d2dDevice = new D2D1.Device1(d2dFactory, dxgiDevice);
            d2dContext = new D2D1.DeviceContext1(d2dDevice, D2D1.DeviceContextOptions.None);
        }

        void CreateRenderTargetResource(IntPtr windowHandle, bool isSwapChainReset = false)
        {
            d3dContext.OutputMerger.SetTargets((D3D11.DepthStencilView)null, (D3D11.RenderTargetView)null);

            Utilities.Dispose(ref d3dRenderTargetView);
            Utilities.Dispose(ref d3dDepthStencilView);

            d3dContext.Flush();
            d2dContext.Flush();

            if (isSwapChainReset || windowHandle == IntPtr.Zero)
                Utilities.Dispose(ref dxgiSwapChain);

            #region SwapChain
            // 멀티 샘플링 설정

            // 교환 사슬 초기화
            if (dxgiSwapChain == null)
            {
                // 교환 사슬 생성 준비
                var swapChainDesc = new DXGI.SwapChainDescription()
                {
                    OutputHandle = windowHandle,
                    Usage = DXGI.Usage.RenderTargetOutput
                };
                // 전체화면시 설정
                if (DisplayProperties.IsFullscreen)
                {
                    swapChainDesc.IsWindowed = false;
                    swapChainDesc.ModeDescription = adapter.Outputs[0].FindClosetDisplayMode(this, DisplayProperties.Width, DisplayProperties.Height);
                }
                // 창모드시 설정
                else
                {
                    // 핸들에서 창크기 가져오기
                    GetClientRect(windowHandle, out var clientRect);

                    swapChainDesc.IsWindowed = true;
                    swapChainDesc.ModeDescription = new DXGI.ModeDescription(
                        clientRect.Width,
                        clientRect.Height,
                        new DXGI.Rational(1, 60),
                        DXGI.Format.R8G8B8A8_UNorm);
                }
                // 샘플링 여부
                swapChainDesc.SampleDescription = DisplayProperties.MultiSampleAntialias ?
                    new DXGI.SampleDescription(
                        (int)DisplayProperties.Sample,
                        d3dDevice.CheckMultisampleQualityLevels(DXGI.Format.R8G8B8A8_UNorm, (int)DisplayProperties.Sample) - 1) :
                    new DXGI.SampleDescription(1, 0);

                // 교환 사슬 생성
                using (var dxgiDevice = d3dDevice.QueryInterface<DXGI.Device>())
                using (var dxgiAdapter = dxgiDevice.GetParent<DXGI.Adapter>())
                using (var dxgiFactory = dxgiAdapter.GetParent<DXGI.Factory>())
                {
                    dxgiSwapChain = new DXGI.SwapChain(dxgiFactory, d3dDevice, swapChainDesc);
                }
            }
            // 교환 사슬 리사이즈
            else
            {
                dxgiSwapChain.ResizeBuffers(
                    (int)DisplayProperties.Buffer,
                    DisplayProperties.Width,
                    DisplayProperties.Height,
                    DXGI.Format.R8G8B8A8_UNorm,
                    DXGI.SwapChainFlags.AllowModeSwitch);
            }
            #endregion

            #region RenderTarget & DepthStencil, D2D:RenderTarget
            using (var backBuffer = dxgiSwapChain.GetBackBuffer<D3D11.Texture2D>(0))
            {
                // 백버퍼의 설정을 복사후 수정
                var depthStencilBufferDesc = backBuffer.Description;
                depthStencilBufferDesc.Format = DXGI.Format.D24_UNorm_S8_UInt;
                depthStencilBufferDesc.BindFlags = D3D11.BindFlags.DepthStencil;

                using (var depthStencilBuffer = new D3D11.Texture2D(d3dDevice, depthStencilBufferDesc))
                {
                    d3dRenderTargetView = new D3D11.RenderTargetView(d3dDevice, backBuffer);
                    d3dDepthStencilView = new D3D11.DepthStencilView(d3dDevice, depthStencilBuffer);
                    using (var d2dRenderTargetSurface = backBuffer.QueryInterface<DXGI.Surface>())
                    {
                        d2dRenderTarget = new D2D1.RenderTarget(
                            d2dFactory,
                            d2dRenderTargetSurface,
                            new D2D1.RenderTargetProperties(new D2D1.PixelFormat(DXGI.Format.B8G8R8A8_UNorm, D2D1.AlphaMode.Premultiplied)));
                    }
                }
            }
            #endregion

            // 출력 병합기
            d3dContext.OutputMerger.SetRenderTargets(d3dDepthStencilView, d3dRenderTargetView);
        }

        public void Present()
        {
            lock (d3dContext)
            {
                dxgiSwapChain.Present((int)DisplayProperties.VSyncInterval, DXGI.PresentFlags.None);
            }
        }
        #region DirectX Source
        public D3D11.Device D3D_DEVICE => d3dDevice;
        public D3D11.DeviceContext D3D_CONTEXT => d3dContext;
        public D3D11.RenderTargetView D3D_RENDER_TARGET_VIEW => d3dRenderTargetView;
        public D3D11.DepthStencilView D3D_DEPTH_STENCIL_VIEW => d3dDepthStencilView;

        public D2D1.Device1 D2D_DEVICE => d2dDevice;
        public D2D1.DeviceContext1 D2D_CONTEXT => d2dContext;
        public D2D1.Factory2 D2D_FACTORY => d2dFactory;

        public DXGI.SwapChain DXGI_SWAP_CHAIN => dxgiSwapChain;
        #endregion

        [System.Diagnostics.Conditional("DEBUG")]
        private void SystemInfomation()
        {
            var adapterDesc = adapter.Description;
            System.Diagnostics.Debug.WriteLine("Device Infomation :");
            System.Diagnostics.Debug.Indent();
            System.Diagnostics.Debug.WriteLine($"Device Name : {adapterDesc.Description}");
            System.Diagnostics.Debug.WriteLine($"Dedicate Video Memory : {adapterDesc.DedicatedVideoMemory}");
            System.Diagnostics.Debug.WriteLine($"Dedicate System Memory : {adapterDesc.DedicatedSystemMemory}");
            System.Diagnostics.Debug.WriteLine($"Shared System Memory : {adapterDesc.SharedSystemMemory}");
            System.Diagnostics.Debug.Unindent();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
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
                Utilities.Dispose(ref d3dRenderTargetView);
                Utilities.Dispose(ref d3dDepthStencilView);
                Utilities.Dispose(ref d2dDevice);
                Utilities.Dispose(ref d2dContext);
                Utilities.Dispose(ref d2dFactory);

                Utilities.Dispose(ref dxgiSwapChain);

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~GraphicsDevice() {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
