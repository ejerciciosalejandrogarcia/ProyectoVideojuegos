using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Si usas TextMeshPro

public class Fade_Level3 : MonoBehaviour
{
    public Animator animator;

    // Objetos que quieres que empiecen desactivados
    public GameObject[] objetosParaDesactivar;

    // Panel de mensaje y su texto
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public string mensaje = "Abre la puerta";

    // Audio
    public AudioSource audioSource;

    void Start()
    {
        // 1. Desactivar todos los objetos al inicio
        foreach (GameObject obj in objetosParaDesactivar)
        {
            obj.SetActive(false);
        }

        // 2. Reproducir animación de fade
        if (animator != null)
            animator.Play("FadeIn");

        // 3. Mostrar panel con mensaje
        if (messagePanel != null)
            messagePanel.SetActive(true);

        if (messageText != null)
            messageText.text = mensaje;

        // 4. Reproducir audio
        if (audioSource != null)
            audioSource.Play();

        // ⚠️ Ya no ocultamos el panel automáticamente
        // StartCoroutine(HideMessageAfterDelay(2f));
    }

    // ⚠️ Esta función ya no es necesaria
    /*
    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (messagePanel != null)
            messagePanel.SetActive(false);
    }
    */
}
