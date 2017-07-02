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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandiEngine
{
    public class GameTime
    {
        const double DefaultFramePerSecound = 60;
        
        Stopwatch totalTimer = new Stopwatch();
        TimeSpan elapsed;
        TimeSpan total;
        double? fps = null;
        double? timeRatio = null;

        ulong totalFrameCount;

        bool isRunning;

        public TimeSpan Elapsed => elapsed;
        public TimeSpan Total => total;
        public ulong TotalFrameCount => totalFrameCount;

        /// <summary>
        /// 현재 초당 프레임을 가져옵니다.
        /// </summary>
        public double FPS
        {
            get
            {
                if (fps == null)
                    fps = 1 / DeltaTimeD;
                return (double)fps;
            }
        }
        /// <summary>
        /// 이전 프레임의 시간 간격을 가져옵니다. <seealso cref="Elapsed"/>의 <seealso cref="TimeSpan.TotalSeconds"/>프로퍼티와 같은 역할을 합니다.
        /// </summary>
        public double DeltaTimeD => elapsed.TotalSeconds;
        /// <summary>
        /// 보정 값을 가져옵니다. (<seealso cref="DeltaTimeD"/> * <seealso cref="DefaultFramePerSecound"/>)와 같은 역할을 합니다.
        /// </summary>
        public double TimeRatioD
        {
            get
            {
                if (timeRatio == null)
                    timeRatio = DeltaTimeD * DefaultFramePerSecound;
                return (double)timeRatio;
            }
        }
        /// <summary>
        /// 이전 프레임의 시간 간격을 가져옵니다. <seealso cref="DeltaTimeD"/>의 단정밀도 값입니다.
        /// </summary>
        public float DeltaTime => (float)DeltaTimeD;
        /// <summary>
        /// 보정 값을 가져옵니다. <seealso cref="TimeRatioD"/>의 단정밀도 값입니다.
        /// </summary>
        public float TimeRatio => (float)TimeRatioD;
        internal void Start()
        {
            totalTimer.Start();
        }
        internal void Pause()
        {
            totalTimer.Stop();
        }
        internal void Stop()
        {
            totalTimer.Reset();
        }
        public void Update()
        {
            var current = totalTimer.Elapsed;
            elapsed = current - total;
            totalFrameCount++;

            fps = null;
            timeRatio = null;
        }
    }
}
