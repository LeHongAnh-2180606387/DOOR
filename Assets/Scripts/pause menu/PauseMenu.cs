using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;

    void Update()
    {
        // Kiểm tra nếu nhấn phím Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("PreviousSceneName", currentSceneName);
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        // Lưu tên của Scene hiện tại vào PlayerPrefs
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousSceneName", currentSceneName);

        // Hiển thị pause menu
        pauseMenu.SetActive(true);

        // Tạm dừng trò chơi
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        // Ẩn pause menu
        pauseMenu.SetActive(false);

        // Tiếp tục trò chơi
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Home()
    {
        // Tiếp tục thời gian trước khi quay lại menu chính
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }

    public void Restart()
    {
        // Tiếp tục thời gian trước khi tải lại màn chơi
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map_0");
    }
}
