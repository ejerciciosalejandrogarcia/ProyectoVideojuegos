using System.Collections;
using UnityEngine;

namespace SojaExiles
{
    public class opencloseDoor : MonoBehaviour
    {
        public Animator openandclose; // Animator de la puerta
        public bool open;             // Estado de la puerta
        public Transform Player;      // Referencia al jugador

        void Start()
        {
            open = true;
            StartCoroutine(opening());
            Debug.Log("Las puertas estan abiertas");

        }

        void OnMouseOver()
        {
            Debug.Log("Mouse sobre la puerta");

            if (Player == null)
            {
                Debug.LogWarning("No hay Player asignado en el inspector!");
                return; // Salimos si Player no está asignado
            }

            float dist = Vector3.Distance(Player.position, transform.position);
            Debug.Log("Distancia al jugador: " + dist);

            if (dist < 15)
            {
                Debug.Log("Jugador dentro del rango de interacción");

                if (!open)
                {
                    Debug.Log("La puerta está cerrada, esperando clic para abrir...");
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("Clic detectado para abrir");
                        StartCoroutine(opening());
                    }
                }
                else
                {
                    Debug.Log("La puerta está abierta, esperando clic para cerrar...");
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("Clic detectado para cerrar");
                        StartCoroutine(closing());
                    }
                }
            }
            else
            {
                Debug.Log("Jugador fuera del rango de interacción");
            }
        }

        IEnumerator opening()
        {
            Debug.Log("Ejecutando coroutine: abrir puerta");
            if (openandclose == null)
            {
                Debug.LogError("Animator no asignado en el inspector!");
                yield break; // Salimos si no hay Animator
            }

            openandclose.Play("Opening");
            open = true;
            Debug.Log("Puerta abierta");
            yield return new WaitForSeconds(0.5f);
        }

        IEnumerator closing()
        {
            Debug.Log("Ejecutando coroutine: cerrar puerta");
            if (openandclose == null)
            {
                Debug.LogError("Animator no asignado en el inspector!");
                yield break;
            }

            openandclose.Play("Closing");
            open = false;
            Debug.Log("Puerta cerrada");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
