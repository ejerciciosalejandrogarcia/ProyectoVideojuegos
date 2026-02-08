using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MonsterChase : MonoBehaviour
{
    [Header("Referencias")]
    public Transform jugador;

    [Header("Configuración")]
    public float velocidad = 5f;
    public float distanciaDeteccion = 50f;
    public float distanciaParada = 1.5f;

    private Rigidbody rb;

    // 🔒 NUEVO
    private bool puedePerseguir = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.isKinematic = false;
    }

    void FixedUpdate()
    {
        // 🔒 si no puede perseguir, no hace nada
        if (!puedePerseguir) return;
        if (jugador == null) return;

        Vector3 direccion = jugador.position - transform.position;
        direccion.y = 0f;
        float distancia = direccion.magnitude;

        if (distancia <= distanciaDeteccion && distancia > distanciaParada)
        {
            direccion.Normalize();
            Vector3 movimiento = direccion * velocidad * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + movimiento);
            rb.MoveRotation(
                Quaternion.Euler(0, Quaternion.LookRotation(direccion).eulerAngles.y, 0)
            );
        }
    }

    // 🔓 Se llama desde el evento
    public void EmpezarPersecucion()
    {
        puedePerseguir = true;
    }
}
