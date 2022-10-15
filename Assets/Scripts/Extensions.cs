using System;
using UnityEngine;
using Zomlypse.Enums;
using Zomlypse.IO.Containers;

namespace Zomlypse.Extensions
{
    public static class Extensions
    {
        public static string ToHeader(this string str)
        {
            char[] chars = str.ToUpper().ToCharArray();
            return string.Join(" ", chars);
        }

        public static string ToHeader(this Enum type)
        {
            return ToHeader(type.ToString());
        }

        public static int CountChar(this string str, char c)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != c)
                {
                    continue;
                }

                count++;
            }
            return count;
        }

        public static Direction DirectionTo(this Vector3 vector, Vector3 target)
        {
            Vector3 direction = (target - vector).normalized;
            float x = Mathf.Abs(direction.x);
            float y = Mathf.Abs(direction.y);

            if (x > y)
            {
                return direction.x > 0
                    ? Direction.Right
                    : Direction.Left;
            }
            else if (x < y)
            {
                return direction.y > 0
                    ? Direction.Up
                    : Direction.Down;
            }
            else
            {
                return Direction.Normal;
            }
        }

        public static string FormatDate(this DateTime date, IFormatProvider format)
        {
            return $"{date.DayOfWeek}, {date.ToString("d MMMM yyyy", format)}";
        }

        public static string FormatTime(this DateTime date, IFormatProvider format)
        {
            return date.ToString("HH:mm", format);
        }

        public static Color32 ToColor32(this HexColor hex)
        {
            return new Color32(hex.RR, hex.GG, hex.BB, byte.MaxValue);
        }

        public static Gender Opposite(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return Gender.Female;
                case Gender.Female:
                    return Gender.Male;
                default:
                    throw new InvalidOperationException($"{nameof(gender)} must be of either {Gender.Male} or {Gender.Female}");
            }
        }

        public static string Format(this TextColor color)
        {
            return color.ToString().ToLower();
        }
    }
}
