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

namespace BandiEngine.Mathematics
{
    public struct Matrix
    {
        public static readonly Matrix Zero = new Matrix();
        public static readonly Matrix Identity = new Matrix()
        {
            M11 = 1f,
            M22 = 1f,
            M33 = 1f,
            M44 = 1f
        };
        #region Matrix primitive type
        public float M11;
        public float M12;
        public float M13;
        public float M14;
        public float M21;
        public float M22;
        public float M23;
        public float M24;
        public float M31;
        public float M32;
        public float M33;
        public float M34;
        public float M41;
        public float M42;
        public float M43;
        public float M44;
        #endregion

        public Vector3 Up
        {
            get => new Vector3(M21, M22, M23);
            set
            {
                M21 = value.X;
                M22 = value.Y;
                M23 = value.Z;
            }
        }

        public Vector3 Down
        {
            get => new Vector3(-M21, -M22, -M23);
            set
            {
                M21 = value.X;
                M22 = value.Y;
                M23 = value.Z;
            }
        }

        public Vector3 Right
        {
            get => new Vector3(M11, M12, M13);
            set
            {
                M11 = value.X;
                M12 = value.Y;
                M13 = value.Z;
            }
        }

        public Vector3 Left
        {
            get => new Vector3(-M11, -M12, -M13);
            set
            {
                M11 = -value.X;
                M12 = -value.Y;
                M13 = -value.Z;
            }
        }

        public Vector3 Forward
        {
            get => new Vector3(-M31, -M32, -M33);
            set
            {
                M31 = -value.X;
                M32 = -value.Y;
                M33 = -value.Z;
            }
        }

        public Vector3 Backward
        {
            get => new Vector3(M31, M32, M33);
            set
            {
                M31 = value.X;
                M32 = value.Y;
                M33 = value.Z;
            }
        }

        public Matrix(float value)
        {
            M11 = M12 = M13 = M14 =
            M21 = M22 = M23 = M24 =
            M31 = M32 = M33 = M34 =
            M41 = M42 = M43 = M44 = value;
        }
        public Matrix(
            float M11, float M12, float M13, float M14,
            float M21, float M22, float M23, float M24,
            float M31, float M32, float M33, float M34,
            float M41, float M42, float M43, float M44)
        {
            this.M11 = M11; this.M12 = M12; this.M13 = M13; this.M14 = M14;
            this.M21 = M21; this.M22 = M22; this.M23 = M23; this.M24 = M24;
            this.M31 = M31; this.M32 = M32; this.M33 = M33; this.M34 = M34;
            this.M41 = M41; this.M42 = M42; this.M43 = M43; this.M44 = M44;
        }

        public Matrix(float[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values.Length < 16)
                throw new ArgumentOutOfRangeException(nameof(values), "행렬에는 16개 이상의 입력 값이 필요합니다.");

            M11 = values[0];
            M12 = values[1];
            M13 = values[2];
            M14 = values[3];

            M21 = values[4];
            M22 = values[5];
            M23 = values[6];
            M24 = values[7];

            M31 = values[8];
            M32 = values[9];
            M33 = values[10];
            M34 = values[11];

            M41 = values[12];
            M42 = values[13];
            M43 = values[14];
            M44 = values[15];
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return M11;
                    case 1: return M12;
                    case 2: return M13;
                    case 3: return M14;
                    case 4: return M21;
                    case 5: return M22;
                    case 6: return M23;
                    case 7: return M24;
                    case 8: return M31;
                    case 9: return M32;
                    case 10: return M33;
                    case 11: return M34;
                    case 12: return M41;
                    case 13: return M42;
                    case 14: return M43;
                    case 15: return M44;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Matrix run from 0 to 15, inclusive.");
            }
            set
            {
                switch (index)
                {
                    case 0: M11 = value; break;
                    case 1: M12 = value; break;
                    case 2: M13 = value; break;
                    case 3: M14 = value; break;
                    case 4: M21 = value; break;
                    case 5: M22 = value; break;
                    case 6: M23 = value; break;
                    case 7: M24 = value; break;
                    case 8: M31 = value; break;
                    case 9: M32 = value; break;
                    case 10: M33 = value; break;
                    case 11: M34 = value; break;
                    case 12: M41 = value; break;
                    case 13: M42 = value; break;
                    case 14: M43 = value; break;
                    case 15: M44 = value; break;
                    default: throw new ArgumentOutOfRangeException("index", "Indices for Matrix run from 0 to 15, inclusive.");
                }
            }
        }

