using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton

    [Header("Música de persecución")]
    public AudioSource chaseMusic;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // para que persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Reproducir música de persecución
    public void PlayChaseMusic(bool loop = true)
    {
        if (chaseMusic == null) return;
        chaseMusic.loop = loop;
        if (!chaseMusic.isPlaying)
            chaseMusic.Play();
    }

    // Detener música de persecución
    public void StopChaseMusic()
    {
        if (chaseMusic == null) return;
        chaseMusic.Stop();
    }

    // Saber si la música se está reproduciendo
    public bool IsChaseMusicPlaying()
    {
        if (chaseMusic == null) return false;
        return chaseMusic.isPlaying;
    }
}
