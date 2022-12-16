using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Campfire : Interactable
{
    public Transform spawnPoint;

    [SerializeField]
    Animator animator;

    public override void Interact()
    {
        if (!isInteracted)
        {
            Hint(false);
            animator.SetTrigger("interact");
            isInteracted = true;
            if (spawnPoint != null)
            {
                GameManager.instance.checkpoint = spawnPoint.position;
            }
        }
    }
}
