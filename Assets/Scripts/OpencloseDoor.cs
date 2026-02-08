using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpencloseDoor : MonoBehaviour
{
    public Animator animator;
    private bool playerInside = false;
    private bool isOpen = false;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpen)
            {
                animator.SetTrigger("Open");
                isOpen = true;
            }
            else
            {
                animator.SetTrigger("Close");
                isOpen = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}
