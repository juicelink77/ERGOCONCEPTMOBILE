using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.SceneManagement;

public static class Parse
{
    public static float ToFloat(string str)
    {
        return float.Parse(str);
    }

    public static string ConvertToString(float value)
    {
        return value.ToString("F2");
    }

    public static string ChangeCommaByDot(string str)
    {
        return str.Replace(',', '.');
    }
}

public class SettingsManager : MonoBehaviour
{
    public float modelSize = 1.2f;
    public Text ChairName;
    public Text TotalPriceHT;
    public Text TotalPriceTTC;
    public Text TotalWeight;
    public float TotalLPP;

    public GameObject AccessoryPrefab;
    public GameObject AccessorySeriesPrefab;
    public GameObject ItemsTitlePrefab;
    public GameObject AccessorySettingPrefab;
    public GameObject CategoryNamePrefab;
    public Transform AccessoryContener;
    public Transform ModelContener;
    public Button[] FoldButtons;

    private Hashtable packs;
    private int packIndex = 0;
    private List<string> packAllRef;
    private GameObject model3D;
    private Hashtable items;
    private Hashtable itemsSeries;
    private Hashtable currentInfos;
    private string currentReference;
    private float chairPriceHT;
    private float chairPriceTTC;
    private float chairPriceWeight;
    private ArrayList categories;
    private List<Accessory> AccessoryToggleList;
    private List<Accessory> AccessorySeriesList;
    public List<Accessory> AccessorySelected = new List<Accessory>();

    private bool plier = false;
    private DisplayAccessoryByRef[] DisplayAccessoryByRef;

    public enum SettingTypes
    {
        Toggle,
        Slider,
        Cran
    }
    
    public void LoadPack(Hashtable allSettings)
    {
        packAllRef = new List<string>();
        packs = (Hashtable)allSettings["Chairs"];
        items = (Hashtable)allSettings["Items"];
        categories = (ArrayList)allSettings["Categories"];
        itemsSeries = (Hashtable)allSettings["ItemsSeries"];

        foreach (DictionaryEntry entry in packs)
        {
            Hashtable inf = (Hashtable)entry.Value;
            packAllRef.Add(inf["Ref"].ToString());
        }

        currentInfos = (Hashtable)packs[packAllRef[packIndex]];

        ChairName.text = currentInfos["Name"].ToString();
        chairPriceHT = Parse.ToFloat(currentInfos["PriceHT"].ToString());
        chairPriceTTC = Parse.ToFloat(currentInfos["PriceTTC"].ToString());
        chairPriceWeight = Parse.ToFloat(currentInfos["Weight"].ToString());

        currentReference = packAllRef[packIndex];
        GameObject targetFile = Resources.Load("Prefabs/Chairs/" + currentReference) as GameObject;
        model3D = Instantiate(targetFile, ModelContener);
        model3D.layer = 8;
        //////FOLD BUTTON
        foreach(Button bt in FoldButtons)
        {
            bt.onClick.AddListener(FoldChair);

        }
        //////SET CHAIR IN VIEW SWITCHER ////
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "DisplayModel_Mobile")
        {
           GameObject.Find("Switcher").SendMessage("SetChair", model3D);
        }

        DisplayMenuAccessories();
        DisplayPriceHT();
        DisplayPriceTTC();
        DisplayWeight();

