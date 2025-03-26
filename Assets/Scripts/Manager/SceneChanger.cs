using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;
    public string FullText;// Tên của scene mà bạn muốn chuyển đến
    public GameObject loadingScreenPrefab;
    private float delayTime = 4f; // thời gian chờ
    private bool isWaiting = false; // Biến kiểm tra xem hiện đang trong quá trình chờ hay không
    private GameObject loadingScreenInstance;

    void OnEnable()
    {
        EventsManager.EventSceneChanger += StartSceneChange;
    }

    void OnDisable()
    {
        EventsManager.EventSceneChanger -= StartSceneChange;
    }

    void StartSceneChange()
    {
        if (!isWaiting)
        {
            // Instantiate the loading screen prefab
            if (loadingScreenPrefab != null)
            {
                loadingScreenInstance = Instantiate(loadingScreenPrefab);
                loadingScreenInstance.SetActive(true);

                // Find the Text component in the loading screen prefab
                Text loadingSceneText = loadingScreenInstance.GetComponentInChildren<Text>();
                if (loadingSceneText != null)
                {
                    // Set the text to the name of the next scene
                    loadingSceneText.text = FullText;
                }
            }
            else
            {
                Debug.LogError("Loading screen prefab is not assigned.");
            }

            if (Camera.main != null)
            {
                Camera.main.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Main camera not found.");
            }

            isWaiting = true; // Bắt đầu quá trình chờ
            StartCoroutine(WaitAndLoadScene());
        }
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    void Update()
    {
        // Không cần kiểm tra Camera.main ở đây vì camera đã bị tắt và scene mới sẽ quản lý camera mới
    }
}
