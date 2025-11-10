using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float minSpeed = 2f;
    public float maxSpeed = 5f;

    // Gán tham chiếu đến Spawner (Kéo và thả AsteroidSpawner vào Inspector)
    public AsteroidSpawner spawner;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float currentHealth;
    private int currentSizeIndex;
    private float baseHealth = 1f;

    // ĐÃ SỬA: KHAI BÁO currentSpeed Ở CẤP ĐỘ LỚP (class level)
    private float currentSpeed;

    // Hàm được gọi từ Spawner để khởi tạo Thiên thạch
    public void Initialize(int sizeIndex, Vector2 direction)
    {
        currentSizeIndex = sizeIndex;
        moveDirection = direction;

        // --- LOGIC MÁU MỚI: ---
        if (currentSizeIndex == 0) // Big (Index 0)
        {
            // Đặt máu cho loại Big là 2 hit (hoặc 3, tùy bạn)
            currentHealth = baseHealth + 1;
        }
        else // Medium, Small, Tiny (Index 1, 2, 3)
        {
            // Mọi loại còn lại chỉ cần 1 hit
            currentHealth = baseHealth; // currentHealth = 1
        }

        // Thiết lập tốc độ và góc quay
        StartMovement();
    }

    void StartMovement() // Đổi tên hàm cũ Start() thành StartMovement()
    {
        rb = GetComponent<Rigidbody2D>();

        // SỬA: Khởi tạo giá trị cho currentSpeed ở đây
        currentSpeed = Random.Range(minSpeed, maxSpeed);

        // Đặt tốc độ bay
        rb.angularVelocity = Random.Range(-100f, 100f);

        if (rb != null)
        {
            // SỬA: currentSpeed giờ đã là biến toàn cục
            rb.linearVelocity = moveDirection * currentSpeed;
        }

        // Hủy sau 10 giây nếu không va chạm
        Destroy(gameObject, 10f);
    }

    // Bạn cần thay thế hàm Start() cũ bằng StartMovement()
    // và đảm bảo logic va chạm và TakeDamage, BreakApart không thay đổi.

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            BreakApart();
        }
    }

    void BreakApart()
    {
        Destroy(gameObject); // Hủy thiên thạch hiện tại

        // 1. Chỉ phân tán nếu KHÔNG phải là kích thước Tiny (index 3)
        if (currentSizeIndex < 3) // Big (0), Medium (1), Small (2)
        {
            // Kích thước mới là kích thước bé hơn 1 bậc
            int nextSizeIndex = currentSizeIndex + 1;

            // Tìm Spawner nếu chưa gán (phải gán trong Inspector)
            if (spawner == null)
            {
                spawner = FindObjectOfType<AsteroidSpawner>();
            }

            if (spawner != null)
            {
                // Sinh ra 4 mảnh vỡ
                for (int i = 0; i < 4; i++)
                {
                    // Tính toán hướng phân tán ngẫu nhiên
                    float angle = Random.Range(0f, 360f);
                    Vector2 scatterDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

                    // Sinh ra mảnh vỡ tại vị trí vỡ của thiên thạch cũ
                    spawner.SpawnAsteroid(nextSizeIndex, transform.position, scatterDirection);
                }
            }
        }
    }
}