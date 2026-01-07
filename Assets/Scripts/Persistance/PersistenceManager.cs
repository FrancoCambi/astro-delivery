using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class PersistenceManager
{
    private const string FILENAME = "/GameData.sav";

    public static event Action OnGameDataUpdated;

    public static void Save(GameData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + FILENAME, FileMode.Create);

        bf.Serialize(stream, data);
        stream.Close();

        OnGameDataUpdated?.Invoke();
    }

    public static GameData Load()
    {
        if (File.Exists(Application.persistentDataPath + FILENAME))
        {
            BinaryFormatter bf = new();
            FileStream stream = new(Application.persistentDataPath + FILENAME, FileMode.Open);

            GameData data = bf.Deserialize(stream) as GameData;

            stream.Close();

            return data;
        }
        else
        {
            return new GameData();
        }
    }

    public static void UpdateMaxLevelUnlocked(int newMax)
    {
        GameData data = Load();

        data.MaxLevelUnlocked = newMax;

        Save(data);
    }

    public static void UpdateMinTimeInLevel(int level, float newTime)
    {
        GameData data = Load();

        data.MinTimes[level] = newTime;

        Save(data);
    }

    public static void AppendToMinTimes(float newTime)
    {
        GameData data = Load();

        data.MinTimes = data.MinTimes.Concat(new float[] { newTime }).ToArray();

        Save(data);
    }

    public static void UpdateStarsInLevel(int level, int newStars)
    {
        GameData data = Load();

        data.LevelStars[level] = newStars;

        Save(data);
    }

    public static void AppendToStarsInLevel(int stars)
    {
        GameData data = Load();

        data.LevelStars = data.LevelStars.Concat(new int[] { stars }).ToArray();

        Save(data);
    }

    public static void UpdateVersion(Version newVersion)
    {
        GameData data = Load();

        data.SaveVersion = newVersion;

        Save(data);
    }

    public static void UpdateLevelAttempts(int level, int newAttempts)
    {
        GameData data = Load();

        data.LevelAttempts[level] = newAttempts;

        Save(data);
    }

    public static void AddLevelAttempt(int level)
    {
        GameData data = Load();

        data.LevelAttempts[level]++;

        Save(data);
    }

    public static void UpdateLevelStreaks(int level, int newStreak)
    {
        GameData data = Load();

        data.LevelStreaks[level] = newStreak;

        Save(data);
    }

    public static void AddLevelStreak(int level)
    {
        GameData data = Load();

        data.LevelStreaks[level]++;

        Save(data);
    }
}
