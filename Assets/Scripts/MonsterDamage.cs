using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int daño = 10;
    public float tiempoEntreDaños = 1f;
    private float siguienteDaño = 0f;

    private void OnCollisionStay(Collision collision)
    {
        // Solo hace daño si colisiona con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= siguienteDaño)
            {
                PlayerHealth vidaJugador = collision.gameObject.GetComponent<PlayerHealth>();
                if (vidaJugador != null)
                {
                    vidaJugador.RecibirDaño(daño);
                    siguienteDaño = Time.time + tiempoEntreDaños;
                }
            }
        }
    }
}
