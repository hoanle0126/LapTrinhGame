using UnityEngine;

public class Star : MonoBehaviour
{
    public int scoreValue = 10; // Số điểm cộng khi nhặt

    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng va chạm là Player
        if (other.CompareTag("Player"))
        {
            // 1. Cộng điểm
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreValue);
            }

            // 2. Hủy ngôi sao
            Destroy(gameObject);
        }
    }
}