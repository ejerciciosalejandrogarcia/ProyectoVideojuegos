using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneEvent : MonoBehaviour
{
    [Header("Interacción")]
    public GameObject interactPanel;        // Panel "E para interactuar"
    public GameObject messagePanel;         // Panel para mostrar mensajes temporales
    public TextMeshProUGUI messageText;     // Texto de mensaje temporal

    [Header("Objetivo / Misiones")]
    public GameObject missionPanel;         // Panel de misión siempre visible
    public TextMeshProUGUI missionText;     // Texto de misión
    public int totalTrash = 3;              // Cantidad de basura a recolectar

    private int trashCollected = 0;
    private bool playerInRange = false;
    private bool hasInteracted = false;

    private void Start()
    {
        // Inicializamos paneles
        if (interactPanel != null)
            interactPanel.SetActive(false);

        if (messagePanel != null)
            messagePanel.SetActive(false);

        // Mission panel siempre visible
        if (missionPanel != null)
        {
            missionPanel.SetActive(true);
            UpdateMissionText();
        }
    }

    private void Update()
    {
        if (playerInRange && !hasInteracted)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hasInteracted = true;

                if (interactPanel != null)
                    interactPanel.SetActive(false);

                StartCoroutine(ShowTemporaryMessage("Volverá mamá, por favor tira la basura"));

                // Actualizar misión para mostrar progreso 0/3
                UpdateMissionText();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            // Mostrar panel de interacción solo si no ha interactuado
            if (!hasInteracted && interactPanel != null)
                interactPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // Ocultar panel de interacción al alejarse
            if (interactPanel != null)
                interactPanel.SetActive(false);
        }
    }

    private IEnumerator ShowTemporaryMessage(string text)
    {
        if (messagePanel != null && messageText != null)
        {
            messagePanel.SetActive(true);
            messageText.text = text;
        }

        // Tiempo que se muestra el mensaje
        yield return new WaitForSeconds(4f);

        if (messagePanel != null)
            messagePanel.SetActive(false);
    }

    // Llamar a este método cuando el jugador recolecta basura
    public void CollectTrash()
    {
        trashCollected++;
        UpdateMissionText();

        if (trashCollected >= totalTrash && missionText != null)
        {
            missionText.text = "Recolectar basura: Completado!";
        }
    }

  private void UpdateMissionText()
{
    if (missionText != null)
    {
        // Cambiamos el texto según la basura recolectada
        missionText.text = $"Recolectar basura: {trashCollected}/{totalTrash}";

        // Forzamos que TMP refresque
        missionText.ForceMeshUpdate();

        Debug.Log("Misión actualizada: " + missionText.text);
    }
    else
    {
        Debug.LogError("missionText no está asignado en el inspector!");
    }
}


}
