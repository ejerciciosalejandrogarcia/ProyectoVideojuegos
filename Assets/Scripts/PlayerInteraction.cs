using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    NPCDialog currentNPC;
    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentNPC != null)
        {
            currentNPC.Talk();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DialogManager.Instance.HideDialog();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        NPCDialog npc = other.GetComponent<NPCDialog>();
        if (npc != null)
            currentNPC = npc;
    }

    void OnTriggerExit(Collider other)
    {
        NPCDialog npc = other.GetComponent<NPCDialog>();
        if (npc == currentNPC)
            currentNPC = null;
    }*/



}
