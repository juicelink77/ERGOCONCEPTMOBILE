using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject PopupInfoPrefab;
    public Transform Target;

    public void CreatPopupInfos(string itemName, string  descr)
    {
        GameObject pop = Instantiate(PopupInfoPrefab, Target);
        pop.GetComponent<PopupInfo>().Init(itemName, descr);
    }
}
