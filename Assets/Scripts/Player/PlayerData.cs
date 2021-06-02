using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string currentLevel;
    public float[] position;
    public int[] levels;
    public int[] completedLevels;

    public void Clear()
    {
        currentLevel = "";
    }
}

public static class StaticData
{
    public static bool hasMainSceneLoaded = false;
}
