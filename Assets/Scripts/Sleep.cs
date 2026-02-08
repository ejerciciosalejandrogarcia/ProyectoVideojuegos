using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sleep : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public float fadeOutDuration = 2.5f;
    public string sceneToLoad = "Level3";

    private bool isFading = false;

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

        // Fade Out
        animator.Play("FadeOut");

        if (audioSource != null)
            audioSource.Play();

        // Esperar que termine el FadeOut
        yield return new WaitForSeconds(fadeOutDuration);

        // Cargar escena 3
        SceneManager.LoadScene(sceneToLoad);
    }

}
