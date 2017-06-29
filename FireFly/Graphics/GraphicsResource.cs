// Copyright (c) 2017 SteamB23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Windows;

namespace FireFly.Graphics
{
    public static class GraphicsResource
    {
        static RenderForm renderForm;
        static GraphicsDevice graphicsDevice;

        static GraphicsResource()
        {
            renderForm = new RenderForm("FireFly Framework Game");
            graphicsDevice = new GraphicsDevice(renderForm);
        }
    }
}
