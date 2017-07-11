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
using SharpDX.Windows;

namespace BandiEngine
{
    public abstract class Game
    {
        public GameTime GameTime { get; } = new GameTime();
        public ModuleContainer Modules { get; } = new ModuleContainer();
        public Platform Platform { get; protected set; }
        public Graphics.Device GraphicsDevice { get; protected set; }

        public Game()
        {
        }
        
        public void Run()
        {
            Initialize();
            GameTime.Start();

            Modules.Find<Platform>().RunLoop();
        }

        public virtual void Initialize()
        {
            this.Platform = new WindowsPlatform(this, "Bandi Engine");
            this.GraphicsDevice = new Graphics.DirectX.Device((WindowsPlatform)Platform, null);

            Modules.Add(Platform);
            Modules.Add(GraphicsDevice);
        }

        public virtual void Update()
        {
            GameTime.Update();
        }

        public virtual void Draw()
        {
            GraphicsDevice.Present();
        }
    }
}
