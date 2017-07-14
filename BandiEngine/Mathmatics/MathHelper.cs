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
    public static class MathHelper
    {
        public const float ZeroTolerance = 1e-6f;

        public const float PI = (float)System.Math.PI;

        /// <summary>
        /// 회전수를 도 단위로 변환합니다.
        /// </summary>
        /// <param name="revolution"></param>
        /// <returns></returns>
        public static float RevolutionsToDegrees(float revolution) =>
            revolution * 360;

        /// <summary>
        /// 회전수를 라디안 단위로 변환합니다.
        /// </summary>
        /// <param name="revolution"></param>
        /// <returns></returns>
        public static float RevolutionsToRadians(float revolution) =>
            revolution * (2 * PI);

        /// <summary>
        /// 도 단위를 회전수로 변환합니다.
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static float DegreesToRevolutions(float degree) =>
            degree / 360;

        /// <summary>
        /// 도 단위를 라디안 단위로 변환합니다.
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static float DegreesToRadians(float degree) =>
            degree * (PI / 180);

        /// <summary>
        /// 라디안 단위를 회전수로 변환합니다.
        /// </summary>
        /// <param name="radian"></param>
        /// <returns></returns>
        public static float RadiansToRevolutions(float radian) =>
            radian / (2 * PI);

        /// <summary>
        /// 라디안 단위를 도 단위로 변환합니다.
        /// </summary>
        /// <param name="radian"></param>
        /// <returns></returns>
        public static float RadiansToDegrees(float radian) =>
            radian * (180 / PI);

        public static double Clamp(double value, double min, double max) =>
            value < min ? min :
            value > max ? max :
            value;

        public static float Clamp(float value, float min, float max) =>
            value < min ? min :
            value > max ? max :
            value;

        public static long Clamp(long value, long min, long max) =>
            value < min ? min :
            value > max ? max :
            value;

        public static int Clamp(int value, int min, int max) =>
            value < min ? min :
            value > max ? max :
            value;

        public static double Lerp(double from, double to, double amount) =>
            (1 - amount) * from + amount * to;

        public static float Lerp(float from, float to, float amount) =>
            (1 - amount) * from + amount * to;

        public static double SmoothStep(double amount) =>
            (amount <= 0) ? 0 :
            (amount >= 1) ? 1 :
            amount * amount * (3 - (2 * amount));

        public static float SmoothStep(float amount) =>
            (amount <= 0) ? 0 :
            (amount >= 1) ? 1 :
            amount * amount * (3 - (2 * amount));

        public static double SmootherStep(double amount) =>
            (amount <= 0) ? 0 :
            (amount >= 1) ? 1 :
            amount * amount * amount * (amount * ((amount * 6) - 15) + 10);

        public static float SmootherStep(float amount) =>
            (amount <= 0) ? 0 :
            (amount >= 1) ? 1 :
            amount * amount * amount * (amount * ((amount * 6) - 15) + 10);
    }
}
