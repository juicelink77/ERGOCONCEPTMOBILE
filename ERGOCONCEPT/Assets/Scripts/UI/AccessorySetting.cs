using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessorySetting : MonoBehaviour
{
    public Slider Slider;
    public GameObject SliderGroup;
    public GameObject ToogleGroupe;
    public GameObject CranGroupe;
    private Accessory MyParent;
    private string parameterName;

    public void SetSettingType(Hashtable settingInfo, string settingName, Accessory accessory)
    {
        parameterName = settingName;
        SettingsManager.SettingTypes settingType = (SettingsManager.SettingTypes)System.Enum.Parse(typeof(SettingsManager.SettingTypes), settingInfo["Type"].ToString());
        SliderGroup.SetActive(false);
        ToogleGroupe.SetActive(false);
        CranGroupe.SetActive(false);
        MyParent = accessory;
        if (settingType == SettingsManager.SettingTypes.Slider || settingType == SettingsManager.SettingTypes.Cran)
        {
            Slider Slider = null;
            ArrayList values = (ArrayList)settingInfo["Values"];
            if(settingType == SettingsManager.SettingTypes.Slider)
            {
                SliderGroup.SetActive(true);
                SliderGroup.transform.Find("Label").GetComponent<Text>().text = settingName;
                Slider = SliderGroup.transform.Find("Slider").GetComponent<Slider>();
            }
            else
            {
                CranGroupe.SetActive(true);
                CranGroupe.transform.Find("Label").GetComponent<Text>().text = settingName;
                Slider = CranGroupe.transform.Find("Slider").GetComponent<Slider>();
            }
            Slider.minValue = int.Parse(values[0].ToString());
            Slider.maxValue = int.Parse(values[1].ToString());
            int multiplicateur = 1;
            if(values.Count > 2)
            {
                multiplicateur = int.Parse(values[2].ToString());
            }
            
            Slider.onValueChanged.AddListener(delegate { OnChangeValue(Slider, multiplicateur); });
        }
        else if (settingType == SettingsManager.SettingTypes.Toggle)
        {
            ArrayList values = (ArrayList)settingInfo["Values"];
            ToogleGroupe.SetActive(true);
            ToogleGroupe.transform.Find("ToggleA").transform.Find("Label").GetComponent<Text>().text = values[0].ToString();
            ToogleGroupe.transform.Find("ToggleB").transform.Find("Label").GetComponent<Text>().text = values[1].ToString();
        }
    }

    public void OnChangeValue(Slider s, int multiplicateur)
    {
        MyParent.ChangeValue(s, parameterName, multiplicateur);
    }
}