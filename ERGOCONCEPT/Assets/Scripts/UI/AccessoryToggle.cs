using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AccessoryToggle : Accessory
{
    public Text Label;
    public Text PriceHT_Label;
    public Text PriceTTC_Label;
    public Text Weight_Label;

    public List<GameObject> SettingList;
    public float PriceHT;
    public float PriceTTC;
    public float LPP;
    public float TVA;
    public float Weight;
    private SettingsManager settingsManager;
    private PopupManager popupManager;
    private Vector2 DefaultRectSize;
    private float targetHeight;
    private float currentHeight;
    private float lerpDuration = 0.3f;
    private float valueToLerp;
    private ArrayList exeptions;
    private bool isSeries;
    private string description;
    private bool canAnimate = true;

    public override void Init(List<GameObject> settingList, Hashtable accessoryInfo)
    {
        Reference = accessoryInfo["Ref"].ToString();
        Label.text = accessoryInfo["Name"].ToString();
        PriceHT = Parse.ToFloat(accessoryInfo["PriceHT"].ToString());
        PriceTTC = Parse.ToFloat(accessoryInfo["PriceTTC"].ToString());
        LPP = Parse.ToFloat(accessoryInfo["LPP"].ToString());
        TVA = Parse.ToFloat(accessoryInfo["TVA"].ToString());
        Weight = Parse.ToFloat(accessoryInfo["Weight"].ToString());
        exeptions = (ArrayList)accessoryInfo["Exeptions"];
        description = accessoryInfo["Description"].ToString();

        PriceHT_Label.text = "Prix HT : " + Parse.ConvertToString(PriceHT) + "€";
        PriceTTC_Label.text = "Prix TTC : " + Parse.ConvertToString(PriceTTC) + "€";
        Weight_Label.text = "Poids : " + Parse.ConvertToString(Weight) + "Kg";

        settingsManager = FindObjectOfType<SettingsManager>();
        popupManager = FindObjectOfType<PopupManager>();
        DefaultRectSize = GetComponent<RectTransform>().sizeDelta;
        SettingList = settingList;
        Toggle.isOn = false;
        DisplaySettings();
    }

    public void Init(List<GameObject> settingList, Hashtable accessoryInfo, bool isSeries)
    {
        Reference = accessoryInfo["Ref"].ToString();
        Label.text = accessoryInfo["Name"].ToString();
        exeptions = (ArrayList)accessoryInfo["Exeptions"];
        LPP = Parse.ToFloat(accessoryInfo["LPP"].ToString());
        PriceHT_Label.gameObject.SetActive(false);
        PriceTTC_Label.gameObject.SetActive(false);
        Weight_Label.gameObject.SetActive(false);

        settingsManager = FindObjectOfType<SettingsManager>();
        popupManager = FindObjectOfType<PopupManager>();
        DefaultRectSize = GetComponent<RectTransform>().sizeDelta;
        SettingList = settingList;
        Toggle.isOn = true;
        Toggle.transform.Find("Background").gameObject.SetActive(false);
        DisplaySettings();
        isSeries = true;
    }

    public void DisplayItemInfos()
    {
        popupManager.CreatPopupInfos(Label.text, description);
    }

    public override void Display3DObject()
    {

    }

    public override void SetItem()
    {
        base.SetItem();
        if (Toggle.isOn)
        {
            Debug.Log("Select Item ref : " + Reference+" "+ Label.text);
            CheckExeptions();
            string result = settingsManager.EditSelectedAccessoryList(Reference,false);
        }
        else
        {
            Debug.Log("Unselect Item ref : " + Reference + " " + Label.text);
            string result = settingsManager.EditSelectedAccessoryList(Reference, true);
            settingsManager.CheckAccessorySerieMustDisplay(exeptions);
        }
        ChangeAccessoryInfo();
    }

    public override void DisplaySettings()
    {
        base.DisplaySettings();
        float rectSize = 0;
        currentHeight = GetComponent<RectTransform>().sizeDelta.y;
        foreach (GameObject obj in SettingList)
        {
            obj.gameObject.SetActive(Toggle.isOn);
            float size = obj.GetComponent<RectTransform>().sizeDelta.y;
            rectSize += obj.GetComponent<RectTransform>().sizeDelta.y;
        }
        targetHeight = Toggle.isOn ? (DefaultRectSize.y + rectSize) : DefaultRectSize.y;

        if (!canAnimate)
            return;
        StartCoroutine(Lerp());
    }

    public void CheckExeptions()
    {
        if (Toggle.isOn)
        {
            settingsManager.DisableUnAuthorizedAccessories(exeptions);
        }
    }

    private void Update()
    {
        if (canAnimate)
        {
            if (valueToLerp != targetHeight)
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(DefaultRectSize.x, valueToLerp);
            }
            else
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(DefaultRectSize.x, targetHeight);
                canAnimate = true;
            }
        }
    }

    private IEnumerator Lerp()
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        valueToLerp = targetHeight;
    }

    public void ChangeAccessoryInfo()
    {
        settingsManager.ChangePriceHT(Toggle.isOn ? PriceHT : -PriceHT);
        settingsManager.ChangePriceTTC(Toggle.isOn ? PriceTTC : -PriceTTC);
        settingsManager.ChangePriceLPP(Toggle.isOn ? LPP : -LPP);
        settingsManager.ChangeWeight(Toggle.isOn ? Weight : -Weight);
    }

    public override void ChangeValue(Slider slider,string parameterTypes, int multiplicateur)
    {
        base.ChangeValue(slider, parameterTypes, multiplicateur);
    }
}