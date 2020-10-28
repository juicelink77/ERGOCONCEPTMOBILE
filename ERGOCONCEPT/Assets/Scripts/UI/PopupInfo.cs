using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class PopupInfo : Popup
{
    public Text Name;
    public Text Description;
    public Image Picture;

    public void Init(string itemName, string descr)
    {
        Name.text = itemName;
        Description.text = descr;
    }
}