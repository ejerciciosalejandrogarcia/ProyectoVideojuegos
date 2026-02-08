using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    [TextArea(3, 6)]
    public string dialog;

    bool playerNear;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            Debug.Log("Player cerca del NPC");

            // Mostrar mensaje de interacción
            DialogManager.Instance.ShowInteractText();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            Debug.Log("Player se fue del NPC");

            // Ocultar diálogo y mensaje de interacción
            DialogManager.Instance.HideDialog();
            DialogManager.Instance.HideInteractText();
        }
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E presionada, mostrando diálogo");
            DialogManager.Instance.ShowDialog(dialog);

            // Opcional: ocultar el mensaje de "Presiona E" mientras se muestra el diálogo
            DialogManager.Instance.HideInteractText();
        }
    }

}
