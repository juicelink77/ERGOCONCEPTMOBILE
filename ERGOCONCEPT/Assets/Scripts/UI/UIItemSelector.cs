using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSelector : MonoBehaviour
{
    public Text ItemNameField;
    private List<Button> buttonsList;

    private void OnEnable()
    {
        buttonsList = new List<Button>();
        foreach (Transform t in transform)
        {
            Button bt = t.GetComponent<Button>();
            bt.onClick.RemoveListener(() => OnSelectedItem(bt));
            bt.onClick.AddListener(()=>OnSelectedItem(bt));
            buttonsList.Add(bt);
        }
        OnSelectedItem(buttonsList[0]);
    }

    private void DisplayCheckedItem(Button bt, bool value)
    {
        bt.transform.Find("checked")?.gameObject.SetActive(value);
    }

    private void UnCheckedAllItems()
    {
        foreach (Button bt in buttonsList)
        {
            bt.transform.Find("checked")?.gameObject.SetActive(false);
        }
    }

    private void OnSelectedItem(Button bt)
    {
        ItemNameField.text = bt.name;
        UnCheckedAllItems();
        DisplayCheckedItem(bt, true);
    }
}