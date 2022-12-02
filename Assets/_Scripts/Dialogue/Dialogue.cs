using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "new Dialogue", menuName = "Assets/Dialogue")]
public class Dialogue : ScriptableObject
{
    public Message[] messages;
}

[System.Serializable]
public class Message
{
    [TextArea(2, 6)]
    public string text;
}