        public Vector4 Row1
        {
            get => new Vector4(M11, M12, M13, M14);
            set
            {
                M11 = value.X;
                M12 = value.Y;
                M13 = value.Z;
                M14 = value.W;
            }
        }

        public Vector4 Row2
        {
            get => new Vector4(M21, M22, M23, M24);
            set
            {
                M21 = value.X;
                M22 = value.Y;
                M23 = value.Z;
                M24 = value.W;
            }
        }

        public Vector4 Row3
        {
            get => new Vector4(M31, M32, M33, M34);
            set
            {
                M31 = value.X;
                M32 = value.Y;
                M33 = value.Z;
                M34 = value.W;
            }
        }

        public Vector4 Row4
        {
            get => new Vector4(M41, M42, M43, M44);
            set
            {
                M41 = value.X;
                M42 = value.Y;
                M43 = value.Z;
                M44 = value.W;
            }
        }

        public Vector4 Column1
        {
            get => new Vector4(M11, M21, M31, M41);
            set
            {
                M11 = value.X;
                M21 = value.Y;
                M31 = value.Z;
                M41 = value.W;
            }
        }

        public Vector4 Column2
        {
            get => new Vector4(M12, M22, M32, M42);
            set
            {
                M12 = value.X;
                M22 = value.Y;
                M32 = value.Z;
                M42 = value.W;
            }
        }

        public Vector4 Column3
        {
            get => new Vector4(M13, M23, M33, M43);
            set
            {
                M13 = value.X;
                M23 = value.Y;
                M33 = value.Z;
                M43 = value.W;
            }
        }

        public Vector4 Column4
        {
            get => new Vector4(M14, M24, M34, M44);
            set
            {
                M14 = value.X;
                M24 = value.Y;
                M34 = value.Z;
                M44 = value.W;
            }
        }

        public Vector3 Translation
        {
            get => new Vector3(M41, M42, M43);
            set
            {
                M41 = value.X;
                M42 = value.Y;
                M43 = value.Z;
            }
        }

        public Vector3 Scale
        {
            get => new Vector3(M11, M22, M33);
            set
            {
                M11 = value.X;
                M22 = value.Y;
                M33 = value.Z;
            }
        }

        public static Matrix CreateTranslation(float x, float y, float z) =>
            new Matrix(
                1f, 0f, 0f, 0f,
                0f, 1f, 0f, 0f,
                0f, 0f, 1f, 0f,
                x, y, z, 1f);

        public static Matrix CreateTranslation(Vector3 position) =>
            CreateTranslation(position.X, position.Y, position.Z);

        public static Matrix CreateScale(float x, float y, float z) =>
            new Matrix(
                x, 0f, 0f, 0f,
                0f, y, 0f, 0f,
                0f, 0f, z, 0f,
                0f, 0f, 0f, 1f);

        public static Matrix CreateScale(float x, float y, float z, float centerX, float centerY, float centerZ) =>
            new Matrix(
                x, 0f, 0f, 0f,
                0f, y, 0f, 0f,
                0f, 0f, z, 0f,
                centerX * (1 - x), centerY * (1 - y), centerZ * (1 - z), 1f);

        public static Matrix CreateScale(Vector3 scale) =>
            CreateScale(scale.X, scale.Y, scale.Z);

        public static Matrix CreateScale(Vector3 scale, Vector3 center) =>
            CreateScale(scale.X, scale.Y, scale.Z, center.X, center.Y, center.Z);

        public static Matrix CreateScale(float scale) =>
            CreateScale(scale, scale, scale);

        public static Matrix CreateRotationX(float radians)
        {
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);

