using UnityEngine;
using System.Collections;

public class MonsterRevealTrigger : MonoBehaviour
{
    [Header("Cámaras")]
    public GameObject monsterCamera;
    public GameObject mainCamera;
    public float cameraTime = 4f;

    [Header("Monstruo")]
    public MonsterChase monster;
    [Header("Monstruo Visual")]
    public GameObject monsterModel; // referencia al modelo del monstruo

    [Header("Sonido")]
    public AudioSource monsterSound; // AudioSource que reproduce el rugido
    public float soundDuration = 4f;

    private void OnTriggerEnter(Collider other)
    {
        if (MonsterEventManager.eventoActivado) return;
        if (!other.CompareTag("Player")) return;

        MonsterEventManager.eventoActivado = true;

        StartCoroutine(ShowMonster());
        StartCoroutine(PlayMonsterSound()); // reproducir sonido
    }

    IEnumerator ShowMonster()
    {
        // Aparecer el monstruo visualmente
        if (monsterModel != null)
        {
            monsterModel.SetActive(true);
            Debug.Log("Monstruo activado en la posición: " + monsterModel.transform.position);
        }
        else
        {
            Debug.LogWarning("monsterModel es nulo!");
        }        // Cambiar a cámara del monstruo
        mainCamera.SetActive(false);
        monsterCamera.SetActive(true);

        yield return new WaitForSeconds(cameraTime);

        // Volver a cámara principal
        monsterCamera.SetActive(false);
        mainCamera.SetActive(true);

        // Empezar persecución
        monster.EmpezarPersecucion();
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayChaseMusic(true); // true = bucle
        }

    }
    IEnumerator PlayMonsterSound()
    {
        if (monsterSound != null)
        {
            monsterSound.Play();
            yield return new WaitForSeconds(soundDuration);
            monsterSound.Stop();
        }
    }
}
