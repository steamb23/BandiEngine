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

        public Vector2(float scala)
        {
            X = Y = scala;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
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
        public bool Equals(Vector2 other) => Equals(this, other);

        public override bool Equals(object obj) =>
            obj is Vector2 ? Equals((Vector2)obj) : false;

        public override int GetHashCode() =>
            MathHelper.CombineHashCodes(X.GetHashCode(), Y.GetHashCode());

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
            sb.Append('>');
            return sb.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Add(Vector2 vector1, Vector2 vector2) =>
            new Vector2(
                vector1.X + vector2.X,
                vector1.Y + vector2.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Add(Vector2 vector, float scala) =>
            new Vector2(
                vector.X + scala,
                vector.Y + scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Subtract(Vector2 vector1, Vector2 vector2) =>
            new Vector2(
                vector1.X - vector2.X,
                vector1.Y - vector2.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Subtract(Vector2 vector, float scala) =>
            new Vector2(
                vector.X - scala,
                vector.Y - scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Subtract(float scala, Vector2 vector) =>
            new Vector2(
                scala - vector.X,
                scala - vector.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Multiply(Vector2 vector1, Vector2 vector2) =>
            new Vector2(
                vector1.X * vector2.X,
                vector1.Y * vector2.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Multiply(Vector2 vector, float scala) =>
            new Vector2(
                vector.X * scala,
                vector.Y * scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(Vector2 vector1, Vector2 vector2) =>
            new Vector2(
                vector1.X / vector2.X,
                vector1.Y / vector1.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(Vector2 vector, float scala) =>
            new Vector2(
                vector.X / scala,
                vector.Y / scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(float scala, Vector2 vector) =>
            new Vector2(
                scala / vector.X,
                scala / vector.Y);

        public static Vector2 Negate(Vector2 vector) =>
            new Vector2(
                -vector.X,
                -vector.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Normalize(Vector2 vector)
        {
            var length = Length(vector);
            return
                length > MathHelper.ZeroTolerance ? vector * (1f / length) :
                vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vector2 vector1, Vector2 vector2) =>
                vector1.X * vector2.X +
                vector1.Y * vector2.Y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Length(Vector2 vector) =>
            (float)Math.Sqrt(LengthSquared(vector));

        public static float LengthSquared(Vector2 vector) =>
            Dot(vector, vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector2 vector1, Vector2 vector2) =>
            Length(vector1 - vector2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquared(Vector2 vector1, Vector2 vector2) =>
            LengthSquared(vector1 - vector2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2[] Orthogonalize(params Vector2[] source)
        {
            var result = new Vector2[source.Length];
            Orthogonalize(ref result, source);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2[] Orthonormalize(params Vector2[] source)
        {
            var result = new Vector2[source.Length];
            Orthonormalize(ref result, source);
            return result;
        }

        public static void Orthogonalize(ref Vector2[] destination, params Vector2[] source)
        {
            //Uses the modified Gram-Schmidt process.
            //q1 = m1
            //q2 = m2 - ((q1 ⋅ m2) / (q1 ⋅ q1)) * q1
            //q3 = m3 - ((q1 ⋅ m3) / (q1 ⋅ q1)) * q1 - ((q2 ⋅ m3) / (q2 ⋅ q2)) * q2
            //q4 = m4 - ((q1 ⋅ m4) / (q1 ⋅ q1)) * q1 - ((q2 ⋅ m4) / (q2 ⋅ q2)) * q2 - ((q3 ⋅ m4) / (q3 ⋅ q3)) * q3
            //q5 = ...

            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");
            if (destination.Length < source.Length)
                throw new ArgumentOutOfRangeException("destination", "The destination array must be of same length or larger length than the source array.");

            for (int i = 0; i < source.Length; ++i)
            {
                Vector2 newvector = source[i];

                for (int r = 0; r < i; ++r)
                {
                    newvector -= (Dot(destination[r], newvector) / Dot(destination[r], destination[r])) * destination[r];
                }

                destination[i] = newvector;
            }
        }

        public static void Orthonormalize(ref Vector2[] destination, params Vector2[] source)
        {
            //Uses the modified Gram-Schmidt process.
            //Because we are making unit vectors, we can optimize the math for orthogonalization
            //and simplify the projection operation to remove the division.
            //q1 = m1 / |m1|
            //q2 = (m2 - (q1 ⋅ m2) * q1) / |m2 - (q1 ⋅ m2) * q1|
            //q3 = (m3 - (q1 ⋅ m3) * q1 - (q2 ⋅ m3) * q2) / |m3 - (q1 ⋅ m3) * q1 - (q2 ⋅ m3) * q2|
            //q4 = (m4 - (q1 ⋅ m4) * q1 - (q2 ⋅ m4) * q2 - (q3 ⋅ m4) * q3) / |m4 - (q1 ⋅ m4) * q1 - (q2 ⋅ m4) * q2 - (q3 ⋅ m4) * q3|
            //q5 = ...

            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");
            if (destination.Length < source.Length)
                throw new ArgumentOutOfRangeException("destination", "The destination array must be of same length or larger length than the source array.");

            for (int i = 0; i < source.Length; ++i)
            {
                Vector2 newvector = source[i];

                for (int r = 0; r < i; ++r)
                {
                    newvector -= Dot(destination[r], newvector) * destination[r];
                }

                Normalize(newvector);
                destination[i] = newvector;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Reflect(Vector2 incident, Vector2 normal) =>
            incident - 2f * Dot(incident, normal) * normal;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Refract(Vector2 incident, Vector2 normal, float refractionIndex) => Refract(incident, normal, new Vector2(refractionIndex));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Refract(Vector2 incident, Vector2 normal, Vector2 refractionIndex)
        {
            // Powered by DirectXMath XMVector2RefractV
            var iDotN = Dot(incident, normal);
            Vector2 result =
                1f - refractionIndex * refractionIndex * (1f - iDotN * iDotN);
            return result < Zero ? Zero :
                refractionIndex * incident - normal * refractionIndex * (iDotN + Sqrt(result));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Sqrt(Vector2 value) =>
            new Vector2(
                (float)Math.Sqrt(value.X),
                (float)Math.Sqrt(value.Y));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearEquals(Vector2 vector1, Vector2 vector2) =>
            MathHelper.NearEquals(vector1.X, vector2.X) &&
            MathHelper.NearEquals(vector1.Y, vector2.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RelativeNearEquals(Vector2 vector1, Vector2 vector2) =>
            MathHelper.RelativeNearEquals(vector1.X, vector2.X) &&
            MathHelper.RelativeNearEquals(vector1.Y, vector2.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Vector2 vector1, Vector2 vector2) =>
            vector1.X == vector2.X &&
            vector1.Y == vector2.Y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Greater(Vector2 vector1, Vector2 vector2) =>
            vector1.X > vector2.X &&
            vector1.Y > vector2.Y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Less(Vector2 vector1, Vector2 vector2) =>
            vector1.X < vector2.X &&
            vector1.Y < vector2.Y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterOrEquals(Vector2 vector1, Vector2 vector2) =>
            vector1.X >= vector2.X &&
            vector1.Y >= vector2.Y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessOrEquals(Vector2 vector1, Vector2 vector2) =>
            vector1.X <= vector2.X &&
            vector1.Y <= vector2.Y;

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
        public static bool operator !=(Vector2 left, Vector2 right) => !Equals(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector2 left, Vector2 right) => Equals(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(Vector2 left, Vector2 right) => Greater(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(Vector2 left, Vector2 right) => Less(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(Vector2 left, Vector2 right) => GreaterOrEquals(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(Vector2 left, Vector2 right) => LessOrEquals(left, right);
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
