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
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// DXGI 1.1을 래핑하는 클래스입니다.
    /// </remarks>
    public class GraphicsAdapter : IDisposable
    {
        #region Static

        public static ReadOnlyCollection<GraphicsAdapter> Adapters
        {
            get
            {
                using (var dxgiFactory = new DXGI.Factory1())
                {
                    var dxgiAdapters = dxgiFactory.Adapters1;
                    var adapters = new GraphicsAdapter[dxgiAdapters.Length];
                    for (var i = 0; i < dxgiAdapters.Length; i++)
                    {
                        adapters[i] = new GraphicsAdapter(dxgiAdapters[i]);
                    }
                    return new ReadOnlyCollection<GraphicsAdapter>(adapters);
                }
            }
        }
        #endregion

        DXGI.Adapter1 dxgiAdapter;

        internal GraphicsAdapter(DXGI.Adapter1 dxgiAdapter)
        {
            this.dxgiAdapter = dxgiAdapter;
        }

        public DXGI.AdapterDescription Description => dxgiAdapter.Description;
        public DXGI.AdapterDescription1 Description1 => dxgiAdapter.Description1;

        /// <summary>
        /// 출력 목록을 가져옵니다.
        /// </summary>
        public ReadOnlyCollection<GraphicsOutput> Outputs
        {
            get
            {
                // 아웃풋 목록 래핑
                var dxgiOutputs = dxgiAdapter.Outputs;
                var outputs = new GraphicsOutput[dxgiOutputs.Length];

                for (var i = 0; i < dxgiOutputs.Length; i++)
                {
                    var dxgiOutput = dxgiOutputs[i];
                    outputs[i] = new GraphicsOutput(dxgiOutput);
                }
                return new ReadOnlyCollection<GraphicsOutput>(outputs);
            }
        }
        public GraphicsOutput GetOutput(int index)
        {
            return new GraphicsOutput(dxgiAdapter.GetOutput(index));
        }
        public int OutputCount => dxgiAdapter.GetOutputCount();

        #region DirectX Source
        internal DXGI.Adapter1 DXGI_ADAPTER => dxgiAdapter;
        #endregion

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
                Utilities.Dispose(ref dxgiAdapter);

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~GraphicsAdapter() {
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
