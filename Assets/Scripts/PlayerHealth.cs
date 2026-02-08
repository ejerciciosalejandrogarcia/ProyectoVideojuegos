using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida del jugador")]
    public int vidaMaxima = 100;
    public int vidaActual;

    [Header("UI de vida")]
    public Image barraVida; // Arrastra aquí la Image que será la barra

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarBarraVida();
    }

    // Función para recibir daño
    public void RecibirDaño(int daño)
    {
        vidaActual -= daño;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima); // evita valores negativos

        Debug.Log("Jugador recibe daño. Vida actual: " + vidaActual);

        ActualizarBarraVida();

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    // Función para curar
    public void Curar(int cantidad)
    {
        vidaActual += cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        ActualizarBarraVida();
    }

    // Actualiza la barra de vida
    void ActualizarBarraVida()
    {
        if (barraVida != null)
        {
            barraVida.fillAmount = (float)vidaActual / vidaMaxima;

            // Opcional: cambiar color según vida
            barraVida.color = Color.Lerp(Color.red, Color.green, (float)vidaActual / vidaMaxima);
        }
    }

    void Morir()
    {
        Debug.Log("El jugador ha muerto");
        // Aquí luego puedes:
        // - reiniciar nivel
        // - mostrar pantalla de muerte
        // - desactivar controles
    }
}
