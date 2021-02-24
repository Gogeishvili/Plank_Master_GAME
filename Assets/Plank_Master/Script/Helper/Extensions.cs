using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random random = new System.Random();
        int index = list.Count;
        while (index > 1)
        {
            index--;
            int k = random.Next(index + 1);
            T value = list[k];
            list[k] = list[index];
            list[index] = value;
        }
    }

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
