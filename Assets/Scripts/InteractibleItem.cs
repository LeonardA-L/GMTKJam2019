using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Interactible Item")]
public class InteractibleItem : ScriptableObject
{
    public Sprite m_2DSprite = null;
    [TextArea(10,100)]
    public string m_text = null;
    public TMPro.TMP_FontAsset m_font = null;
    public string m_switch = null;
}
