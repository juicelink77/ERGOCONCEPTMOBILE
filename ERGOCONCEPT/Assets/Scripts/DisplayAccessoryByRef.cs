using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAccessoryByRef : MonoBehaviour
{
    public string Reference;
    public GameObject Accessory;
    public List<GameObject> SerieAccessories;

    public bool IsDisplay = false;

    public void Awake()
    {
        DisplayAccessory(false);
    }

    public void DisplayAccessory(bool value)
    {
        IsDisplay = value;

        if (Accessory != null)
            Accessory.SetActive(value);

        foreach(GameObject g in SerieAccessories)
            g.SetActive(!value);
    }

    public void RefreshAccessory(bool isDisplay)
    {
        DisplayAccessory(isDisplay);
    }
}