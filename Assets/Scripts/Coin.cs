using UnityEngine;

public class Coin : MonoBehaviour
{
    public int pointValue = 10; // <-- tambahkan ini

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player ngambil koin!");
            PointManager.instance.AddPoint(pointValue);
            Destroy(gameObject);
        }
    }
}
