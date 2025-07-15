using UnityEngine;

public class Coin : MonoBehaviour
{
    public int pointValue = 10; // <-- tambahkan ini
    public AudioClip coinSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player ngambil koin!");
            PointManager.instance.AddPoint(pointValue);
            FindObjectOfType<GameManager>().AddCoin();
             if (coinSound != null)
            {
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
