using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    [Header("Diálogo")]
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;

    [Header("Interacción")]
    public GameObject interactPanel;
    public TextMeshProUGUI interactText;

    void Awake()
    {
        Instance = this;
        dialogPanel.SetActive(false);
        interactPanel.SetActive(false);
    }

    // Mostrar diálogo
    public void ShowDialog(string text)
    {
        dialogPanel.SetActive(true);
        dialogText.text = text;
    }

    // Ocultar diálogo
    public void HideDialog()
    {
        dialogPanel.SetActive(false);
    }

    // Mostrar mensaje de interacción
    public void ShowInteractText()
    {
        interactPanel.SetActive(true);
        interactText.text = "Presiona E para interactuar";
    }

    // Ocultar mensaje de interacción
    public void HideInteractText()
    {
        interactPanel.SetActive(false);
    }
}
