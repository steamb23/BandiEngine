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
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BandiEngine.Mathematics
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector2
    {
        public static readonly Vector2 Zero = new Vector2();
        public static readonly Vector2 Up = new Vector2(0, 1);
        public static readonly Vector2 Down = new Vector2(0, -1);
        public static readonly Vector2 Left = new Vector2(-1, 0);
        public static readonly Vector2 Right = new Vector2(1, 0);
        public static readonly Vector2 One = new Vector2(1);

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

        public Vector2 Negative => Negate(this);

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
        /// 이 인스턴스와 <paramref name="value"/>를 사용해 내적을 구합니다.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public float Dot(Vector2 value)
        {
            return Dot(this, value);
        }

        /// <summary>
        /// 이 인스턴스에 <paramref name="value"/>를 나눕니다.
        /// </summary>
        public void Divide(Vector2 value)
        {
            X /= value.X;
            Y /= value.Y;
        }

        /// <summary>
        /// 이 인스턴스의 값을 부정합니다.
        /// </summary>
        public void Negate()
        {
            X = -X;
            Y = -Y;
        }

        public static Vector2 Add(Vector2 value1, Vector2 value2) =>
            new Vector2(value1.X + value2.X, value1.Y + value2.Y);

        public static Vector2 Add(Vector2 value1, float value2) =>
            new Vector2(value1.X + value2, value1.Y + value2);

        public static Vector2 Subtract(Vector2 value1, Vector2 value2) =>
            new Vector2(value1.X - value2.X, value1.Y + value2.Y);

        public static Vector2 Subtract(Vector2 value1, float value2) =>
            new Vector2(value1.X - value2, value1.Y - value2);

        public static Vector2 Subtract(float value1, Vector2 value2) =>
            new Vector2(value1 - value2.X, value1 - value2.Y);

        public static Vector2 Multiply(Vector2 value1, Vector2 value2) =>
            new Vector2(value1.X * value2.X, value1.Y * value2.Y);

        public static Vector2 Multiply(Vector2 value, float scale) =>
            new Vector2(value.X * scale, value.Y * scale);

        public static float Dot(Vector2 value1, Vector2 value2) =>
            value1.X * value2.X + value1.Y * value2.Y;

        public static Vector2 Divide(Vector2 value1, Vector2 value2) =>
            new Vector2(value1.X / value2.X, value1.Y / value1.Y);

        public static Vector2 Divide(Vector2 value, float divider) =>
            new Vector2(value.X / divider, value.Y / divider);

        public static Vector2 Divide(float value, Vector2 divider) =>
            new Vector2(value / divider.X, value / divider.Y);

        public static Vector2 Negate(Vector2 value) =>
            new Vector2(-value.X, -value.Y);
        public static float Distance(Vector2 value1, Vector2 value2) =>
            (value1 - value2).Length;
        public static float DistanceSquered(Vector2 value1, Vector2 value2) =>
            (value1 - value2).LengthSquared;

        public static Vector2 operator +(Vector2 right, Vector2 left) => Add(right, left);
        public static Vector2 operator +(Vector2 right, float left) => Add(right, left);
        public static Vector2 operator +(float right, Vector2 left) => Add(left, right);
        public static Vector2 operator -(Vector2 right, Vector2 left) => Subtract(right, left);
        public static Vector2 operator -(Vector2 right, float left) => Subtract(right, left);
        public static Vector2 operator -(float right, Vector2 left) => Subtract(right, left);
        public static Vector2 operator *(Vector2 right, Vector2 left) => Multiply(right, left);
        public static Vector2 operator *(Vector2 right, float left) => Multiply(right, left);
        public static Vector2 operator *(float right, Vector2 left) => Multiply(left, right);
        public static Vector2 operator /(Vector2 right, Vector2 left) => Divide(right, left);
        public static Vector2 operator /(Vector2 right, float left) => Divide(right, left);
        public static Vector2 operator /(float right, Vector2 left) => Divide(right, left);
    }
}
