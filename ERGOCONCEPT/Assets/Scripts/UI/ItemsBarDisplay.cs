using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsBarDisplay : MonoBehaviour
{
    public GameObject ArrowOpen;
    public GameObject ArrowClose;
    public Text Title;
    public List<Accessory> Accessories;

    private List<GameObject> OptionalList;
    private bool isOpen;

    public void Init(bool open, List<Accessory> accessories, string title, List<GameObject> optionalList = null)
    {
        Title.text = title;
        isOpen = open;
        Accessories = accessories;
        OptionalList = optionalList;
        OnChangeState();
    }

    public void OnCLicked()
    {
        isOpen = !isOpen;
        OnChangeState();
    }

    private void OnChangeState()
    {
        foreach (Accessory accessory in Accessories)
        {
            accessory.gameObject.SetActive(isOpen);
        }
        if(OptionalList != null)
        {
            foreach (GameObject obj in OptionalList)
            {
                obj.gameObject.SetActive(isOpen);
            }
        }
        ArrowOpen.SetActive(isOpen);
        ArrowClose.SetActive(!isOpen);
    }
}