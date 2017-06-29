// Copyright (c) 2017 SteamB23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Windows;

namespace FireFly
{
    public abstract class Game
    {
        RenderForm renderForm = new RenderForm("FireFly Engine");
        Modules.GraphicsBaseModule graphicModule;

        public GameTime GameTime { get; } = new GameTime();
        public ModuleContainer Modules { get; } = new ModuleContainer();

        public IntPtr WindowHandle => renderForm.Handle;
        public Game()
        {
        }
        
        public void Run()
        {
            Initialize();
            GameTime.Start();
            RenderLoop.Run(renderForm, () =>
            {
                GameTime.Update();
                Update();
                Draw();
            });
        }

        protected virtual void Initialize()
        {
            Modules.Add(graphicModule = new Modules.FireFlyGraphicsBaseModule(WindowHandle));
        }

        protected virtual void Update()
        {

        }

        protected virtual void Draw()
        {
            graphicModule.Present();
        }
    }
}
