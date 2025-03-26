using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public GameObject loadingScreenPrefab;
    public string fullText;
    private GameObject loadingScreenInstance;
    private Text textComponent;
    private int currentIndex;
    private float nextCharacterTime;
    private float loadSceneTime;
    private bool loadingScene;
    public Canvas canvas;


    void OnEnable()
    {
        EventsManager.EventSceneChanger += StartDisplayTextAndLoadScene;
    }

    void OnDisable()
    {
        EventsManager.EventSceneChanger -= StartDisplayTextAndLoadScene;
    }

    public void StartDisplayTextAndLoadScene()
    {
        loadingScene = false; // Khởi tạo loadingScene là false
        loadingScreenInstance = Instantiate(loadingScreenPrefab);
        loadingScreenInstance.SetActive(true);

        Camera.main.gameObject.SetActive(false);
        
        // Lấy component Text từ prefab
        textComponent = loadingScreenInstance.GetComponentInChildren<Text>();
        if (textComponent == null)
        {
            Debug.LogError("Prefab không có component Text!");
            return;
        }

        // Thiết lập văn bản ban đầu cho textComponent
        textComponent.text = fullText;
        //canvas = loadingScreenInstance.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.enabled = false;
        }
        currentIndex = 0;
        nextCharacterTime = Time.time; // Thời điểm bắt đầu hiển thị ký tự đầu tiên
        loadSceneTime = Time.time + 7f; // Thời gian để load scene mới
    }

    private void Update()
    {
        // Nếu vẫn còn ký tự để hiển thị và đã đến thời điểm hiển thị ký tự tiếp theo
        if (textComponent != null && currentIndex < fullText.Length && Time.time >= nextCharacterTime)
        {
            textComponent.text = fullText.Substring(0, currentIndex + 1); // Hiển thị từ đầu đến ký tự hiện tại
            currentIndex++;
            nextCharacterTime += 0.1f; // Thời gian giữa mỗi ký tự
        }
        // Nếu đã hiển thị hết văn bản và chưa loading scene và đã đến thời gian load scene
        else if (currentIndex >= fullText.Length && !loadingScene && Time.time >= loadSceneTime)
        {
            loadingScene = true; // Đánh dấu đang loading scene
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("Main_Menu");
        if (Camera.main != null && !Camera.main.enabled )
        {
            Camera.main.enabled = true;
        }
        if(canvas != null && !canvas.enabled)
        { 
            canvas.enabled = true;
        }    
    }
}
