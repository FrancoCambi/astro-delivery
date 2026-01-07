using Steamworks;
using Steamworks.Data;
using UnityEngine;

public static class SteamAchievements
{
    public static bool IsUnlocked(string id)
    {
        if (!SteamIntegration.Instance.IsConnected) return false;

        Achievement ach = new Achievement(id);
        return ach.State;
    }

    public static void UnlockAchievement(string id)
    {
        if (!SteamIntegration.Instance.IsConnected) return;

        Achievement ach = new Achievement(id);
        ach.Trigger();
    }

    public static void ClearAchievementStatus(string id)
    {
        if (!SteamIntegration.Instance.IsConnected) return;

        Achievement ach = new Achievement(id);
        ach.Clear();
    }
}
