﻿using System;
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
            else
            {
                return direction.y > 0
                    ? Direction.Up
                    : Direction.Down;
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
    }
}
