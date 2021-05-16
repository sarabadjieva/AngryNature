using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public float[] position;
    public int[] levels;
    public int[] completedLevels;

    public void Clear()
    {
        level = 0;
        health = 0;
    }
}

public static class StaticData
{
    public static bool hasMainSceneLoaded = false;
}