        DisplayAccessoryByRef = FindObjectsOfType<DisplayAccessoryByRef>();
    }

    private void FoldChair()
    {
        Animator animator = GameObject.Find("bloc_fauteuil").GetComponentInChildren<Animator>();
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            return;
        }
        if (!plier)
        {
            animator.Play("pliage");
            plier = true;
        }
        else
        {
            animator.Play("depliage");
            plier = false;
        }
    }

    private void DisplayPriceHT()
    {
        TotalPriceHT.text = Parse.ConvertToString(chairPriceHT) + "€";
    }

    private void DisplayPriceTTC()
    {
        TotalPriceTTC.text = "" + Parse.ConvertToString(chairPriceTTC) + "€";
    }

    public string GetPriceTTC()
    {
        return TotalPriceTTC.text;
    }

    public string GetPriceLPP()
    {
        return TotalLPP +"€";
    }

    private void DisplayWeight()
    {
        TotalWeight.text = "" + Parse.ConvertToString(chairPriceWeight) + "Kg";
    }

    public void ChangePriceHT(float value)
    {
        chairPriceHT += value;
        DisplayPriceHT();
    }

    public void ChangePriceTTC(float value)
    {
        chairPriceTTC += value;
        DisplayPriceTTC();
    }

    public void ChangePriceLPP(float value)
    {
        TotalLPP += value;
    }

    public void ChangeWeight(float value)
    {
        chairPriceWeight += value;
        DisplayWeight();
    }


    private Hashtable GetItemByRef(string refe)
    {
        return items[refe] != null ? (Hashtable)items[refe]:new Hashtable();
    }

    private Hashtable GetSettingByRef(string refe)
    {
        return (Hashtable)GetItemByRef(refe)["Settings"];
    }

    private ArrayList GetAccessories()
    {
        return (ArrayList)currentInfos["ItemsRef"];
    }

    private AccessoryToggle GetAccessoryToggleByRef(string reference)
    {
        foreach(AccessoryToggle accessory in AccessoryToggleList)
        {
            if(reference == accessory.Reference)
            {
                return accessory;
            }
        }
        return null;
    }

    private AccessorySeries GetAccessorySeriesByRef(string reference)
    {
        foreach (AccessorySeries accessory in AccessorySeriesList)
        {
            if (reference == accessory.Reference)
            {
                return accessory;
            }
        }
        return null;
    }

    public void DisableUnAuthorizedAccessories(ArrayList referencesNotAuthorized)
    {
        if (referencesNotAuthorized == null)
            return;

        for (int i = 0; i< referencesNotAuthorized.Count; i++)
        {
            string badRef = referencesNotAuthorized[i].ToString();
            AccessoryToggle badAccessory = GetAccessoryToggleByRef(badRef);
            AccessorySeries badAccessorySeries = GetAccessorySeriesByRef(badRef);
            badAccessory?.ForceToDisableToggle(true);
            badAccessorySeries?.ForceToDisableToggle(true);
        }
    }

    private void InstantiateAccessoriesAndSettings(Hashtable accessoryInfo, bool series = false)
    {
        GameObject accessory;
        Accessory accessoryScript;
        if (!series)
        {
            accessory = Instantiate(AccessoryPrefab, AccessoryContener);
            accessoryScript = accessory.GetComponent<AccessoryToggle>();
            AccessoryToggleList.Add(accessoryScript);
        }
        else
        {
            accessory = Instantiate(AccessorySeriesPrefab, AccessoryContener);
            accessoryScript = accessory.GetComponent<AccessorySeries>();
            AccessorySeriesList.Add(accessoryScript);
        }

        Hashtable accessorySettings = (Hashtable)accessoryInfo["Settings"];
        List<GameObject> listSettingObj = new List<GameObject>();
        foreach (DictionaryEntry entry in accessorySettings)
        {
            GameObject accessorySetting = Instantiate(AccessorySettingPrefab, accessory.transform.Find("params").transform);
            AccessorySetting accessorySettingScript = accessorySetting.GetComponent<AccessorySetting>();
            accessorySettingScript.SetSettingType((Hashtable)entry.Value, entry.Key.ToString(), accessoryScript);
            listSettingObj.Add(accessorySetting);
        }
        accessoryScript.Init(listSettingObj, accessoryInfo);
    }

    public void CheckAccessorySerieMustDisplay(ArrayList referencesNotAuthorized)
    {
        bool canCheckSeries = true;
        for (int i = 0; i < referencesNotAuthorized.Count; i++)
        {
            string badRef = referencesNotAuthorized[i].ToString();
            AccessoryToggle badAccessory = GetAccessoryToggleByRef(badRef);
            if (badAccessory != null)
            {
                if (badAccessory.IsOn)
                {
                    canCheckSeries = false;
                }
            }
        }

        if (canCheckSeries)
        {
            for (int i = 0; i < referencesNotAuthorized.Count; i++)
            {
                string badRef = referencesNotAuthorized[i].ToString();
                AccessorySeries accessorySerieToDisplay = GetAccessorySeriesByRef(badRef);
                Debug.Log("accessorySerieToDisplay = " + accessorySerieToDisplay);

                if (accessorySerieToDisplay != null)
                {
                    if (!accessorySerieToDisplay.IsOn)
                    {
                        accessorySerieToDisplay.ForceToEnableToggle(true);
                    }
                }
            }
        }
    }

    public void DisplayMenuAccessories()
    {
        GameObject TitleSeries = Instantiate(ItemsTitlePrefab, AccessoryContener);
        AccessorySeriesList = new List<Accessory>();

        foreach (DictionaryEntry item in itemsSeries)
        {
            Hashtable listItemSameRefHash = (Hashtable)item.Value;
            ArrayList listItemSameRef = (ArrayList)listItemSameRefHash["List"];
            for (int j = 0; j < listItemSameRef.Count; j++)
            {
                Hashtable accessoryInfo = (Hashtable)listItemSameRef[j];
                InstantiateAccessoriesAndSettings(accessoryInfo, true);
            }
        }

        TitleSeries.GetComponent<ItemsBarDisplay>().Init(true, AccessorySeriesList, "Réglages de série");

        GameObject TitleItems = Instantiate(ItemsTitlePrefab, AccessoryContener);
        AccessoryToggleList = new List<Accessory>();

        List<GameObject> categoryNameList = new List<GameObject>();
        for (int i = 0; i< categories.Count; i++)
        {
            string cat = categories[i].ToString();
            GameObject CategoryNameObject = Instantiate(CategoryNamePrefab, AccessoryContener);
            CategoryNameObject.GetComponentInChildren<Text>().text = cat;
            categoryNameList.Add(CategoryNameObject);

            foreach (DictionaryEntry item in items)
            {
                Hashtable listItemSameRefHash = (Hashtable)item.Value;
                ArrayList listItemSameRef = (ArrayList)listItemSameRefHash["List"];
                for (int j = 0; j< listItemSameRef.Count; j++)
                {
                    Hashtable accessoryInfo = (Hashtable)listItemSameRef[j];
                    string itemCatego = accessoryInfo["Category"].ToString();
                    if (itemCatego == cat)
                    {
                        InstantiateAccessoriesAndSettings(accessoryInfo);
                    }
                }
            }
        }
        TitleItems.GetComponent<ItemsBarDisplay>().Init(true, AccessoryToggleList, "Accessoires", categoryNameList);
    }


    public string EditSelectedAccessoryList(string refe, bool remove)
    {

        foreach (DisplayAccessoryByRef d in DisplayAccessoryByRef)
        {
            if (d.Reference == refe)
                d.DisplayAccessory(!remove);
        }

        foreach (AccessoryToggle accessory in AccessorySelected)
        {
            if (refe == accessory.Reference)
            {
                if (remove)
                {
                    Debug.Log(refe+" Item deleted");
                    AccessorySelected.Remove(GetAccessoryToggleByRef(refe));
                    return "deleted";
                }
                else
                {
                    Debug.Log(refe+" Allready exist");
                    return "allreadyExist";
                }
            }
        }
        if (!remove)
        {
            AccessorySelected.Add(GetAccessoryToggleByRef(refe));
            Debug.Log(refe + " item added");
            return "item added";
        }
        else
        {
            return null;
        }
        
    }
}