using UnityEngine;
using TMPro; // Thêm thư viện này nếu bạn dùng TextMeshPro

public class GameManager : MonoBehaviour
{
    // Kéo và thả đối tượng Text UI hiển thị điểm số vào đây
    public TextMeshProUGUI scoreText;

    private int score = 0;
    public static GameManager Instance; // Singleton pattern

    void Awake()
    {
        // Đảm bảo chỉ có một GameManager trong Scene
        if (Instance == null)
        {
            Instance = this;
            // BẬT: KHÔNG HỦY đối tượng này khi chuyển Scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Nếu đã có GameManager, hủy đối tượng mới này
            Destroy(gameObject);
        }

        // Khởi tạo điểm số hiển thị
        UpdateScoreDisplay();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public int GetScore()
    {
        return score;
    }
}