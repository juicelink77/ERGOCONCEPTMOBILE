using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
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
        }
    }

    public void ForceToEnableToggle(bool isException)
    {
        if (!Toggle.isOn)
        {
            Toggle.isOn = true;
        }
    }
    public virtual void ChangeValue(Slider slider, string parameterTypes, float multiplicateur, string axe)
    {
        Debug.Log(Reference + "_" + parameterTypes);
        float v = (float)(slider.value * multiplicateur);
        string vector = axe;
        GameObject ob = GameObject.Find(Reference+"_"+ parameterTypes);
        if(ob != null) { 
        StoreTransform initialTransform = ob.GetComponent<StoreTransform>();
            switch (parameterTypes)
            {
                case "Inclinaison":
                    switch (vector)
                    {
                        case "x":
                            ob.transform.localEulerAngles = new Vector3(initialTransform.initialRot.x + v, ob.transform.localEulerAngles.y, ob.transform.localEulerAngles.z);
                            break;
                        case "y":
                            ob.transform.localEulerAngles = new Vector3(ob.transform.localEulerAngles.x, initialTransform.initialRot.y + v, ob.transform.localEulerAngles.z);
                            break;
                        case "z":
                            ob.transform.localEulerAngles = new Vector3(ob.transform.localEulerAngles.x, ob.transform.localEulerAngles.y, initialTransform.initialRot.z + v);
                            break;
                    }
                    break;
                case "Hauteur":
                    switch (vector)
                    {
                        case "x":
                            ob.transform.localPosition = new Vector3(initialTransform.initialPos.x + v, ob.transform.localPosition.y, ob.transform.localPosition.z);
                            break;
                        case "y":
                            ob.transform.localPosition = new Vector3(ob.transform.localPosition.x, initialTransform.initialPos.y + v, ob.transform.localPosition.z);
                            break;
                        case "z":
                            ob.transform.localPosition = new Vector3(ob.transform.localPosition.x, ob.transform.localPosition.y, initialTransform.initialPos.z + v);
                            break;
                    }
                    break;
                case "Profondeur":
                    switch (vector)
                    {
                        case "x":
                            ob.transform.localPosition = new Vector3(initialTransform.initialPos.x + v, ob.transform.localPosition.y, ob.transform.localPosition.z);
                            break;
                        case "y":
                            ob.transform.localPosition = new Vector3(ob.transform.localPosition.x, initialTransform.initialPos.y + v, ob.transform.localPosition.z);
                            break;
                        case "z":
                            ob.transform.localPosition = new Vector3(ob.transform.localPosition.x, ob.transform.localPosition.y, initialTransform.initialPos.z + v);
                            break;
                    }
                    break;
                case "Emplacement":
                    switch (vector)
                    {
                        case "x":
                            ob.transform.localPosition = new Vector3(initialTransform.initialPos.x + v, ob.transform.localPosition.y, ob.transform.localPosition.z);
                            break;
                        case "y":
                            ob.transform.localPosition = new Vector3(ob.transform.localPosition.x, initialTransform.initialPos.y + v, ob.transform.localPosition.z);
                            break;
                        case "z":
                            ob.transform.localPosition = new Vector3(ob.transform.localPosition.x, ob.transform.localPosition.y, initialTransform.initialPos.z + v);
                            break;
                    }
                    break;
            }
        }
    }
}