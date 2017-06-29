// Copyright (c) 2017 SteamB23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFly.Graphics
{
    [Flags]
    public enum ClearOptions
    {
        Target = 0b001,
        DepthBuffer = 0b010,
        Stencil = 0b100,
        All = Target | DepthBuffer | Stencil
    }
}
