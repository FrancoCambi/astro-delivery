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
        titleText.text = "<size=50>Demo</size>";
#endif
#if FULL_BUILD
        titleText.text = "";
#endif



    }

}
