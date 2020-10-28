using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewResizer : MonoBehaviour
{
    public Camera cam;
#if UNITY_EDITOR

         private int resolutionX;
         private int resolutionY;
  
         private void Awake()
         {
              resolutionX = Screen.width;
              resolutionY = Screen.height;
          }
  
          private void Update ()
          {
              if (resolutionX == Screen.width && resolutionY == Screen.height) return;
              resolutionX = Screen.width;
              resolutionY = Screen.height;
              CalcAspect();
          }
  
#endif //UNITY_EDITOR
    void Start()
    {
        CalcAspect();
    }
    private void CalcAspect()
    {
        RectTransform rect = GetComponent<RectTransform>();
        float r = cam.aspect;
        string _r = r.ToString("F2");
        string ratio = _r.Substring(0, 4);
        switch (ratio)
        {
            case "0,75": //3:4
                rect.sizeDelta = new Vector2(1000, 615);
                break;
            case "0,67": //2:3
                rect.sizeDelta = new Vector2(1000, 680);
                break;
            case "0,63": //10:16
                rect.sizeDelta = new Vector2(1000, 772);
                break;
            case "0,56": //9:16
                rect.sizeDelta = new Vector2(1000, 869);
                break;
            case "0,50": //9:18
                rect.sizeDelta = new Vector2(1000, 1005);
                break;
        }
    }

}
