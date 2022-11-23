using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogue
{
    public void StartDialogue();
    public void NextDialogueLine();
    public IEnumerator ShowLine();
}
