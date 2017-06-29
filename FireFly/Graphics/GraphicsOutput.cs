// Copyright (c) 2017 SteamB23
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
using DXGI = SharpDX.DXGI;
using DXInterop = SharpDX.Mathematics.Interop;

namespace FireFly.Graphics
{
    public class GraphicsOutput : DisposeBase
    {
        DXGI.Output dxgiOutput;
        float gamma = 1f;
        
        internal GraphicsOutput(DXGI.Output dxgiOutput)
        {
            this.dxgiOutput = dxgiOutput;
        }

        public DXGI.OutputDescription Description => dxgiOutput.Description;

        public float Gamma
        {
            set
            {
                gamma = value;
                SetDXGIGammaCurve(value);
            }
            get => gamma;
        }

        public DXGI.ModeDescription FindClosetDisplayMode(GraphicsDevice device, int width, int height)
        {
            DXGI.ModeDescription match = new DXGI.ModeDescription()
            {
                Width = width,
                Height = height,
                Format = DXGI.Format.R8G8B8A8_UNorm
            };
            DXGI.ModeDescription result;
            dxgiOutput.GetClosestMatchingMode(device.D3D_DEVICE, match, out result);
            return result;
        }
        public DXGI.ModeDescription[] GetDisplayModes(DXGI.Format format)
        {
            return dxgiOutput.GetDisplayModeList(format, DXGI.DisplayModeEnumerationFlags.Interlaced | DXGI.DisplayModeEnumerationFlags.Scaling);
        }
        public DXGI.ModeDescription[] DisplayModes => GetDisplayModes(DXGI.Format.R8G8B8A8_UNorm);

        void SetDXGIGammaCurve(float gamma)
        {
            lock (dxgiOutput)
            {
                var gammaCap = dxgiOutput.GammaControlCapabilities;

                DXGI.GammaControl gammaControl = new DXGI.GammaControl()
                {
                    Scale = new DXInterop.RawColor4(1, 1, 1, 1)
                };
                for (var i = 0; i < gammaCap.ControlPointsCount; i++)
                {
                    float gammaPoint = gammaCap.ControlPoints[i];
                    float gammaResult = (float)Math.Pow(gammaPoint, 1 / gamma);

                    gammaControl.GammaCurve[i] = new DXInterop.RawColor4(gammaResult, gammaResult, gammaResult, gammaResult);

                    dxgiOutput.GammaControl = gammaControl;
                }
            }
        }

        #region DirectX Source
        internal DXGI.Output DXGI_OUTPUT => dxgiOutput;
        #endregion

        protected override void Dispose(bool disposing)
        {
            Utilities.Dispose(ref dxgiOutput);
        }
    }
}
