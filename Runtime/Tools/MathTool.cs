using System;
using UnityEngine;

namespace NonsensicalKit.Tools
{
    public static class MathTool
    {
        /// <summary>
        /// 判断是否为质数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsPrime(int num)
        {
            if (num <= 3)
            {
                return num > 1;
            }
            // 不在6的倍数两侧的一定不是质数
            if (num % 6 != 1 && num % 6 != 5)
            {
                return false;
            }

            int sqrt = (int)Math.Sqrt(num);
            for (int i = 5; i <= sqrt; i += 6)
            {
                if (num % i == 0 || num % (i + 2) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 交换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        public static void Swap<T>(T[] values, int i1, int i2)
        {
            T tmp = values[i1];
            values[i1] = values[i2];
            values[i2] = tmp;
        }

        public static int IndexOfMin<T>(this T[] array) where T : struct, IComparable<T>
        {
            if (array.Length == 0)
            {
                return -1;
            }
            int minIndex = 0;
            T min = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (min.CompareTo(array[i]) > 0)
                {
                    minIndex = i;
                    min = array[i];
                }
            }
            return minIndex;
        }

        /// <summary>
        /// 排序大顶堆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T[] SortMaxHeap<T>(T[] values) where T : struct, IComparable<T>
        {
            for (int i = values.Length - 1; i > 0; i--)
            {
                Swap(values, 0, i);

                int index = 0;
                while (2 * index + 1 < i)
                {
                    int child = 2 * index + 1;

                    if (child + 1 < i)
                    {
                        if (values[child].CompareTo(values[index]) < 0 && values[child + 1].CompareTo(values[index]) < 0)
                            break;
                        if (values[child].CompareTo(values[child + 1]) < 0) child++;
                        Swap(values, index, child);
                        index = child;
                    }
                    else
                    {
                        if (values[child].CompareTo(values[index]) < 0)
                            break;
                        Swap(values, index, child);
                        index = child;
                    }
                }
            }
            return values;
        }

        /// <summary>
        /// 排序小顶堆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T[] SortHeap<T>(T[] values) where T : struct, IComparable<T>
        {
            for (int i = values.Length - 1; i > 0; i--)
            {
                Swap(values, 0, i);

                int index = 0;
                while (2 * index + 1 < i)
                {
                    int child = 2 * index + 1;

                    if (child + 1 < i)
                    {
                        if (values[child].CompareTo(values[index]) > 0 && values[child + 1].CompareTo(values[index]) > 0)
                            break;
                        if (values[child].CompareTo(values[child + 1]) > 0) child++;
                        Swap(values, index, child);
                        index = child;
                    }
                    else
                    {
                        if (values[child].CompareTo(values[index]) > 0)
                            break;
                        Swap(values, index, child);
                        index = child;
                    }
                }
            }
            return values;
        }

        /// <summary>
        /// 获取传入数值的级数
        /// </summary>
        /// <param name="rawNum">传入数值</param>
        /// <returns>级数，个位数（如5）是0级，十位数（25）是1级，一位小数（0.5）是-1级</returns>
        public static int GetLevel(float rawNum)
        {
            if (rawNum == 0)
            {
                return 0;
            }
            rawNum = Mathf.Abs(rawNum);
            if (rawNum > 1)
            {
                int level = -1;
                float crtNum = rawNum;
                while (crtNum > 1)
                {
                    crtNum /= 10;
                    level++;
                }
                return level;
            }
            else
            {
                int level = 0;
                float crtNum = rawNum;
                while (crtNum < 1)
                {
                    crtNum *= 10;
                    level--;
                }
                return level;
            }
        }

        /// <summary>
        /// 根据传入float变量与当前等级求最近的整值float变量
        /// </summary>
        /// <param name="rawFloat">传入的float变量</param>
        /// <param name="level">传入的float变量</param>
        /// <returns>最近的整值float变量</returns>
        public static float GetNearValue(float rawFloat, int level)
        {
            float crtFloat = rawFloat / Mathf.Pow(10, level);
            float nearInt = Mathf.Round(crtFloat);
            return nearInt * Mathf.Pow(10, level);
        }

        public static float GetNearInt(float rawFloat, int magnification = 1, int offset = 0)
        {
            return Mathf.Round((rawFloat + offset) / magnification) * magnification - offset;
        }

        /// <summary>
        /// 根据传入float变量与当前等级求最近的整值float变量
        /// </summary>
        /// <param name="rawFloat">传入的float变量</param>
        /// <param name="level">传入的float变量</param>
        /// <returns>最近的整值float变量</returns>
        public static float GetNearValueUse2(float rawFloat, int level)
        {
            float crtFloat = rawFloat / Mathf.Pow(2, level);
            float nearInt = Mathf.Round(crtFloat);
            return nearInt * Mathf.Pow(2, level);
        }

        /// <summary>
        /// 进一法取整至整数
        /// </summary>
        /// <param name="rawFloat"></param>
        /// <returns></returns>
        public static int RoundingToInt_Add(this float rawFloat)
        {
            int rawFloatIntPart = (int)rawFloat;
            if (rawFloat - rawFloatIntPart > 0)
            {
                return rawFloatIntPart + 1;
            }
            else
            {
                return rawFloatIntPart;
            }
        }

        /// <summary>
        /// 判断两个float是否非常接近
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static bool IsNear(float num1, float num2)
        {
            float absX = Mathf.Abs(num1 - num2);
            return absX < 0.000001f;
        }

        public static float[] ArrayPlus(float[] array1, float[] array2)
        {
            int min = Mathf.Min(array1.Length, array2.Length);
            int max = Mathf.Max(array1.Length, array2.Length);
            bool isArray1Long = array1.Length > array2.Length;

            float[] newArray = new float[max];

            for (int i = 0; i < min; i++)
            {
                newArray[i] = array1[i] + array2[i];
            }

            float[] lastArray = isArray1Long ? array1 : array2;

            for (int i = min; i < max; i++)
            {
                newArray[i] = lastArray[i];
            }

            return newArray;
        }
    }
}
