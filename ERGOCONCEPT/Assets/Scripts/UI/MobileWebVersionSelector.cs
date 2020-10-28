using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileWebVersionSelector : MonoBehaviour
{
    public GameObject WebVersion;
    public GameObject MobileVersion;

    private void OnEnable()
    {
        bool isMobile = Application.isMobilePlatform;
        WebVersion.SetActive(!isMobile);
        MobileVersion.SetActive(isMobile);
    }
}