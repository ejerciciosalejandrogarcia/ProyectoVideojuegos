using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrushCount : MonoBehaviour
{
    [Header("Panel de Misiones")]
    public GameObject missionPanel;         // Asigna el panel de misiones
    public TextMeshProUGUI missionText;     // Asigna el texto de misiones

    [Header("Configuración")]
    public int totalTrash = 3;              // Total de basura a recolectar
    public int trashValue = 1;              // Cantidad que vale este objeto

    private static int trashCollected = 0;  // STATIC para compartir entre todas las basuras

    private void Start()
    {
        // Buscar automáticamente si no están asignados
        if (missionPanel == null)
        {
            missionPanel = GameObject.FindGameObjectWithTag("MissionPanel");
        }

        if (missionText == null && missionPanel != null)
        {
            missionText = missionPanel.GetComponentInChildren<TextMeshProUGUI>();
        }

        // Inicializar el texto de misión
        UpdateMissionText();
    }

    // Método llamado cuando se hace clic en el objeto
    private void OnMouseDown()
    {
        CollectTrash();
    }

    private void CollectTrash()
    {
        // Incrementar el contador global
        trashCollected += trashValue;

        Debug.Log($"Basura recolectada: {trashCollected}/{totalTrash} - Objeto: {gameObject.name}");

        // Actualizar el panel de misiones
        UpdateMissionText();

        // Mostrar mensaje cuando se complete
        if (trashCollected >= totalTrash)
        {
            // Cambiar el texto del panel de misiones
            if (missionText != null)
            {
                missionText.text = "Tíralo al basurero de la ciudad";
                Debug.Log("Misión actualizada: Tíralo al basurero de la ciudad");
            }


        }

        // Destruir este objeto de basura
        Destroy(gameObject);
    }

    private void UpdateMissionText()
    {
        if (missionText != null)
        {
            if (trashCollected < totalTrash)
            {
                missionText.text = $"Recolectar basura: {trashCollected}/{totalTrash}";
            }
            // Nota: El texto "Tíralo al basurero" se pone en CollectTrash()
        }
        else
        {
            Debug.LogWarning("missionText no asignado. No se puede actualizar el panel de misiones.");
        }
    }

    // Método para resetear si es necesario (opcional)
    public static void ResetTrashCount()
    {
        trashCollected = 0;
    }
}