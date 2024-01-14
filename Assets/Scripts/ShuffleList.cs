using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ShuffleList<T>
{
    public static void Shuffle(ref List<T> list)
    {
        // Lifted from last year's OOP assignment :)
        for (int i = list.Count - 1; i >= 0; i--)
        {
            float jF = Random.value;
            jF *= list.Count - 1;
            int j = (int)jF;

            T currElem = list[i];
            T randomElem = list[j];

            list[i] = randomElem;
            list[j] = currElem;

        }
    }
}
