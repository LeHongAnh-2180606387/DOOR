using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadSceneScript : MonoBehaviour
{
    private bool isPlayerInside;
    private GameObject player;
    private Vector3 playerPosition;

    // Trường để lưu tên của màn sẽ load tiếp theo
    public string nextScene;
    public GameObject loadingScreenPrefab;
    private float delayTime = 2f;//thời gian chờ
    private bool isWaiting = false; // Biến kiểm tra xem hiện đang trong quá trình chờ hay không
    private float waitingTimer = 0f; // Thời gian đã chờ
    private GameObject loadingScreenInstance;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerInside = true;
            player = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E) && !isWaiting)
        {
            // Instantiate the loading screen prefab
            loadingScreenInstance = Instantiate(loadingScreenPrefab);
            loadingScreenInstance.SetActive(true);

            
            // Find the Text component in the loading screen prefab
            Text loadingSceneText = loadingScreenInstance.GetComponentInChildren<Text>();
            if (loadingSceneText != null)
            {
                // Set the text to the name of the next scene
                loadingSceneText.text = nextScene;
            }
            Camera.main.gameObject.SetActive(false);
            
            isWaiting = true; // Bắt đầu quá trình chờ
            waitingTimer = 0f; // Đặt thời gian đã chờ về 0
        }
        
        // Nếu đang trong quá trình chờ, cập nhật thời gian đã chờ
        if (isWaiting)
        {
            waitingTimer += Time.deltaTime;

            // Nếu thời gian đã chờ vượt quá thời gian delay, thì load scene và kết thúc quá trình chờ
            if (waitingTimer >= delayTime)
            {
               
                isWaiting = false;
                SceneManager.LoadScene(nextScene);
                playerPosition = player.transform.position;
            }
        }
        if (!isWaiting && !Camera.main.enabled)
        {
            Camera.main.enabled = true;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == nextScene)
        {
            if (player != null)
            {
                player.transform.position = playerPosition;
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    /*  private void SetAllObjectsActive(bool isActive)
      {
          GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
          foreach (GameObject obj in rootObjects)
          {
              if (obj != loadingScreenInstance) // Bỏ qua màn hình loading
              {
                  obj.SetActive(isActive);
              }
          }
      }*/
   

}

