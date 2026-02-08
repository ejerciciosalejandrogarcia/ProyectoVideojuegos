using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class NPCDialogLevel_3 : MonoBehaviour
{
    [TextArea(3, 6)]
    public string dialog;

    public GameObject personaje;
    public GameObject roomEventObject;

    public NavMeshAgent agent;
    public Animator animator;
    public Transform[] waypoints;

    [Header("Diálogo")]
    public GameObject DialogPanel;          // Panel del NPC y protagonista
    public TextMeshProUGUI dialogText;

    [Header("Objetivo")]
    public GameObject MessagePanel;         // Panel para objetivos
    public TextMeshProUGUI messageText;

    public float waitBeforeLeaving = 5f;
    public float dialogDuration = 3f;

    [TextArea(2, 4)]
    public string leavingDialog = "Iré a ver la tele mientras espero a mamá, seguro que no tardará nada en llegar.";

    bool playerNear = false;
    bool hasTalked = false;
    bool isMoving = false;
    int currentWaypointIndex = 0;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Asegurarse de que los paneles estén ocultos al inicio
        if (DialogPanel != null) DialogPanel.SetActive(false);
        if (MessagePanel != null) MessagePanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            DialogManager.Instance.ShowInteractText();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            // Solo ocultar el diálogo del NPC si aún no ha hablado
            if (!hasTalked)
                DialogManager.Instance.HideDialog();

            DialogManager.Instance.HideInteractText();
        }
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E) && !hasTalked)
        {
            hasTalked = true;

            // Mostrar diálogo inicial del NPC en su panel
            if (DialogPanel != null && dialogText != null)
            {
                DialogPanel.SetActive(true);
                dialogText.text = dialog;
            }

            DialogManager.Instance.HideInteractText();

            if (personaje != null) personaje.SetActive(true);
            if (roomEventObject != null) roomEventObject.SetActive(true);

            // Esperar y luego iniciar el diálogo del protagonista + movimiento
            if (agent != null && waypoints.Length > 0)
                StartCoroutine(LeaveAfterTime(waitBeforeLeaving));
        }

        // Animación de caminar
        if (agent != null && animator != null)
        {
            animator.SetBool("Walk", agent.velocity.magnitude > 0.1f);
        }
    }

    IEnumerator LeaveAfterTime(float time)
    {
        // Espera los segundos antes de que el NPC empiece a irse
        yield return new WaitForSeconds(time);

        // 💬 Mostrar diálogo del protagonista
        if (DialogPanel != null && dialogText != null)
        {
            DialogPanel.SetActive(true);
            dialogText.text = leavingDialog;
        }

        // 🎯 Mostrar objetivo en MessagePanel
        if (MessagePanel != null && messageText != null)
        {
            MessagePanel.SetActive(true);
            messageText.text = "Ve al salón para relajarte";
        }

        // ⏱️ Iniciar corrutina que oculta el diálogo tras 3 segundos
        StartCoroutine(HideDialogAfterTime(dialogDuration));

        // ✅ Ahora el NPC empieza a caminar
        isMoving = true;
        currentWaypointIndex = 0;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    IEnumerator HideDialogAfterTime(float t)
    {
        yield return new WaitForSeconds(t);
        if (DialogPanel != null) DialogPanel.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!isMoving || agent == null || agent.pathPending) return;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Al llegar al PRIMER waypoint → atravesar edificio
            if (currentWaypointIndex == 0 && rb != null)
            {
                rb.isKinematic = true;
                rb.detectCollisions = false;
            }

            // Al llegar al SEGUNDO waypoint → desaparecer
            if (currentWaypointIndex == 1)
            {
                gameObject.SetActive(false);
                return;
            }

            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                isMoving = false;
                return;
            }

            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}
