using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine.UI;
using UnityEngine;

public class SwitchMode : MonoBehaviour
{
    public GameObject configurator;
    public GameObject ARcontent;
    public GameObject chairContainerConfigurator;
    public GameObject chairContainerARcontent;
    public Image buttonAR;
    public Sprite fullscreen;
    public Sprite exitFullscreen;
    private GameObject chair = null;
    private GameObject chairCopy = null;
    public void OnClick()
    {

        if (chair != null) 
        { 
            if (configurator.activeSelf)
            {
                ARcontent.SetActive(true);
                chairCopy = Instantiate(chair, chairContainerARcontent.transform);
                configurator.SetActive(false);
                EnableAllRenderers(false);
               // buttonAR.sprite = exitFullscreen;
            }
            else
            {

                if(chairCopy != null)
                {
                    Destroy(chairCopy);
                }
                ARcontent.SetActive(false);
                configurator.SetActive(true);
                EnableAllRenderers(true);
               // buttonAR.sprite = fullscreen;
            }
        }
    }
    public void SetChair(GameObject ob)
    {
        chair = ob;
    }
    private void EnableAllRenderers(bool b)
    {
        Renderer[] allRenderers;
        allRenderers = chair.GetComponentsInChildren<Renderer>();
        foreach (Renderer childRenderer in allRenderers)
        {
            childRenderer.enabled = b;
        }
    }
}
