using UnityEngine;
using UnityEngine.InputSystem; // Bắt buộc phải có khi dùng Input System mới

public class PlayerController : MonoBehaviour
{
    // Tham chiếu đến thành phần Animator
    private Animator anim;

    // Biến trạng thái để theo dõi nhân vật đang hành động hay đứng yên
    private bool isCurrentlyActing = false;

    void Start()
    {
        // Lấy tham chiếu đến thành phần Animator
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator component not found on the Bomber! Make sure the Animator Controller is assigned.");
        }
    }

    void Update()
    {
        // --- LOGIC XỬ LÝ INPUT ---

        // Kiểm tra xem có đang nhấn phím Space trong khung hình hiện tại không
        // Sử dụng lớp Keyboard từ Input System Package
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // 1. Đảo ngược trạng thái hiện tại (TRUE <-> FALSE)
            isCurrentlyActing = !isCurrentlyActing;

            // 2. Gán giá trị trạng thái mới vào tham số "IsActing" của Animator
            // Đây là điều kiện kích hoạt hoạt ảnh
            anim.SetBool("IsActing", isCurrentlyActing);

            // Nếu isCurrentlyActing là TRUE, Animator chuyển sang Action.
            // Nếu isCurrentlyActing là FALSE, Animator chuyển về Idle.
        }
    }

    // Nếu bạn muốn thêm code di chuyển hoặc các hàm khác, bạn có thể thêm ở đây.
}