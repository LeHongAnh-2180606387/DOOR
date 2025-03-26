using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    public void OnButtonClick()
    {
        if (PlayerPrefs.HasKey("PreviousSceneName"))
        {
            // Lấy tên của Scene trước đó từ PlayerPrefs
            string previousSceneName = PlayerPrefs.GetString("PreviousSceneName");
            // Chuyển về Scene trước đó
            SceneManager.LoadScene(previousSceneName);

        }
        else
        {
            SceneManager.LoadScene("Main_Menu");
        }


    }

    
}