using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public static class ListExtensions
{
    private static Random rnd = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static List<Tb> Shuffle<Ta, Tb>(this IList<Ta> listA, IList<Tb> listB)
    {
        if (listB.Count < listA.Count) throw new IndexOutOfRangeException();
        
        int n = listA.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            Ta valueA = listA[k];
            Tb valueB = listB[k];
            listA[k] = listA[n];
            listB[k] = listB[n];
            listA[n] = valueA;
            listB[n] = valueB;
        }

        return listB.ToList();
    }

    public static float AngleToRadian(this float angle)
    {
        return (float)(angle *Math.PI/ 180);
    }

    public static float RadianToAngle(this float rad)
    {
        return (float)(rad / Math.PI  * 180);
    }

    public static Vector2 RotateRadians(this Vector2 v, double radians)
    {
        var ca = (float)Math.Cos(radians);
        var sa = (float)Math.Sin(radians);
        return new Vector2(ca * v.x - sa * v.y, sa * v.x + ca * v.y);
    }
}
