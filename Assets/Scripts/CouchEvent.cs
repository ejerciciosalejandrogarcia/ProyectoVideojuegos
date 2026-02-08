using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class CouchEvent : MonoBehaviour
{
    public Animator animator;

    [Header("Audio")]
    public AudioSource musicSource;

    [Header("Diálogo")]
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;

    [Header("Noticia")]
    public float timePerNews = 41; // 1 minuto por texto

    private bool isFading = false;

    [Header("Notificación Móvil")]
    public AudioSource notificationSource;   // AudioSource del sonido de móvil
    public float notificationTextTime = 4f;  // Tiempo que se muestra el mensaje


    [Header("Objetivos / Misiones")]
    public GameObject objectivePanel;        // Panel de objetivo
    public TextMeshProUGUI objectiveText;    // Texto de objetivo

    // TEXTOS DE LA NOTICIA
    private string[] newsTexts =
    {
        "ÚLTIMAS NOTICIAS — ZAMOSKVORECHYE\n" +
        "Las autoridades han confirmado el hallazgo de cinco cadáveres " +
        "en un edificio residencial abandonado del distrito de Zamoskvorechye. " +
        "Los cuerpos muestran signos de violencia extrema",

        "TESTIMONIOS DE LOS CIUDADANOS\n" +
        "\"Por las noches se escuchan ruidos extraños\", afirma una vecina. " +
        "\"No es la primera vez que alguien desaparece, pero nadie nos hace caso\"."    };

    private void Start()
    {
        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeSequence());
        }
    }

    private IEnumerator FadeSequence()
    {
        isFading = true;

        // 🖤 FADE OUT (0s)
        if (animator != null)
            animator.Play("FadeOut");

        // 📰 MOSTRAR PANEL
        if (dialogPanel != null)
            dialogPanel.SetActive(true);

        // ▶️ MÚSICA (0s)
        if (musicSource != null && musicSource.clip != null)
            musicSource.Play();

        // TEXTO 1 (0s - 22.5s)
        if (dialogText != null)
            dialogText.text = newsTexts[0];

        yield return new WaitForSeconds(22.5f);

        // TEXTO 2 (22.5s - 45s)
        if (dialogText != null)
            dialogText.text = newsTexts[1];

        yield return new WaitForSeconds(22.5f);

        // ⏹️ PARAR MÚSICA (45s)
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();

        // ❌ OCULTAR TEXTO
        if (dialogPanel != null)
            dialogPanel.SetActive(false);

        // 🌅 FADE IN (45s)
        if (animator != null)
            animator.Play("FadeIn");


        // ⏱️ pequeña pausa tras el fade
        yield return new WaitForSeconds(1.5f);

        // 📱 SONIDO DE NOTIFICACIÓN
        if (notificationSource != null && notificationSource.clip != null)
            notificationSource.Play();

        // 📰 MOSTRAR MENSAJE DEL MÓVIL
        if (dialogPanel != null && dialogText != null)
        {
            dialogPanel.SetActive(true);
            dialogText.text =
                "Me acaba de llegar un mensaje,\n" +
                "seguro que será mamá.Vere haber que quiere";
        }

        yield return new WaitForSeconds(notificationTextTime);

        if (objectivePanel != null && objectiveText != null)
        {
            objectivePanel.SetActive(true);
            objectiveText.text = "Mira el mensaje"; // Texto de la misión
        }

        // ❌ OCULTAR MENSAJE
        if (dialogPanel != null)
            dialogPanel.SetActive(false);

       
    }



}
