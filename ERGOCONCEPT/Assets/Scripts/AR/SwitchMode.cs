using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
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
    public async void OnClick()
    {

        if (chair != null) 
        { 
            if (configurator.activeSelf)
            {
                ARcontent.SetActive(true);
                chairCopy = Instantiate(chair, chairContainerARcontent.transform);

                DisplayAccessoryByRef[] ListOrigin = chair.GetComponentsInChildren<DisplayAccessoryByRef>();
                DisplayAccessoryByRef[] ListClone = chairCopy.GetComponentsInChildren<DisplayAccessoryByRef>();

                foreach(DisplayAccessoryByRef displayAccessoryOrigin in ListOrigin)
                {
                    foreach (DisplayAccessoryByRef displayAccessoryClone in ListClone)
                    {
                        if(displayAccessoryOrigin.Reference == displayAccessoryClone.Reference)
                        {
                            displayAccessoryClone.RefreshAccessory(displayAccessoryOrigin.IsDisplay);
                        }
                    }
                }

                configurator.SetActive(false);
                EnableAllRenderers(false);
                GameObject videoBackground = await disableShadowOnVideoBackground();
                videoBackground.GetComponent<Renderer>().receiveShadows = false;
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
    private async Task<GameObject> disableShadowOnVideoBackground()
    {
        while (!GameObject.Find("BackgroundPlane"))
        {
            await Task.Delay(1000 / 30);
        }
        return GameObject.Find("BackgroundPlane");
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
