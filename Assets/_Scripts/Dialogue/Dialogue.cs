using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "new Dialogue", menuName = "Assets/Dialogue")]
public class Dialogue : ScriptableObject
{
    public int npcID;
    public string npcName;
    public Message[] messages;
}

[System.Serializable]
public class Message
{
    [TextArea(2, 6)]
    public string text;
    public Response[] responses;
}

[System.Serializable]
public class Response
{
    public int next;

    [TextArea(2, 6)]
    public string reply;
    public string prereq;
    public string trigger;
}
