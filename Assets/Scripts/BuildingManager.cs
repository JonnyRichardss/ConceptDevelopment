using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingManager
{
     public static List<BuildingScriptable> NewDrawPile = new List<BuildingScriptable>();
    public static List<BuildingScriptable> DrawPile = new List<BuildingScriptable>();
    public static List<BuildingScriptable> DiscardPile = new List<BuildingScriptable>();
    public static List<BuildingScriptable> DrawBuildings(ResourceHolder weighting)
    {
        if (DrawPile.Count < 4)
        {
            DrawPile.AddRange(DiscardPile);
            DiscardPile.Clear();
        }
        List<BuildingScriptable> output = new List<BuildingScriptable>();
        ShuffleList<BuildingScriptable>.Shuffle(ref DrawPile);
        for(int i = 0; i < 3; i++)
        {
            BuildingScriptable b = DrawPile[i];
            DrawPile.Remove(b);
            DiscardPile.Add(b);
            output.Add(b);
        }
        return output;
    }
    public static void Reset()
    {
        DiscardPile.Clear();
        DrawPile = NewDrawPile;
    }
}
