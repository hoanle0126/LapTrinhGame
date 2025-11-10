/*
 * EndGameMenu.cs
 * * Mục đích: Hiển thị điểm và xử lý các nút ở màn hình kết thúc.
 * Gắn vào: Một GameObject quản lý trong scene 'EndGame'.
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Cần dùng TextMeshPro

public class EndGameMenu : MonoBehaviour
{
    // Tham chiếu đến Text hiển thị điểm
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // Lấy điểm số đã lưu từ PlayerPrefs
        int finalScore = GameManager.Instance.GetScore();

        // Hiển thị điểm
        if (scoreText != null)
        {
            scoreText.text = "Your Score: " + finalScore;
        }
    }

    // Hàm cho Nút Quay về Menu
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Hàm cho Nút Thoát Game
    public void QuitGame()
    {
        Debug.Log("Quitting game..."); // Chỉ hoạt động trong bản build, không trong Editor
        Application.Quit();
    }
}