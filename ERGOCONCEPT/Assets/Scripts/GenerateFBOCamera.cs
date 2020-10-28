using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GenerateFBOCamera : MonoBehaviour
{
    public int Width = 500;
    public int Heigth = 500;
    private static RenderTexture targetTexture = null;
    //private Vector2 size = Vector2.one;
    public RenderTexture TargetTexture { get { return targetTexture; } }

    private void Awake()
    {
            if (targetTexture == null)
            {
            targetTexture = new RenderTexture(Width, Heigth, 0, RenderTextureFormat.ARGB32);
            targetTexture.depth = 24;
/*
#if UNITY_EDITOR
            targetTexture = new RenderTexture(1080, 1920, 0, RenderTextureFormat.ARGB32);
#else
                targetTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
#endif*/
            }
            GetComponent<Camera>().targetTexture = targetTexture;
        }
}