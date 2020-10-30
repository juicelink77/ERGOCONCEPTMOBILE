using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GenerateFBOCamera : MonoBehaviour
{
    private int Width;
    private int Heigth;
    private static RenderTexture targetTexture = null;
    public RenderTexture TargetTexture { get { return targetTexture; } }

    private void Awake()
    {
        SetWindowedScreen();
    }

    private void CreatRenderTexture()
    {
        targetTexture = new RenderTexture(Width, Heigth, 0, RenderTextureFormat.ARGB32);
        targetTexture.depth = 24;
        GetComponent<Camera>().targetTexture = targetTexture;
    }

    public int GetWidth()
    {
        return Width;
    }

    public int GetHeight()
    {
        return Heigth;
    }

    public void SetFullScreen()
    {
#if WEB_GL || UNITY_EDITOR
        Width = 1920;
        Heigth = 960;
#else
        Width = 1920;
        Heigth = 960;
#endif
        CreatRenderTexture();
    }

    public void SetWindowedScreen()
    {
#if WEB_GL || UNITY_EDITOR
        Width = 650;
        Heigth = 600;
#else
        Width = 650;
        Heigth = 600;
#endif
        CreatRenderTexture();
    }
}