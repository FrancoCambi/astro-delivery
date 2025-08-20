using Steamworks;
using UnityEngine;

public class SteamIntegration : MonoBehaviour
{ 
    private static SteamIntegration instance;

    public static SteamIntegration Instance
    {
        get
        {
            instance = instance != null ? instance : FindAnyObjectByType<SteamIntegration>();
            return instance;
        }
    }

    public bool IsConnected
    {
        get
        {
            return SteamClient.IsValid;
        }
    }

    void Start()
    {
        if (!IsConnected) ConnectToSteam();
    }

    private void Update()
    {
        SteamClient.RunCallbacks();
    }

    private void OnApplicationQuit()
    {
        SteamClient.Shutdown();
    }

    private void ConnectToSteam()
    {
        try
        {
#if DEMO_BUILD
            SteamClient.Init(3971100);
#endif
#if FULL_BUILD
            SteamClient.Init(3927530);
#endif
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }


}
