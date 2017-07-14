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

namespace BandiEngine.Mathmatics
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float value)
        {
            X = value;
            Y = value;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float Length =>
            (float)Math.Sqrt(LengthSquared);

        public float LengthSquared =>
            Dot(this, this);

        public bool IsNormalized =>
            Math.Abs((X * X + Y * Y) - 1f) < MathHelper.ZeroTolerance;

        /// <summary>
        /// 이 인스턴스에 <paramref name="value"/>를 더합니다.
        /// </summary>
        /// <param name="value"></param>
        public void Add(Vector2 value)
        {
            X += value.X;
            Y += value.Y;
        }

        /// <summary>
        /// 이 인스턴스에 <paramref name="value"/>를 더합니다.
        /// </summary>
        /// <param name="value"></param>
        public void Add(float value)
        {
            X += value;
            Y += value;
        }

        /// <summary>
        /// 이 인스턴스에 <paramref name="value"/>를 뺍니다.
        /// </summary>
        /// <param name="value"></param>
        public void Subtract(Vector2 value)
        {
            X -= value.X;
            Y -= value.Y;
        }

        /// <summary>
        /// 이 인스턴스에 <paramref name="value"/>를 뺍니다.
        /// </summary>
        /// <param name="value"></param>
        public void Subtract(float value)
        {
            X -= value;
            Y -= value;
        }

        /// <summary>
        /// 이 인스턴스에 <paramref name="value"/>를 곱합니다.
        /// </summary>
        /// <param name="value"></param>
        public void Multiply(Vector2 value)
        {
            X *= value.X;
            Y *= value.Y;
        }

        /// <summary>
        /// 이 인스턴스에 <paramref name="value"/>를 곱합니다.
        /// </summary>
        /// <param name="value"></param>
        public void Multiply(float value)
        {
            X *= value;
            Y *= value;
        }

        /// <summary>
        /// 이 인스턴스에 <paramref name="value"/>를 나눕니다.
        /// </summary>
        public void Divide(Vector2 value)
        {
            X /= value.X;
            Y /= value.Y;
        }

        public static Vector2 Add(Vector2 left, Vector2 right) =>
            new Vector2(left.X + right.X, left.Y + right.Y);

        public static Vector2 Add(Vector2 left, float right) =>
            new Vector2(left.X + right, left.Y + right);

        public static Vector2 Subtract(Vector2 left, Vector2 right) =>
            new Vector2(left.X - right.X, left.Y + right.Y);

        public static Vector2 Subtract(Vector2 left, float right) =>
            new Vector2(left.X - right, left.Y - right);

        public static Vector2 Subtract(float left, Vector2 right) =>
            new Vector2(left - right.X, left - right.Y);

        public static Vector2 Multiply(Vector2 left, Vector2 right) =>
            new Vector2(left.X * right.X, left.Y * right.Y);

        public static Vector2 Multiply(Vector2 value, float scale) =>
            new Vector2(value.X * scale, value.Y * scale);

        public static float Dot(Vector2 left, Vector2 right) =>
            left.X * right.X + left.Y * right.Y;

        public static Vector2 Divide(Vector2 left, Vector2 right) =>
            new Vector2(left.X / right.X, left.Y / left.Y);

        public static Vector2 Divide(Vector2 value, float divider) =>
            new Vector2(value.X / divider, value.Y / divider);

        public static Vector2 Divide(float value, Vector2 divider) =>
            new Vector2(value / divider.X, value / divider.Y);
    }
}
