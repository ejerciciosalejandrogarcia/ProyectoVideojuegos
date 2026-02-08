using UnityEngine;
using UnityEngine.AI; // Necesario para NavMeshAgent
using TMPro;

public class NPCController : MonoBehaviour
{
    public NavMeshAgent agent;       // El agente del NPC
    public Transform targetPoint;    // A dónde queremos que camine
    public GameObject messagePanel;  // Panel de diálogo
    public TextMeshProUGUI messageText;
    public string mensaje = "Hola, jugador!";

    public bool hasTalked = false;   // Control para que solo hable una vez

    void Start()
    {
        if (messagePanel != null)
            messagePanel.SetActive(false);
    }

    // Llamamos a esta función cuando el jugador interactúa con el NPC
    public void Talk()
    {
        if (hasTalked) return;

        hasTalked = true;

        // Mostrar mensaje
        if (messagePanel != null)
            messagePanel.SetActive(true);

        if (messageText != null)
            messageText.text = mensaje;

        // Después de mostrar mensaje, ir al punto
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (agent != null && targetPoint != null)
        {
            agent.SetDestination(targetPoint.position);
        }
    }
}
