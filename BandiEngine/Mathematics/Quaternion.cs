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
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BandiEngine.Mathematics
{
    public struct Quaternion : IEquatable<Quaternion>
    {
        public static readonly Quaternion Identity = new Quaternion();

        public float X;
        public float Y;
        public float Z;
        public float W;

        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Quaternion(Vector3 vector, float w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            W = w;
        }

        public Quaternion(Vector2 vector, float z, float w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = z;
            W = w;
        }


        public bool Equals(Quaternion other) => Equals(this, other);

        public override bool Equals(object obj) =>
            obj is Quaternion ? Equals((Quaternion)obj) : false;

        public override int GetHashCode() =>
            MathHelper.CombineHashCodes(
                MathHelper.CombineHashCodes(
                    MathHelper.CombineHashCodes(X.GetHashCode(), Y.GetHashCode()), Z.GetHashCode()), W.GetHashCode());

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
            sb.Append(", ");
            sb.Append(this.W.ToString(format, formatProvider));
            sb.Append('>');
            return sb.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Add(Quaternion quaternion1, Quaternion quaternion2) =>
            new Quaternion(
                quaternion1.X + quaternion2.X,
                quaternion1.Y + quaternion2.Y,
                quaternion1.Z + quaternion2.Z,
                quaternion1.W + quaternion2.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Add(Quaternion quaternion, float scala) =>
            new Quaternion(
                quaternion.X + scala,
                quaternion.Y + scala,
                quaternion.Z + scala,
                quaternion.W + scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Subtract(Quaternion quaternion1, Quaternion quaternion2) =>
            new Quaternion(
                quaternion1.X - quaternion2.X,
                quaternion1.Y - quaternion2.Y,
                quaternion1.Z - quaternion2.Z,
                quaternion1.W - quaternion2.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Subtract(Quaternion quaternion, float scala) =>
            new Quaternion(
                quaternion.X - scala,
                quaternion.Y - scala,
                quaternion.Z - scala,
                quaternion.W - scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Subtract(float scala, Quaternion quaternion) =>
            new Quaternion(
                scala - quaternion.X,
                scala - quaternion.Y,
                scala - quaternion.Z,
                scala - quaternion.W);

        public static Quaternion Multiply(Quaternion quaternion, float scala) =>
            new Quaternion(
                quaternion.X * scala,
                quaternion.Y * scala,
                quaternion.Z * scala,
                quaternion.W * scala);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearEquals(Quaternion quaternion1, Quaternion quaternion2) =>
            MathHelper.NearEquals(quaternion1.X, quaternion2.X) &&
            MathHelper.NearEquals(quaternion1.Y, quaternion2.Y) &&
            MathHelper.NearEquals(quaternion1.Z, quaternion2.Z) &&
            MathHelper.NearEquals(quaternion1.W, quaternion2.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RelativeNearEquals(Quaternion quaternion1, Quaternion quaternion2) =>
            MathHelper.RelativeNearEquals(quaternion1.X, quaternion2.X) &&
            MathHelper.RelativeNearEquals(quaternion1.Y, quaternion2.Y) &&
            MathHelper.RelativeNearEquals(quaternion1.Z, quaternion2.Z) &&
            MathHelper.RelativeNearEquals(quaternion1.W, quaternion2.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Quaternion quaternion1, Quaternion quaternion2) =>
            quaternion1.X == quaternion2.X &&
            quaternion1.Y == quaternion2.Y &&
            quaternion1.Z == quaternion2.Z &&
            quaternion1.W == quaternion2.W;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Greater(Quaternion quaternion1, Quaternion quaternion2) =>
            quaternion1.X > quaternion2.X &&
            quaternion1.Y > quaternion2.Y &&
            quaternion1.Z > quaternion2.Z &&
            quaternion1.W > quaternion2.W;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Less(Quaternion quaternion1, Quaternion quaternion2) =>
            quaternion1.X < quaternion2.X &&
            quaternion1.Y < quaternion2.Y &&
            quaternion1.Z < quaternion2.Z &&
            quaternion1.W < quaternion2.W;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterOrEquals(Quaternion quaternion1, Quaternion quaternion2) =>
            quaternion1.X >= quaternion2.X &&
            quaternion1.Y >= quaternion2.Y &&
            quaternion1.Z >= quaternion2.Z &&
            quaternion1.W >= quaternion2.W;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessOrEquals(Quaternion quaternion1, Quaternion quaternion2) =>
            quaternion1.X <= quaternion2.X &&
            quaternion1.Y <= quaternion2.Y &&
            quaternion1.Z <= quaternion2.Z &&
            quaternion1.W <= quaternion2.W;
    }
}