            return new Matrix(
                1f, 0f, 0f, 0f,
                0f, cos, sin, 0f,
                0f, -sin, cos, 0f,
                0f, 0f, 0f, 1f);
        }

        public static Matrix CreateRotationX(float radians, float centerY, float centerZ)
        {
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);

            float y = centerY * (1 - cos) + centerZ * sin;
            float z = centerZ * (1 - cos) - centerY * sin;

            return new Matrix(
                1f, 0f, 0f, 0f,
                0f, cos, sin, 0f,
                0f, -sin, cos, 0f,
                0f, y, z, 1f);
        }

        public static Matrix CreateRotationX(float radians, Vector3 centerPoint) =>
            CreateRotationX(radians, centerPoint.Y, centerPoint.Z);

        public static Matrix CreateRotationX(float radians, Vector2 centerPoint) =>
            CreateRotationX(radians, centerPoint.X, centerPoint.Y);

        public static Matrix CreateRotationY(float radians)
        {
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);

            return new Matrix(
                cos, 0f, -sin, 0f,
                0f, 1f, 0f, 0f,
                sin, 0f, cos, 0f,
                0f, 0f, 0f, 1f);
        }

        public static Matrix CreateRotationY(float radians, float centerX, float centerZ)
        {
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);

            float x = centerX * (1 - cos) + centerZ * sin;
            float z = centerZ * (1 - cos) - centerX * sin;

            return new Matrix(
                cos, 0f, -sin, 0f,
                0f, 1f, 0f, 0f,
                sin, 0f, cos, 0f,
                x, 0f, z, 1f);
        }

        public static Matrix CreateRotationY(float radians, Vector3 centerPoint) =>
            CreateRotationY(radians, centerPoint.X, centerPoint.Z);

        public static Matrix CreateRotationY(float radians, Vector2 centerPoint) =>
            CreateRotationY(radians, centerPoint.X, centerPoint.Y);

        public static Matrix CreateRotationZ(float radians)
        {
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);

            return new Matrix(
                cos, sin, 0f, 0f,
                -sin, 1f, 0f, 0f,
                0f, 0f, 1f, 0f,
                0f, 0f, 0f, 1f);
        }

        public static Matrix CreateRotationZ(float radians, float centerX, float centerY)
        {
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);

            float x = centerX * (1 - cos) + centerY * sin;
            float y = centerY * (1 - cos) - centerX * sin;

            return new Matrix(
                cos, sin, 0f, 0f,
                -sin, 1f, 0f, 0f,
                0f, 0f, 1f, 0f,
                x, y, 0f, 1f);
        }

        public static Matrix CreateRotationZ(float radians, Vector3 centerPoint) =>
            CreateRotationZ(radians, centerPoint.X, centerPoint.Y);

        public static Matrix CreateRotationZ(float radians, Vector2 centerPoint) =>
            CreateRotationZ(radians, centerPoint.X, centerPoint.Y);

        public static Matrix CreateFromAxisAngle(Vector3 axis, float radians)
        {
            float x = axis.X, y = axis.Y, z = axis.Z;
            float sa = (float)Math.Sin(radians), ca = (float)Math.Cos(radians);
            float xx = x * x, yy = y * y, zz = z * z;
            float xy = x * y, xz = x * z, yz = y * z;

            return new Matrix(
                xx + ca * (1f - xx), xy - ca * xy + sa * z, xz - ca * xz - sa * y, 0f,
                xy - ca * xy - sa * z, yy + ca * (1f - yy), yz - ca * yz + sa * x, 0f,
                xz - ca * xz + sa * y, yz - ca * yz - sa * x, zz + ca * (1 - zz), 0f,
                0f, 0f, 0f, 1f);
        }

        public static Matrix CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance)
        {
            if (nearPlaneDistance <= 0.0f)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            if (farPlaneDistance <= 0.0f)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));

            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            var x = 2f * nearPlaneDistance / width;
            var y = 2f * nearPlaneDistance / height;

            return new Matrix(
                x, 0f, 0f, 0f,
                0f, y, 0f, 0f,
                0f, 0f, farPlaneDistance / (nearPlaneDistance - farPlaneDistance), -1f,
                0f, 0f, nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance), 0f);
        }

        public static Matrix CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
        {
            if (fieldOfView <= 0f || fieldOfView >= MathHelper.PI)
                throw new ArgumentOutOfRangeException(nameof(fieldOfView));

            if (nearPlaneDistance <= 0.0f)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            if (farPlaneDistance <= 0.0f)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));

            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            float y = 1f / (float)Math.Tan(fieldOfView * 0.5f);
            float x = y / aspectRatio;

            return new Matrix(
                x, 0f, 0f, 0f,
                0f, y, 0f, 0f,
                0f, 0f, farPlaneDistance / (nearPlaneDistance - farPlaneDistance), -1f,
                0f, 0f, nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance), 0f);
        }

        public static Matrix CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance)
        {
            if (nearPlaneDistance <= 0.0f)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            if (farPlaneDistance <= 0.0f)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));

            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            var x = 2f * nearPlaneDistance / (right - left);
            var y = 2f * nearPlaneDistance / (top - bottom);

            var posX = (left + right) / (right - left);
            var posY = (top + bottom) / (top - bottom);

            return new Matrix(
                x, 0f, 0f, 0f,
                0f, y, 0f, 0f,
                posX, posY, farPlaneDistance / (nearPlaneDistance - farPlaneDistance), -1f,
                0f, 0f, nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance), 0f);
        }

        public static Matrix CreateOrthographic(float width, float height, float nearPlaneDistance, float farPlaneDistance)
        {
            var x = 2f / width;
            var y = 2f / height;

            return new Matrix(
                x, 0f, 0f, 0f,
                0f, y, 0f, 0f,
                0f, 0f, 1f / (nearPlaneDistance - farPlaneDistance), 0f,
                0f, 0f, nearPlaneDistance / (nearPlaneDistance - farPlaneDistance), 1f);
        }

        public static Matrix CreateOrthographicOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance)
        {
            var x = 2f / (right - left);
            var y = 2f / (bottom - top);

            var posX = (left + right) / (left - right);
            var posY = (top + bottom) / (bottom - top);

            return new Matrix(
                x, 0f, 0f, 0f,
                0f, y, 0f, 0f,
                0f, 0f, 1f / (nearPlaneDistance - farPlaneDistance), 0f,
                posX, posY, nearPlaneDistance / (nearPlaneDistance - farPlaneDistance), 1f);
        }

        public static Matrix CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
        {
            Vector3 zAxis = Vector3.Normalize(cameraPosition - cameraTarget);
            Vector3 xAxis = Vector3.Normalize(Vector3.Cross(cameraUpVector, zAxis));
            Vector3 yAxis = Vector3.Cross(zAxis, xAxis);

            return new Matrix(
                xAxis.X, yAxis.X, zAxis.X, 0f,
                xAxis.Y, yAxis.Y, zAxis.Y, 0f,
                xAxis.Z, yAxis.Z, zAxis.Z, 0f,
                -Vector3.Dot(xAxis, cameraPosition), -Vector3.Dot(yAxis, cameraPosition), -Vector3.Dot(zAxis, cameraPosition), 1f);
        }

        public static Matrix CreateWorld(Vector3 position, Vector3 forward, Vector3 up)
        {
            Vector3 zAxis = Vector3.Normalize(-forward);
            Vector3 xAxis = Vector3.Normalize(Vector3.Cross(up, zAxis));
            Vector3 yAxis = Vector3.Cross(zAxis, xAxis);

            return new Matrix(
                xAxis.X, yAxis.X, zAxis.X, 0f,
                xAxis.Y, yAxis.Y, zAxis.Y, 0f,
                xAxis.Z, yAxis.Z, zAxis.Z, 0f,
                position.X, position.Y, position.Z, 1f);
        }

        //public static Matrix4x4 CreateFromQuaternion(Quaternion quaternion)
        //{
        //    Matrix4x4 result;

        //    float xx = quaternion.X * quaternion.X;
        //    float yy = quaternion.Y * quaternion.Y;
        //    float zz = quaternion.Z * quaternion.Z;

        //    float xy = quaternion.X * quaternion.Y;
        //    float wz = quaternion.Z * quaternion.W;
        //    float xz = quaternion.Z * quaternion.X;
        //    float wy = quaternion.Y * quaternion.W;
        //    float yz = quaternion.Y * quaternion.Z;
        //    float wx = quaternion.X * quaternion.W;

        //    result.M11 = 1.0f - 2.0f * (yy + zz);
        //    result.M12 = 2.0f * (xy + wz);
        //    result.M13 = 2.0f * (xz - wy);
        //    result.M14 = 0.0f;
        //    result.M21 = 2.0f * (xy - wz);
        //    result.M22 = 1.0f - 2.0f * (zz + xx);
        //    result.M23 = 2.0f * (yz + wx);
        //    result.M24 = 0.0f;
        //    result.M31 = 2.0f * (xz + wy);
        //    result.M32 = 2.0f * (yz - wx);
        //    result.M33 = 1.0f - 2.0f * (yy + xx);
        //    result.M34 = 0.0f;
        //    result.M41 = 0.0f;
        //    result.M42 = 0.0f;
        //    result.M43 = 0.0f;
        //    result.M44 = 1.0f;

        //    return result;
        //}

        //public static Matrix4x4 CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        //{
        //    Quaternion q = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);

        //    return Matrix4x4.CreateFromQuaternion(q);
        //}

        //public static Matrix4x4 CreateShadow(Vector3 lightDirection, Plane plane)
        //{
        //    Plane p = Plane.Normalize(plane);

        //    float dot = p.Normal.X * lightDirection.X + p.Normal.Y * lightDirection.Y + p.Normal.Z * lightDirection.Z;
        //    float a = -p.Normal.X;
        //    float b = -p.Normal.Y;
        //    float c = -p.Normal.Z;
        //    float d = -p.D;

        //    Matrix4x4 result;

        //    result.M11 = a * lightDirection.X + dot;
        //    result.M21 = b * lightDirection.X;
        //    result.M31 = c * lightDirection.X;
        //    result.M41 = d * lightDirection.X;

        //    result.M12 = a * lightDirection.Y;
        //    result.M22 = b * lightDirection.Y + dot;
        //    result.M32 = c * lightDirection.Y;
        //    result.M42 = d * lightDirection.Y;

        //    result.M13 = a * lightDirection.Z;
        //    result.M23 = b * lightDirection.Z;
        //    result.M33 = c * lightDirection.Z + dot;
        //    result.M43 = d * lightDirection.Z;

        //    result.M14 = 0.0f;
        //    result.M24 = 0.0f;
        //    result.M34 = 0.0f;
        //    result.M44 = dot;

        //    return result;
        //}

        public float Determinant()
        {
            // | a b c d |     | f g h |     | e g h |     | e f h |     | e f g |
            // | e f g h | = a | j k l | - b | i k l | + c | i j l | - d | i j k |
            // | i j k l |     | n o p |     | m o p |     | m n p |     | m n o |
            // | m n o p |
            //
            //   | f g h |
            // a | j k l | = a ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
            //   | n o p |
            //
            //   | e g h |     
            // b | i k l | = b ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
            //   | m o p |     
            //
            //   | e f h |
            // c | i j l | = c ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
            //   | m n p |
            //
            //   | e f g |
            // d | i j k | = d ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
            //   | m n o |
            //
            // Cost of operation
            // 17 adds and 28 muls.
            //
            // add: 6 + 8 + 3 = 17
            // mul: 12 + 16 = 28

            float a = M11, b = M12, c = M13, d = M14;
            float e = M21, f = M22, g = M23, h = M24;
            float i = M31, j = M32, k = M33, l = M34;
            float m = M41, n = M42, o = M43, p = M44;

            float kp_lo = k * p - l * o;
            float jp_ln = j * p - l * n;
            float jo_kn = j * o - k * n;
            float ip_lm = i * p - l * m;
            float io_km = i * o - k * m;
            float in_jm = i * n - j * m;

            return a * (f * kp_lo - g * jp_ln + h * jo_kn) -
                   b * (e * kp_lo - g * ip_lm + h * io_km) +
                   c * (e * jp_ln - f * ip_lm + h * in_jm) -
                   d * (e * jo_kn - f * io_km + g * in_jm);
        }

        public static bool Invert(Matrix matrix, out Matrix result)
        {
            //                                       -1
            // If you have matrix M, inverse Matrix M   can compute
            //
            //     -1       1      
            //    M   = --------- A
            //            det(M)
            //
            // A is adjugate (adjoint) of M, where,
            //
            //      T
            // A = C
            //
            // C is Cofactor matrix of M, where,
            //           i + j
            // C   = (-1)      * det(M  )
            //  ij                    ij
            //
            //     [ a b c d ]
            // M = [ e f g h ]
            //     [ i j k l ]
            //     [ m n o p ]
            //
            // First Row
            //           2 | f g h |
            // C   = (-1)  | j k l | = + ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
            //  11         | n o p |
            //
            //           3 | e g h |
            // C   = (-1)  | i k l | = - ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
            //  12         | m o p |
            //
            //           4 | e f h |
            // C   = (-1)  | i j l | = + ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
            //  13         | m n p |
            //
            //           5 | e f g |
            // C   = (-1)  | i j k | = - ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
            //  14         | m n o |
            //
            // Second Row
            //           3 | b c d |
            // C   = (-1)  | j k l | = - ( b ( kp - lo ) - c ( jp - ln ) + d ( jo - kn ) )
            //  21         | n o p |
            //
            //           4 | a c d |
            // C   = (-1)  | i k l | = + ( a ( kp - lo ) - c ( ip - lm ) + d ( io - km ) )
            //  22         | m o p |
            //
            //           5 | a b d |
            // C   = (-1)  | i j l | = - ( a ( jp - ln ) - b ( ip - lm ) + d ( in - jm ) )
            //  23         | m n p |
            //
            //           6 | a b c |
            // C   = (-1)  | i j k | = + ( a ( jo - kn ) - b ( io - km ) + c ( in - jm ) )
            //  24         | m n o |
            //
            // Third Row
            //           4 | b c d |
            // C   = (-1)  | f g h | = + ( b ( gp - ho ) - c ( fp - hn ) + d ( fo - gn ) )
            //  31         | n o p |
            //
            //           5 | a c d |
            // C   = (-1)  | e g h | = - ( a ( gp - ho ) - c ( ep - hm ) + d ( eo - gm ) )
            //  32         | m o p |
            //
            //           6 | a b d |
            // C   = (-1)  | e f h | = + ( a ( fp - hn ) - b ( ep - hm ) + d ( en - fm ) )
            //  33         | m n p |
            //
            //           7 | a b c |
            // C   = (-1)  | e f g | = - ( a ( fo - gn ) - b ( eo - gm ) + c ( en - fm ) )
            //  34         | m n o |
            //
            // Fourth Row
            //           5 | b c d |
            // C   = (-1)  | f g h | = - ( b ( gl - hk ) - c ( fl - hj ) + d ( fk - gj ) )
            //  41         | j k l |
            //
            //           6 | a c d |
            // C   = (-1)  | e g h | = + ( a ( gl - hk ) - c ( el - hi ) + d ( ek - gi ) )
            //  42         | i k l |
            //
            //           7 | a b d |
            // C   = (-1)  | e f h | = - ( a ( fl - hj ) - b ( el - hi ) + d ( ej - fi ) )
            //  43         | i j l |
            //
            //           8 | a b c |
            // C   = (-1)  | e f g | = + ( a ( fk - gj ) - b ( ek - gi ) + c ( ej - fi ) )
            //  44         | i j k |
            //
            // Cost of operation
            // 53 adds, 104 muls, and 1 div.
            float a = matrix.M11, b = matrix.M12, c = matrix.M13, d = matrix.M14;
            float e = matrix.M21, f = matrix.M22, g = matrix.M23, h = matrix.M24;
            float i = matrix.M31, j = matrix.M32, k = matrix.M33, l = matrix.M34;
            float m = matrix.M41, n = matrix.M42, o = matrix.M43, p = matrix.M44;

            float kp_lo = k * p - l * o;
            float jp_ln = j * p - l * n;
            float jo_kn = j * o - k * n;
            float ip_lm = i * p - l * m;
            float io_km = i * o - k * m;
            float in_jm = i * n - j * m;

            float a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
            float a12 = -(e * kp_lo - g * ip_lm + h * io_km);
            float a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
            float a14 = -(e * jo_kn - f * io_km + g * in_jm);

            float det = a * a11 + b * a12 + c * a13 + d * a14;

            if (MathHelper.NearEquals(det, 0f))
            {
                result = new Matrix(float.NaN, float.NaN, float.NaN, float.NaN,
                                    float.NaN, float.NaN, float.NaN, float.NaN,
                                    float.NaN, float.NaN, float.NaN, float.NaN,
                                    float.NaN, float.NaN, float.NaN, float.NaN);
                return false;
            }

            float invDet = 1.0f / det;

            result.M11 = a11 * invDet;
            result.M21 = a12 * invDet;
            result.M31 = a13 * invDet;
            result.M41 = a14 * invDet;

            result.M12 = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
            result.M22 = +(a * kp_lo - c * ip_lm + d * io_km) * invDet;
            result.M32 = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
            result.M42 = +(a * jo_kn - b * io_km + c * in_jm) * invDet;

            float gp_ho = g * p - h * o;
            float fp_hn = f * p - h * n;
            float fo_gn = f * o - g * n;
            float ep_hm = e * p - h * m;
            float eo_gm = e * o - g * m;
            float en_fm = e * n - f * m;

            result.M13 = +(b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
            result.M23 = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
            result.M33 = +(a * fp_hn - b * ep_hm + d * en_fm) * invDet;
            result.M43 = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;

            float gl_hk = g * l - h * k;
            float fl_hj = f * l - h * j;
            float fk_gj = f * k - g * j;
            float el_hi = e * l - h * i;
            float ek_gi = e * k - g * i;
            float ej_fi = e * j - f * i;

            result.M14 = -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet;
            result.M24 = +(a * gl_hk - c * el_hi + d * ek_gi) * invDet;
            result.M34 = -(a * fl_hj - b * el_hi + d * ej_fi) * invDet;
            result.M44 = +(a * fk_gj - b * ek_gi + c * ej_fi) * invDet;

            return true;
        }
    }
}
