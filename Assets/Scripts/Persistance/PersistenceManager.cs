using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class PersistenceManager
{
    private const string FILENAME = "/GameData.sav";

    public static void Save(GameData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + FILENAME, FileMode.Create);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static GameData Load()
    {
        if (File.Exists(Application.persistentDataPath + FILENAME))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + FILENAME, FileMode.Open);

            GameData data = bf.Deserialize(stream) as GameData;

            stream.Close();

            return data;
        }
        else
        {
            GameData emptyData = new();
            Save(emptyData);
            return Load();
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
    public static void UpdateStarsInLevel(int level, int newStars)
    {
        GameData data = Load();

        data.LevelStars[level] = newStars;

        Save(data);
    }
}
