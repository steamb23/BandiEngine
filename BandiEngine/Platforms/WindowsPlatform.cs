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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.Windows;

namespace BandiEngine
{
    public sealed class WindowsPlatform : Platform
    {
        RenderForm renderForm;

        public WindowsPlatform(Game game) : base(game)
        {
            renderForm = new RenderForm();
        }

        public WindowsPlatform(Game game, string title) : base(game, title)
        {
            renderForm = new RenderForm();
        }

        public RenderForm RenderForm => renderForm;

        public IntPtr Handle => renderForm.Handle;

        public override string Title { get => renderForm.Text; set => renderForm.Text = value; }

        public override Size Size { get => renderForm.Size; set => renderForm.Size = value; }

        public override void RunLoop()
        {
            RenderLoop.Run(renderForm, () =>
            {
                Game.GameTime.Update();
                Game.Update();
                Game.Draw();
            });
        }

        protected override void Dispose(bool disposing)
        {
            renderForm.Dispose();
            renderForm = null;
        }
    }
}
