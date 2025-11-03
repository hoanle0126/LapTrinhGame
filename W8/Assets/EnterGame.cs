using UnityEngine;

public class EnterGame : MonoBehaviour
{
    // Tạo một ô trong Inspector để kéo GameManager (trên Main Camera) vào
    public GameManager gameManager;

    // Hàm này tự động chạy khi có một vật thể *khác*
    // đi vào trigger (collider đã được đánh dấu "Is Trigger")
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter KÍCH HOẠT! Vật va chạm: " + other.name + " | Tag: " + other.tag);

        // Dòng này kiểm tra xem đối tượng cha (có Rigidbody) của vật va chạm
        // có tag là "Player" không.
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            Debug.Log("ĐÃ PHÁT HIỆN PLAYER! Đang gọi ShowPopup()...");

            if (gameManager != null)
            {
                gameManager.ShowPopup();
            }
            else
            {
                Debug.LogError("LỖI: Chưa kết nối GameManager vào EnterGame script!");
            }
        }
    }
}