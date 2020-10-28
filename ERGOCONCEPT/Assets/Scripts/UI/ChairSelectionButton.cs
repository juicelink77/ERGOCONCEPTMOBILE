using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChairSelectionButton : MonoBehaviour
{
    public Image picture;
    public Text Name;
    public Button Button;

    public void Init(string name, string reference, UnityAction onClick)
    {
        Name.text = name;
        Button.onClick.AddListener(onClick);
        Sprite pict = Resources.Load<Sprite>("Pictures/" + reference);
       /* Sprite sp = new Sprite();
        sp.texture = pict;*/
        picture.sprite = pict;
    }
}