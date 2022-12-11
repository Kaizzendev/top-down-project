using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : Interactable, IDialogue
{
    [SerializeField]
    private Dialogue dialogue;

    public TMP_Text text;
    public Canvas canvas;
    private int pos = 0;
    public bool hasInteracted = false;

    public override void Interact()
    {
        if (pos != dialogue.messages.Length)
        {
            Hint(false);
            OpenDialogue();
        }
        else
        {
            CloseDialogue();
            pos = 0;

            Hint(true);
        }
    }

    public void NextDialogue(int pos)
    {
        text.text = dialogue.messages[pos].text;
    }

    public void OpenDialogue()
    {
        hasInteracted = true;
        NextDialogue(pos);
        pos++;
        canvas.gameObject.SetActive(true);
    }

    public void CloseDialogue()
    {
        canvas.gameObject.SetActive(false);
    }
}
