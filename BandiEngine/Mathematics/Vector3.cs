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
    public struct Vector3 : IEquatable<Vector3>
    {
        public static readonly Vector3 Zero = new Vector3();
        public static readonly Vector3 Up = new Vector3(0f, 1f, 0f);
        public static readonly Vector3 Down = new Vector3(0f, -1f, 0f);
        public static readonly Vector3 Left = new Vector3(-1f, 0f, 0f);
        public static readonly Vector3 Right = new Vector3(1f, 0f, 0f);
        public static readonly Vector3 Foward = new Vector3(0f, 0f, 1f);
        public static readonly Vector3 Backward = new Vector3(0f, 0f, -1f);
        public static readonly Vector3 One = new Vector3(1f);

        public float X;
        public float Y;
        public float Z;

        public Vector3(float scala)
        {
            X = Y = Z = scala;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(Vector2 value, float z)
        {
            X = value.X;
            Y = value.Y;
            Z = z;
        }

        public bool IsNormalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => MathHelper.NearEquals(Dot(this, this), 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Length() =>
            Length(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float LengthSquared() =>
            LengthSquared(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3 other) => Equals(this, other);

        public override bool Equals(object obj) =>
            obj is Vector3 ? Equals((Vector3)obj) : false;

        public override int GetHashCode() =>
            MathHelper.CombineHashCodes(MathHelper.CombineHashCodes(X.GetHashCode(), Y.GetHashCode()), Z.GetHashCode());

        public override string ToString() =>
            ToString(null, CultureInfo.CurrentCulture);

        public string ToString(string format) =>
            ToString(format, CultureInfo.CurrentCulture);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('<');
            sb.Append(this.X.ToString(format, formatProvider));
            sb.Append(", ");
            sb.Append(this.Y.ToString(format, formatProvider));
            sb.Append(", ");
            sb.Append(this.Z.ToString(format, formatProvider));
            sb.Append('>');
            return sb.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Add(Vector3 vector1, Vector3 vector2) =>
            new Vector3(
                vector1.X + vector2.X,
                vector1.Y + vector2.Y,
                vector1.Z + vector2.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Add(Vector3 vector, float scala) =>
            new Vector3(
                vector.X + scala,
                vector.Y + scala,
                vector.Z + scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Subtract(Vector3 vector1, Vector3 vector2) =>
            new Vector3(
                vector1.X - vector2.X,
                vector1.Y - vector2.Y,
                vector1.Z - vector2.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Subtract(Vector3 vector, float scala) =>
            new Vector3(
                vector.X - scala,
                vector.Y - scala,
                vector.Z - scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Subtract(float scala, Vector3 vector) =>
            new Vector3(
                scala - vector.X,
                scala - vector.Y,
                scala - vector.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(Vector3 vector1, Vector3 vector2) =>
            new Vector3(
                vector1.X * vector2.X,
                vector1.Y * vector2.Y,
                vector1.Z * vector2.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(Vector3 vector, float scala) =>
            new Vector3(
                vector.X * scala,
                vector.Y * scala,
                vector.Z * scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Divide(Vector3 vector1, Vector3 vector2) =>
            new Vector3(
                vector1.X / vector2.X,
                vector1.Y / vector1.Y,
                vector1.Z / vector2.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Divide(Vector3 vector, float scala) =>
            new Vector3(
                vector.X / scala,
                vector.Y / scala,
                vector.Y / scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Divide(float scala, Vector3 vector) =>
            new Vector3(
                scala / vector.X,
                scala / vector.Y,
                scala / vector.Z);

        public static Vector3 Negate(Vector3 vector) =>
            new Vector3(-vector.X, -vector.Y, -vector.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalize(Vector3 vector)
        {
            var length = Length(vector);
            return
                length > MathHelper.ZeroTolerance ? vector * (1f / length) :
                vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vector3 vector1, Vector3 vector2) =>
                vector1.X * vector2.X +
                vector1.Y * vector2.Y +
                vector1.Z * vector2.Z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(Vector3 vector1, Vector3 vector2) =>
            new Vector3(
                vector1.Y * vector2.Z - vector1.Z * vector2.Y,
                vector1.Z * vector2.X - vector1.X * vector2.Z,
                vector1.X * vector2.Y - vector1.Y * vector2.X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Length(Vector3 vector) =>
            (float)Math.Sqrt(LengthSquared(vector));

        public static float LengthSquared(Vector3 vector) =>
            Dot(vector, vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector3 vector1, Vector3 vector2) =>
            Length(vector1 - vector2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquared(Vector3 vector1, Vector3 vector2) =>
            LengthSquared(vector1 - vector2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Sqrt(Vector3 value) =>
            new Vector3(
                (float)Math.Sqrt(value.X),
                (float)Math.Sqrt(value.Y),
                (float)Math.Sqrt(value.Z));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearEquals(Vector3 vector1, Vector3 vector2) =>
            MathHelper.NearEquals(vector1.X, vector2.X) &&
            MathHelper.NearEquals(vector1.Y, vector2.Y) &&
            MathHelper.NearEquals(vector1.Z, vector2.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RelativeNearEquals(Vector3 vector1, Vector3 vector2) =>
            MathHelper.RelativeNearEquals(vector1.X, vector2.X) &&
            MathHelper.RelativeNearEquals(vector1.Y, vector2.Y) &&
            MathHelper.RelativeNearEquals(vector1.Z, vector2.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Vector3 vector1, Vector3 vector2) =>
            vector1.X == vector2.X &&
            vector1.Y == vector2.Y &&
            vector1.Z == vector2.Z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Greater(Vector3 vector1, Vector3 vector2) =>
            vector1.X > vector2.X &&
            vector1.Y > vector2.Y &&
            vector1.Z > vector2.Z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Less(Vector3 vector1, Vector3 vector2) =>
            vector1.X < vector2.X &&
            vector1.Y < vector2.Y &&
            vector1.Z < vector2.Z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterOrEquals(Vector3 vector1, Vector3 vector2) =>
            vector1.X >= vector2.X &&
            vector1.Y >= vector2.Y &&
            vector1.Z >= vector2.Z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessOrEquals(Vector3 vector1, Vector3 vector2) =>
            vector1.X <= vector2.X &&
            vector1.Y <= vector2.Y &&
            vector1.Z <= vector2.Z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator +(Vector3 left, Vector3 right) => Add(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator +(Vector3 left, float right) => Add(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator +(float left, Vector3 right) => Add(right, left);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator -(Vector3 left, Vector3 right) => Subtract(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator -(Vector3 left, float right) => Subtract(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator -(float left, Vector3 right) => Subtract(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(Vector3 left, Vector3 right) => Multiply(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(Vector3 left, float right) => Multiply(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(float left, Vector3 right) => Multiply(right, left);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator /(Vector3 left, Vector3 right) => Divide(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator /(Vector3 left, float right) => Divide(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator /(float left, Vector3 right) => Divide(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector3 left, Vector3 right) => !Equals(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector3 left, Vector3 right) => Equals(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(Vector3 left, Vector3 right) => Greater(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(Vector3 left, Vector3 right) => Less(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(Vector3 left, Vector3 right) => GreaterOrEquals(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(Vector3 left, Vector3 right) => LessOrEquals(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator +(Vector3 value) => value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator -(Vector3 value) => Negate(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator ++(Vector3 value) => value + 1f;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator --(Vector3 value) => value - 1f;
    }
}
