using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // Cần thiết để tải/khởi động lại scene
using TMPro; // Cần thiết để làm việc với TextMeshPro

public class GameManager : MonoBehaviour
{
    // === Tham chiếu đến các đối tượng UI ===
    [Header("UI Components")]
    // Kéo toàn bộ đối tượng Canvas vào đây
    public GameObject popupCanvas;

    // Kéo các đối tượng TextMeshPro vào đây
    public TMP_Text titleText;
    public TMP_Text messageText;

    // Kéo đối tượng Text bên trong các nút vào đây
    public TMP_Text enterButtonText;
    public TMP_Text restartButtonText;


    // === Phương thức Start (Chạy khi game bắt đầu) ===
    void Start()
    {
        // 1. Đảm bảo pop-up bị tắt theo mặc định
        if (popupCanvas != null)
        {
            popupCanvas.SetActive(false);
        }

        // 2. Tải nội dung động (như slide 29 yêu cầu)
        SetDynamicContent();
    }

    // Hàm này điền nội dung chữ cho UI
    void SetDynamicContent()
    {
        if (titleText != null)
            titleText.text = "Welcome to Unity!";

        if (messageText != null)
            messageText.text = "Do you want to continue?";

        // Giả sử bạn muốn thay đổi cả văn bản của nút
        // (Nếu bạn đã đặt tên nút trong Editor, bạn có thể bỏ qua phần này)
        if (enterButtonText != null)
            enterButtonText.text = "Enter";

        if (restartButtonText != null)
            restartButtonText.text = "Restart";
    }

    // === Hàm công khai (Public) để điều khiển Pop-up ===

    // Hàm này sẽ được gọi bởi script trigger (EnterGame.cs)
    public void ShowPopup()
    {
        if (popupCanvas != null)
        {
            popupCanvas.SetActive(true);

            // Mẹo nâng cao: Tạm dừng game khi pop-up hiện lên
             Time.timeScale = 0f;
        }
    }

    // === Các hàm cho Nút bấm (như slide 33) ===

    // Hàm này sẽ được gán cho nút "Enter" (Continue)
    public void NextScene()
    {
        // QUAN TRỌNG: Thay "TenSceneTiepTheo" bằng tên Scene tiếp theo của bạn
        // Ví dụ: SceneManager.LoadScene("Level_2");

        // Đảm bảo bạn đã vào File > Build Settings... và thêm scene của bạn vào
        Debug.Log("Đang tải Scene tiếp theo...");
         //Time.timeScale = 1f; // Bật lại thời gian nếu bạn đã tạm dừng

        // Tên scene giả định, hãy thay đổi cho đúng với dự án của bạn
        SceneManager.LoadScene("NextScence");
    }

    // Hàm này sẽ được gán cho nút "Restart"
    public void RestartScene()
    {
        // Bật lại thời gian nếu bạn đã tạm dừng
         Time.timeScale = 1f;

        // Tải lại scene hiện tại
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}