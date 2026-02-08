using UnityEngine;

public class DestroyMonsterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo entró al trigger: " + other.name);

        if (other.CompareTag("Monster"))
        {
            Debug.Log("Monstruo detectado. Destruyendo...");

            // 🎵 Detener música de persecución
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.StopChaseMusic();
            }

            Destroy(other.gameObject);
        }
    }
}
