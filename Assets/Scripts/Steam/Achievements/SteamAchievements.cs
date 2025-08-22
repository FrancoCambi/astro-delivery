using Steamworks;
using Steamworks.Data;
using UnityEngine;

public static class SteamAchievements
{
    public static bool IsUnlocked(string id)
    {
        Achievement ach = new Achievement(id);
        return ach.State;
    }

    public static void UnlockAchievement(string id)
    {
        Achievement ach = new Achievement(id);
        ach.Trigger();
    }

    public static void ClearAchievementStatus(string id)
    {
        Achievement ach = new Achievement(id);
        ach.Clear();
    }
}
