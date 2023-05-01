using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Data/NewSpeaker")]
[System.Serializable]
public class Speaker : ScriptableObject
{
    [SerializeField] private string speakerName;
    [SerializeField] private Color textColor;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private SpriteController prefab;
    public string SpeakerName => speakerName;

    public Color TextColor => textColor;

    public List<Sprite> Sprites => sprites;

    public SpriteController Prefab => prefab;
}
