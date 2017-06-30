// Copyright (c) 2017 SteamB23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFly
{
    public interface IModule
    {
        /// <summary>
        /// 모듈이 컨테이너에 적재되면 호출됩니다.
        /// </summary>
        void Load();
    }
}
