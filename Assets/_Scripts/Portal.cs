using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactable
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private BoxCollider2D boxCollider;

    public override void Interact()
    {
        if (!isInteracted)
        {
            Hint(false);
            isInteracted = true;
            animator.SetBool("ON", true);
            boxCollider.enabled = true;
        }
    }
}
