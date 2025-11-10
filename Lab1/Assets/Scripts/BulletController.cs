using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f; // Tốc độ bay của đạn
    public float lifeTime = 2f; // Thời gian tồn tại của đạn

    void Start()
    {
        // Đạn sẽ bay theo hướng "Phải" cục bộ (transform.right) của nó, 
        // chính là hướng mũi tàu tại thời điểm bắn.
        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed;
        }

        // Hủy đối tượng đạn sau một khoảng thời gian
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Xử lý va chạm
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}