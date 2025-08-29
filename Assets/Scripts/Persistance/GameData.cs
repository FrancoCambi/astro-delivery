using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public int MaxLevelUnlocked;
    public float[] MinTimes = new float[GameConstants.LevelsAmount];
    public int[] LevelStars = new int[GameConstants.LevelsAmount];

    public GameData()
    {
        MaxLevelUnlocked = 1;
        for (int i = 0; i < GameConstants.LevelsAmount; i++)
        {
            MinTimes[i] = float.MaxValue;
            LevelStars[i] = 0;
        }
    }

}
