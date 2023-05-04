using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [TextArea(5, 5)]
    public string[] dialogs;
    public Sprite[] image;
    public AudioClip[] sounds;
}
