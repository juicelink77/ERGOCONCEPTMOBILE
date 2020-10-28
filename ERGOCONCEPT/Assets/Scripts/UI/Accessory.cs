using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accessory : MonoBehaviour
{
    public Toggle Toggle;
    public string Reference;
    public bool IsOn;
    public GameObject ObjectModel3D;


    private void Start()
    {
        ObjectModel3D = GameObject.Find(Reference);
    }

    public virtual void Display3DObject()
    {
        if (ObjectModel3D != null)
            ObjectModel3D.SetActive(IsOn);
    }

    public virtual void Init(List<GameObject> settingList, Hashtable accessoryInfo)
    {

    }

    public virtual void SetItem()
    {
        DisplaySettings();
    }

    public virtual void DisplaySettings()
    {
        IsOn = Toggle.isOn;
        Display3DObject();
    }

    public void ForceToDisableToggle(bool isException)
    {
        if (Toggle.isOn)
        {
            Toggle.isOn = false;
            /*if (isException)
            {
                Debug.Log("Exception item ref : " + Reference + " " + Label.text);
            }*/
        }
    }

    public void ForceToEnableToggle(bool isException)
    {
        if (!Toggle.isOn)
        {
            Toggle.isOn = true;
        }
    }

    public virtual void ChangeValue(Slider slider, string parameterTypes, int multiplicateur)
    {
        GameObject test = GameObject.Find(Reference);
        Debug.Log(parameterTypes);
        if (test != null)
        { 
            if (parameterTypes == "Inclinaison")
            {
                test.transform.localEulerAngles = new Vector3(test.transform.localEulerAngles.x, (float)(slider.value* multiplicateur), test.transform.localEulerAngles.z);
            }
            else if(parameterTypes == "Hauteur")
            {
                Transform H = GameObject.Find("H_" + Reference).transform;
                H.localPosition = new Vector3(H.localPosition.x, (float)(slider.value * multiplicateur), H.localPosition.z);
            }
            else
            {
            } 
        }
    }
}