using System.Collections;
using UnityEngine;
using TMPro;

public class DialogMum : MonoBehaviour
{
    [TextArea(3, 6)]
    public string[] dialogueLines = new string[]
    {
        "Protagonista: He visto a un monstruo...",
        "Madre: No digas tonterías hijo.",
        "Protagonista: Si quieres, compruébalo afuera."
    };

    bool playerNear = false;
    bool dialogActive = false;

    [Header("UI Elements")]
    public GameObject dialogPanel;         // Panel que contiene el texto
    public TextMeshProUGUI dialogText;    // Texto donde se muestra el diálogo
    public GameObject interactText;        // Texto "Presiona E"

    public float lineDuration = 2f;        // Tiempo que dura cada línea en segundos

    void Start()
    {
        dialogPanel.SetActive(false);
        if (interactText != null)
            interactText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            if (interactText != null)
                interactText.SetActive(true); // mostrar "Presiona E"
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            dialogPanel.SetActive(false);
            dialogActive = false;

            if (interactText != null)
                interactText.SetActive(false); // ocultar "Presiona E"
        }
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogActive)
            {
                StartCoroutine(ShowDialogue());
            }
        }
    }

    IEnumerator ShowDialogue()
    {
        dialogActive = true;

        if (interactText != null)
            interactText.SetActive(false);

        dialogPanel.SetActive(true);

        for (int i = 0; i < dialogueLines.Length; i++)
        {
            dialogText.text = dialogueLines[i];

            // Espera "lineDuration" segundos antes de pasar a la siguiente línea
            yield return new WaitForSeconds(lineDuration);
        }

        dialogPanel.SetActive(false);
        dialogActive = false;
    }
}
