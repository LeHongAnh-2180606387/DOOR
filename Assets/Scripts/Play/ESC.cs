using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESC : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Kiểm tra nếu nhấn phím E khi nhân vật ở trong vùng kích hoạt
        {
            player = GameObject.FindGameObjectWithTag("Player");
            // Hiển thị TextBoxPanel
            if (player != null)
            {
                // Lưu vị trí của người chơi vào PlayerPrefs
                PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
                PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
                PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
            }
            string currentSceneName = SceneManager.GetActiveScene().name;

            // Lưu tên của Scene hiện tại vào PlayerPrefs
            PlayerPrefs.SetString("PreviousSceneName", currentSceneName);

            // Chuyển sang Scene Settings
           

            SceneManager.LoadScene("Ui_SaveGame");
            

        }
    }
 
}