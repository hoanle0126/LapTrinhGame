/*
 * MainMenu.cs
 * * Mục đích: Xử lý các nút bấm trên Menu chính.
 * Gắn vào: Một GameObject quản lý trong scene 'MainMenu'.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Hàm này sẽ được gọi bởi Nút Play
    public void PlayGame()
    {
        // Tải scene Gameplay
        SceneManager.LoadScene("GameScene");
    }

    // Hàm này sẽ được gọi bởi Nút Instructions
    public void ShowInstructions(GameObject instructionsPanel)
    {
        // Hiện một UI Panel (GameObject) chứa hướng dẫn
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(true);
        }
    }

    // Hàm này để đóng Panel hướng dẫn
    public void HideInstructions(GameObject instructionsPanel)
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game..."); // Chỉ hoạt động trong bản build, không trong Editor
        Application.Quit();
    }
}