using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupWithJson : MonoBehaviour
{
    private string jsonString = "";
    public Hashtable AllSettings;
    public SettingsManager SettingsManager;
    public ChairSelection ChairSelection;

    public void LoadJson()
    {
        jsonString = LoadResourceTextfile("ErgoSetting");
        object anObject = Procurios.Public.JSON.JsonDecode(jsonString);
        AllSettings = (Hashtable)anObject;
        SettingsManager?.LoadPack(AllSettings);
        ChairSelection?.LoadPack(AllSettings);
    }

    public static string LoadResourceTextfile(string path)
    {
        TextAsset targetFile = Resources.Load<TextAsset>(path);
        return targetFile.text;
    }

    private void Start()
    {
        LoadJson();
    }
}