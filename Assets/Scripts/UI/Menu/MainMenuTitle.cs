using System.Drawing;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MainMenuTitle : MonoBehaviour
{
    private TextMeshProUGUI titleText;

    private void Awake()
    {
        titleText = GetComponent<TextMeshProUGUI>();

#if DEMO_BUILD
        titleText.text = "ASTRO DELIVERY<size=45> Demo</size>";
#endif
#if FULL_BUILD
        titleText.text = "ASTRO DELIVERY";
#endif



    }

}
