﻿using UnityEngine;

public static class Extensions 
{
    public static string ToHeader(this string str)
    {
        char[] chars = str.ToUpper().ToCharArray();
        return string.Join(" ", chars);
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
}
