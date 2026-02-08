using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Para usar Text UI
using TMPro;

public class TrushEventLevel4 : MonoBehaviour
{
    [Header("Referencias")]
    public Animator fadeAnimator;

    [Header("Panel de Objetivos")]
    public TextMeshProUGUI objetivoText;

    void Start()
    {
        // Asignación automática del Animator si no está
        if (fadeAnimator == null)
        {
            GameObject fadeObject = GameObject.FindGameObjectWithTag("FadeCanvas");
            if (fadeObject != null)
            {
                fadeAnimator = fadeObject.GetComponent<Animator>();
            }

            if (fadeAnimator == null)
            {
                fadeAnimator = FindObjectOfType<Animator>();
            }

            if (fadeAnimator == null)
            {
                Debug.LogError("No se encontró Animator para el fade.");
                return;
            }
        }

        // Actualizar el panel de objetivos
        if (objetivoText != null)
        {
            objetivoText.text = "Vuelve a casa";
        }

        // Ejecutar Fade In
        StartCoroutine(PlayFadeIn());
    }

    IEnumerator PlayFadeIn()
    {
        yield return new WaitForSeconds(0.5f);

        if (fadeAnimator != null)
        {
            fadeAnimator.Play("FadeIn");
            Debug.Log("Iniciando Fade In en nivel 4");

            yield return new WaitForSeconds(1f);
            Debug.Log("Fade In completado");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayFadeInManual();
        }
    }

    public void PlayFadeInManual()
    {
        StartCoroutine(PlayFadeIn());
    }
}
