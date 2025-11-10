using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FighterController : MonoBehaviour
{
    public string gameOverSceneName = "EndGame"; // Tên Scene Game Over
    private bool isDead = false; // Biến kiểm tra tàu đã bị hủy chưa
    // CÁC THUỘC TÍNH ĐIỀU KHIỂN
    public float forwardSpeed = 5f;
    public float rotationSpeed = 720f;

    // CÁC THUỘC TÍNH BẮN
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;

    // CÁC BIẾN NỘI BỘ
    private Animator animator;
    private float nextFireTime = 0f;

    // Hướng di chuyển CỐ ĐỊNH (Thay Vector3.right thành Vector3.up nếu cần)
    private readonly Vector3 fixedDirection = Vector3.right;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isActing", true);
        }
    }

    void Update()
    {
        // 1. LUÔN LUÔN DI CHUYỂN THẲNG THEO TRỤC CỐ ĐỊNH
        MoveForward();

        // 2. XOAY ĐỘC LẬP THEO CHUỘT
        RotateTowardsMouse();

        // 3. LOGIC BẮN ĐẠN TỰ ĐỘNG
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void MoveForward()
    {
        // Tàu di chuyển theo trục cố định (World Space), không bị ảnh hưởng bởi góc quay.
        transform.position += transform.right * forwardSpeed * Time.deltaTime;
    }

    void RotateTowardsMouse()
    {
        // Lấy vị trí chuột (Input System Package)
        if (Mouse.current == null) return;
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        // Chuyển đổi vị trí chuột sang thế giới game
        Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Tính toán Hướng từ tàu đến chuột
        Vector3 directionToMouse = targetWorldPosition - transform.position;

        // Tính Góc xoay (Angle)
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Áp dụng góc bù trừ
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Xoay tàu một cách mượt mà
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu tàu va chạm với Thiên thạch và chưa bị hủy
        if (other.CompareTag("Meteor") && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        // 1. Kích hoạt Animation Nổ
        if (animator != null)
        {
            // Kích hoạt Trigger để chuyển sang State Explosion
            animator.SetTrigger("isDestroyed");
        }

        // 2. Vô hiệu hóa điều khiển và bắn
        // Hủy Component này (FighterController) để ngừng di chuyển và bắn
        Destroy(GetComponent<FighterController>());

        // Hoặc vô hiệu hóa chính đối tượng (nếu bạn không muốn thấy tàu)
        // transform.GetComponent<Renderer>().enabled = false; 

        // 3. Chuyển Scene sau khi animation nổ xong (ví dụ: 1.5 giây)
        LoadGameOverScene();

        // Tùy chọn: Hủy thiên thạch đã va chạm
        // Destroy(other.gameObject); 
    }

    void LoadGameOverScene()
    {
        // Tải Scene Game Over
        SceneManager.LoadScene("EndGame");
    }
}