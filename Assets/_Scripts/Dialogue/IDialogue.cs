using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogue
{
    public void OpenDialogue();
    public void NextDialogue(int pos);
    public void CloseDialogue();
}
