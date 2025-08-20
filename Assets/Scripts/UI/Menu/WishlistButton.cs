using UnityEngine;

public class WishlistButton : MonoBehaviour
{
    public void OpenSteamPage()
    {
        if (SteamIntegration.Instance.IsConnected)
        {
            Application.OpenURL("steam://openurl/https://store.steampowered.com/app/3927530/Astro_Delivery/");
        }
        else
        {
            Application.OpenURL("https://store.steampowered.com/app/3927530/Astro_Delivery/");
        }
    }
}
