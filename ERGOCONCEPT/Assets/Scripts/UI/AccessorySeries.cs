using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AccessorySeries : Accessory
{
    public Text Label;
    public List<GameObject> SettingList;
    private SettingsManager settingsManager;
    private ArrayList exeptions;

    public override void Init(List<GameObject> settingList, Hashtable accessoryInfo)
    {
        Reference = accessoryInfo["Ref"].ToString();
        Label.text = accessoryInfo["Name"].ToString();
        exeptions = (ArrayList)accessoryInfo["Exeptions"];

        settingsManager = FindObjectOfType<SettingsManager>();
        SettingList = settingList;

        float rectSize = 0;
        Vector2 currentSize = GetComponent<RectTransform>().sizeDelta;
        foreach (GameObject obj in SettingList)
        {
            float size = obj.GetComponent<RectTransform>().sizeDelta.y;
            rectSize += obj.GetComponent<RectTransform>().sizeDelta.y;
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2(currentSize.x, currentSize.y + rectSize);
    }

    public override void ChangeValue(Slider slider, string parameterTypes, float multiplicateur, string axe)
    {
        base.ChangeValue(slider, parameterTypes, multiplicateur, axe);
    }
}