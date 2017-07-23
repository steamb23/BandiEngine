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
// 
// Original code from SharpDX.Mathematics. https://github.com/sharpdx/SharpDX/
// This code has been modified as needed.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BandiEngine.Mathematics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle : IEquatable<Rectangle>
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public static readonly Rectangle Zero = new Rectangle();

        public Rectangle(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public Rectangle(int x, int y)
        {
            Left = Right = x;
            Top = Bottom = y;
        }

        public Rectangle(int length)
        {
            Left = Top = 0;
            Right = Bottom = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="isExtended">더미 매개 변수입니다.</param>
        public Rectangle(int x, int y, int width, int height, bool isExtended)
        {
            Left = x;
            Top = y;
            Right = x + width;
            Bottom = y + height;
        }

        public int X
        {
            get => Left;
            set
            {
                Left = value;
                Right = Width + value;
            }
        }

        public int Y
        {
            get => Top;
            set
            {
                Top = value;
                Bottom = Width + value;
            }
        }

        public int Width
        {
            get => Right - Left;
            set => Right = Left + value;
        }

        public int Height
        {
            get => Bottom - Top;
            set => Bottom = Top + value;
        }

        public int Area => Width * Height;

        public bool IsZero =>
            (Left == 0) && (Top == 0) && (Right == 0) && (Bottom == 0);

        public void Offset(int x, int y)
        {
            X += x;
            Y += y;
        }

        public void Inflate(int horizontalAmount, int verticalAmount, bool isSafe = true)
        {
            if (isSafe)
            {
                horizontalAmount /= 2;
                verticalAmount /= 2;
            }
            Left -= horizontalAmount;
            Right += horizontalAmount;
            Top -= verticalAmount;
            Bottom += verticalAmount;
        }

        public bool Contains(int x, int y) =>
            (Left <= x) && (x < Right) && (Top <= y) && (y < Bottom);

        public bool Contains(float x, float y) =>
            (Left <= x) && (x < Right) && (Top <= y) && (y < Bottom);

        public bool Contains(Rectangle value) =>
            (Left <= value.Left) && (Right <= value.Right) && (Top <= value.Top) && (Bottom <= value.Bottom);

        public bool Intersects(Rectangle value) =>
            (value.Left < Right) && (value.Right > Left) && (value.Top < Bottom) && (value.Bottom > Top);

        public static Rectangle Intersect(Rectangle value1, Rectangle value2) =>
            new Rectangle(
                Math.Max(value1.Left, value2.Left),
                Math.Max(value1.Top, value1.Top),
                Math.Min(value1.Right, value2.Right),
                Math.Min(value1.Bottom, value2.Bottom));

        public static Rectangle Union(Rectangle value1, Rectangle value2) =>
            new Rectangle(
                Math.Min(value1.Left, value2.Left),
                Math.Min(value1.Top, value1.Top),
                Math.Max(value1.Right, value2.Right),
                Math.Max(value1.Bottom, value2.Bottom));

        public override bool Equals(object obj) =>
            obj is Rectangle ? Equals((Rectangle)obj) : false;

        public bool Equals(Rectangle other) => Equals(this, other);

        public override int GetHashCode() =>
            MathHelper.CombineHashCodes(
                MathHelper.CombineHashCodes(
                    MathHelper.CombineHashCodes(Left, Top), Right), Bottom);

        public static bool Equals(Rectangle value1, Rectangle value2) =>
            value1.Left == value2.Left &&
            value1.Top == value2.Top &&
            value1.Right == value2.Right &&
            value1.Bottom == value2.Bottom;

        public static bool operator ==(Rectangle left, Rectangle right) => Equals(left, right);

        public static bool operator !=(Rectangle left, Rectangle right) => !Equals(left, right);
    }
}
