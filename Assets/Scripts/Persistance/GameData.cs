using System;

[Serializable]
public class GameData
{
    public Version SaveVersion;
    public int MaxLevelUnlocked;
    public float[] MinTimes = new float[GameConstants.LevelsAmount + 1];
    public int[] LevelStars = new int[GameConstants.LevelsAmount + 1];

    public GameData()
    {
        SaveVersion = GameConstants.GameVersion;
        MaxLevelUnlocked = 1;
        for (int i = 0; i <= GameConstants.LevelsAmount; i++)
        {
            MinTimes[i] = float.MaxValue;
            LevelStars[i] = 0;
        }
    }

}
