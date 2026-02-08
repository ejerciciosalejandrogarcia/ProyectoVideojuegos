using UnityEngine;

public class PauseMenuTest : MonoBehaviour
{
    public GameObject PausePanel;

    private bool isPaused = false;

    void Start()
    {
        if (PausePanel != null)
            PausePanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Probar con T
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T presionada");
            if (isPaused)
                Continue();
            else
                Pause();
        }
    }

    public void Pause()
    {
        Debug.Log("Pausando juego");
        if (PausePanel != null)
            PausePanel.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Continue()
    {
        Debug.Log("Reanudando juego");
        if (PausePanel != null)
            PausePanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Método para salir del juego
    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Funciona en build

#if UNITY_EDITOR
        // Si estamos en el editor de Unity, detiene el modo Play
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
