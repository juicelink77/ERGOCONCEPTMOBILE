using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairSelection : MonoBehaviour
{
    public GameObject ChairSelectionPrefab;
    public SceneLoader SceneLoader;
    public Transform Container;
    private Hashtable chairs;


    public void LoadPack(Hashtable allSettings)
    {
        chairs = (Hashtable)allSettings["Chairs"];
        foreach (DictionaryEntry chair in chairs)
        {
            GameObject ch = Instantiate(ChairSelectionPrefab, Container);
            Hashtable infos = (Hashtable)chair.Value;
            ch.GetComponent<ChairSelectionButton>().Init(infos["Name"].ToString(),chair.Key.ToString(), GotoEditor);
        }
    }
    private void GotoEditor()
    {
        SceneLoader.GotoScene("DisplayModel_Mobile");
    }
}