using UnityEngine;

public static class ArrayExtensions
{
    public static void Shuffle<T>(this T[] list)
    {
        T temp;
        for (int i = 0; i < list.Length; i++)
        {
            int randomIndex = Random.Range(i, list.Length);
            temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
