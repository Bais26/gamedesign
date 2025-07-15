using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        // Cek kalau sudah ada musik sebelumnya, hapus yang baru
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); // JANGAN dihapus saat pindah scene
        }
    }
}
