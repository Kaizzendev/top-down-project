using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : Interactable
{
    public Transform spawnPoint;

    [SerializeField]
    Animator animator;
    private bool interacted = false;

    public override void Interact()
    {
        if (!interacted)
        {
            animator.SetTrigger("interact");
            interacted = true;
            if (spawnPoint != null)
            {
                GameManager.instance.checkpoint = spawnPoint.position;
            }
        }
    }
}
