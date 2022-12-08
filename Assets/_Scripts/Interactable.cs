using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    public bool isInteracted = false;
    public GameObject hint;

    public virtual void Interact()
    {
        Debug.Log("Interact!");
    }

    public virtual void Hint(bool isHinted)
    {
        hint.SetActive(isHinted);
    }
}
