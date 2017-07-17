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
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector2 : IEquatable<Vector2>, IFormattable
    {
        public static readonly Vector2 Zero = new Vector2();
        public static readonly Vector2 Up = new Vector2(0f, 1f);
        public static readonly Vector2 Down = new Vector2(0f, -1f);
        public static readonly Vector2 Left = new Vector2(-1f, 0f);
        public static readonly Vector2 Right = new Vector2(1f, 0f);
        public static readonly Vector2 One = new Vector2(1f);

        public float X;
        public float Y;

        public Vector2(float value)
        {
            X = Y = value;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public bool IsNormalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return MathHelper.NearEquals(Dot(this, this), 1);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2 Normalize() =>
            Normalize(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Length() =>
            Length(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float LengthSquared() =>
            LengthSquared(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector2 other) =>
            MathHelper.RelativeNearEquals(X, other.X) && MathHelper.RelativeNearEquals(Y, other.Y);

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2))
                return false;

            return Equals((Vector2)obj);
        }

        public override int GetHashCode() => MathHelper.CombineHashCodes(X.GetHashCode(), Y.GetHashCode());

        public override string ToString()
        {
            return ToString(null, CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('<');
            sb.Append(this.X.ToString(format, formatProvider));
            sb.Append(", ");
            sb.Append(this.Y.ToString(format, formatProvider));
            sb.Append('>');
            return sb.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Add(Vector2 value1, Vector2 value2) =>
            new Vector2(value1.X + value2.X, value1.Y + value2.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Add(Vector2 value, float scala) =>
            new Vector2(value.X + scala, value.Y + scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Subtract(Vector2 value1, Vector2 value2) =>
            new Vector2(value1.X - value2.X, value1.Y - value2.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Subtract(Vector2 value1, float value2) =>
            new Vector2(value1.X - value2, value1.Y - value2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Subtract(float value, Vector2 vector) =>
            new Vector2(value - vector.X, value - vector.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Multiply(Vector2 value1, Vector2 value2) =>
            new Vector2(value1.X * value2.X, value1.Y * value2.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Multiply(Vector2 value, float scala) =>
            new Vector2(value.X * scala, value.Y * scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(Vector2 value1, Vector2 value2) =>
            new Vector2(value1.X / value2.X, value1.Y / value1.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(Vector2 value, float scala) =>
            new Vector2(value.X / scala, value.Y / scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(float value, Vector2 vector) =>
            new Vector2(value / vector.X, value / vector.Y);

        public static Vector2 Negate(Vector2 value) =>
            new Vector2(-value.X, -value.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Normalize(Vector2 value)
        {
            var length = Length(value);
            return
                length > MathHelper.ZeroTolerance ? value * (1f / length) :
                value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vector2 value1, Vector2 value2) =>
            value1.X * value2.X + value1.Y * value2.Y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Length(Vector2 value) =>
            (float)Math.Sqrt(LengthSquared(value));

        public static float LengthSquared(Vector2 value) =>
            Dot(value, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector2 value1, Vector2 value2) =>
            Length((value1 - value2));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquared(Vector2 value1, Vector2 value2) =>
            LengthSquared((value1 - value2));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator +(Vector2 left, Vector2 right) => Add(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator +(Vector2 left, float right) => Add(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator +(float left, Vector2 right) => Add(right, left);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(Vector2 left, Vector2 right) => Subtract(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(Vector2 left, float right) => Subtract(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(float left, Vector2 right) => Subtract(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(Vector2 left, Vector2 right) => Multiply(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(Vector2 left, float right) => Multiply(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(float left, Vector2 right) => Multiply(right, left);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(Vector2 left, Vector2 right) => Divide(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(Vector2 left, float right) => Divide(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(float left, Vector2 right) => Divide(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector2 left, Vector2 right) => !left.Equals(right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector2 left, Vector2 right) => left.Equals(right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator +(Vector2 value) => value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(Vector2 value) => Negate(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator ++(Vector2 value) => value + 1f;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator --(Vector2 value) => value - 1f;
    }
}
