using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // o using TMPro; si usas TMP

public class RoomEvent : MonoBehaviour
{
    public Camera roomCamera;
    public Camera playerCamera;
    public AudioSource music;
    public GameObject personaje;

    public GameObject textPanel; // DialogPanel
    public TextMeshProUGUI panelText;       // DialogText
    public float textSpeed = 0.05f;
    public string mensaje = "¿Qué ha sido eso? Supongo que no era nada,ire a ver la tele para relajarse ";

    bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;
            StartCoroutine(Evento());
        }
    }

   public IEnumerator Evento()
    {
        // 1. Activar cámara de la sala
        playerCamera.gameObject.SetActive(false);
        roomCamera.gameObject.SetActive(true);

        // 2. Reproducir música
        music.Play();

        // 3. Esperar a que termine la música
        yield return new WaitWhile(() => music.isPlaying);

        // 4. Desaparece el personaje
        personaje.SetActive(false);

        // 5. Volver a la cámara del jugador
        roomCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);

        // 6. Mostrar panel y escribir texto
        textPanel.SetActive(true);
        panelText.text = "";
        yield return StartCoroutine(EscribirTexto(mensaje));

        // 7. Esperar 1 segundo antes de ocultar el panel
        yield return new WaitForSeconds(1f);
        textPanel.SetActive(false); // 8. Ocultar el panel
    }
    IEnumerator EscribirTexto(string mensaje)
    {
        panelText.text = "";
        foreach (char c in mensaje)
        {
            panelText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

}
