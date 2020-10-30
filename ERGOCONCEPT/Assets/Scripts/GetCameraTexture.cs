using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCameraTexture : MonoBehaviour
{
    public Camera Camera3D;
    public RawImage Image;

    private GenerateFBOCamera GenerateFBOCamera;

    private void Start()
    {
        ReGenerateFBOCamera(Image);
    }

    public void ReGenerateFBOCamera(RawImage img)
    {
        GenerateFBOCamera GenerateFBOCamera = Camera3D.GetComponent<GenerateFBOCamera>();
        img.rectTransform.sizeDelta = new Vector2(GenerateFBOCamera.GetWidth(), GenerateFBOCamera.GetHeight());
        BoxCollider2D boxCollider = img.GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            img.GetComponent<BoxCollider2D>().size = img.GetComponent<RectTransform>().sizeDelta;
            img.GetComponent<BoxCollider2D>().offset = new Vector2(img.GetComponent<RectTransform>().sizeDelta.x / 2, -img.GetComponent<RectTransform>().sizeDelta.y / 2);
        }
    }

    private void Update()
    {
        Image.texture = Camera3D.targetTexture;
    }
}