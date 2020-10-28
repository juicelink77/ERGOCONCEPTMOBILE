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
        GenerateFBOCamera GenerateFBOCamera = Camera3D.GetComponent<GenerateFBOCamera>();
        Image.rectTransform.sizeDelta = new Vector2(GenerateFBOCamera.Width, GenerateFBOCamera.Heigth);
        Image.GetComponent<BoxCollider2D>().size = Image.GetComponent<RectTransform>().sizeDelta;
        Image.GetComponent<BoxCollider2D>().offset = new Vector2(Image.GetComponent<RectTransform>().sizeDelta.x / 2, -Image.GetComponent<RectTransform>().sizeDelta.y / 2);
    }

    private void Update()
    {
        Image.texture = Camera3D.targetTexture;
    }
}