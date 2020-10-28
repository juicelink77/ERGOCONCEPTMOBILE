using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Popup : MonoBehaviour
{
    public GameObject PopupContaint;
    public Image[] Backgrounds;
    private float transitionTime = 0.75f;
    private float backgroundAlpha = 0.35f;
    private float alphaSpeed = 0.05f;

    public void ClosePopup(Button bt)
    {
        bt.interactable = false;
        StartCoroutine(CloseAndDestroy());
    }

    private void Start()
    {
        Hashtable param = new Hashtable();
        param["time"] = transitionTime;
        param["scale"] = Vector3.one;
        PopupContaint.transform.localScale = Vector3.zero;
        iTween.ScaleTo(PopupContaint, param);
        StartCoroutine(FadeIn());
    }

    private void ForceAlpha(float alpha)
    {
        foreach (Image b in Backgrounds)
        {
            Color c = b.color;
            b.color = new Color(c.r, c.g, c.b, alpha);
        }
    }

    private IEnumerator FadeOut()
    {
        ForceAlpha(backgroundAlpha);
        float alphaValue = backgroundAlpha;
        while (alphaValue > 0)
        {
            foreach (Image b in Backgrounds)
            {
                Color c = b.color;
                b.color = new Color(c.r, c.g, c.b, alphaValue);
                if(b.material != null)
                {
                    b.material = null;
                }
            }
            alphaValue -= alphaSpeed;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator FadeIn()
    {
        ForceAlpha(0);
        float alphaValue = 0;
        while (alphaValue < backgroundAlpha)
        {
            foreach (Image b in Backgrounds)
            {
                Color c = b.color;
                b.color = new Color(c.r, c.g, c.b, alphaValue);
            }
            alphaValue += alphaSpeed;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CloseAndDestroy()
    {
        Hashtable param = new Hashtable();
        param["time"] = transitionTime;
        param["scale"] = Vector3.zero;
        iTween.ScaleTo(PopupContaint, param);
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(transitionTime);
        Destroy(gameObject);
    }
}