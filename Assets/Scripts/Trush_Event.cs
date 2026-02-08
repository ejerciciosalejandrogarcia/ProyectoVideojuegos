using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trush_Event : MonoBehaviour
{
    [Header("Referencias")]
    public Animator fadeAnimator;
    public AudioSource audioSource;  // Asigna el AudioSource en el Inspector
    public AudioClip trashSound;     // Asigna el sonido de tirar basura (5 segundos)

    [Header("Configuración")]
    public float triggerDistance = 2f;

    private Transform player;
    private bool hasTriggered = false;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;

        if (fadeAnimator == null)
            Debug.LogError("FadeAnimator NO asignado");

        if (audioSource == null)
        {
            // Intentar obtener el AudioSource si no está asignado
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogWarning("AudioSource no encontrado. Añadiendo uno automáticamente.");
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    void Update()
    {
        if (hasTriggered || player == null || fadeAnimator == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= triggerDistance)
        {
            hasTriggered = true;
            Debug.Log("FadeAnimator apunta a: " + fadeAnimator.gameObject.name);

            // Iniciar la corutina que maneja el fadeOut y el sonido
            StartCoroutine(PlayFadeWithSound());
        }
    }

    IEnumerator PlayFadeWithSound()
    {
        // Reproducir el sonido de tirar basura
        if (audioSource != null && trashSound != null)
        {
            audioSource.clip = trashSound;
            audioSource.Play();
            Debug.Log("Reproduciendo sonido de tirar basura (duración: " + trashSound.length + " segundos)");
        }
        else
        {
            Debug.LogWarning("AudioSource o trashSound no asignado");
        }

        // Iniciar la animación de fadeOut
        fadeAnimator.Play("FadeOut");

        // Esperar a que termine el sonido (5 segundos)
        yield return new WaitForSeconds(trashSound != null ? trashSound.length : 5f);

        // Aquí puedes añadir más lógica después de que termine el sonido
        Debug.Log("Sonido y fadeOut completados");

        // Opcional: Cambiar de escena o activar algo después
         SceneManager.LoadScene("Level4");
    }
}