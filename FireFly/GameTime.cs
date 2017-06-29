// Copyright (c) 2017 SteamB23
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFly
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
