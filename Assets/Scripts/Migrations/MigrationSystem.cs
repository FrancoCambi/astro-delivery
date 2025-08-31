using System;
using UnityEngine;

public class MigrationSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelsMenu levelsMenu;

    private GameData gameData;
    private Version saveVersion;

    void Start()
    {
        gameData = PersistenceManager.Load();
        saveVersion = gameData.SaveVersion;

        if (saveVersion == null)
        {
            PersistenceManager.UpdateVersion(new Version(0, 1, 0));
            saveVersion = new Version(0, 1, 0);
        }

        if (saveVersion < GameConstants.GameVersion)
        {
            Migrate();
        }
    }

    
    private void Migrate()
    {
        if (saveVersion <= new Version(0,2,0))
        {
            Migration_0_1_0_to_0_2_0();
            PersistenceManager.UpdateVersion(new Version(0,2,0));
        }

#if UNITY_EDITOR
        Debug.Log($"Migration Complete. New Version: {PersistenceManager.Load().SaveVersion}");
#endif
    }

    #region MIGRATIONS

    private void Migration_0_1_0_to_0_2_0()
    {
        for (int i = 1; i <= gameData.MaxLevelUnlocked; i++)
        {

            float completedTime = gameData.MinTimes[i];
            if (completedTime == float.MaxValue) continue;

            int newStars = levelsMenu.GetStarsAmount(i, completedTime);
            PersistenceManager.UpdateStarsInLevel(i, newStars);
        }
    }

    #endregion
}
