using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraChanger : MonoBehaviour
{
    public static CameraChanger Instance;
    public float transitionDuration = 1f; // Thời gian chuyển đổi mượt
    private bool transitioning = false; // Biến để kiểm tra xem chuyển đổi đã hoàn thành chưa
    private Transform previousFollowTarget; // Lưu trữ đối tượng được theo dõi của camera trước khi chuyển đổi

    private PlayerController playerMovement; // Tham chiếu đến script PlayerMovement

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerMovement = FindObjectOfType<PlayerController>();
        if (playerMovement == null)
        {
            Debug.LogError("Không tìm thấy script PlayerMovement!");
        }
    }

    private void OnEnable()
    {
        EventsManager.EventCameraChanger += ActivateCamera;
    }

    private void OnDisable()
    {
        EventsManager.EventCameraChanger -= ActivateCamera;
    }

    public void ActivateCamera(CinemachineVirtualCamera cameraChanger)
    {
        if (!transitioning)
        {
            StartCoroutine(TransitionCamera(cameraChanger));
        }
    }

    private IEnumerator TransitionCamera(CinemachineVirtualCamera targetCamera)
    {
        transitioning = true;

        // Tắt di chuyển của nhân vật
        if (playerMovement != null)
        {
            playerMovement.EnableMovement(false);
        }

        // Kiểm tra Camera.main không null
        if (Camera.main == null)
        {
            Debug.LogError("Không tìm thấy main camera!");
            transitioning = false;
            yield break;
        }

        // Kiểm tra CinemachineBrain không null
        CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
        if (brain == null)
        {
            Debug.LogError("Không tìm thấy CinemachineBrain trên main camera!");
            transitioning = false;
            yield break;
        }

        // Đợi cho đến khi ActiveVirtualCamera không còn null
        while (brain.ActiveVirtualCamera == null)
        {
            yield return null;
        }

        // Kiểm tra ActiveVirtualCamera không null
        var activeVirtualCamera = brain.ActiveVirtualCamera as Cinemachine.CinemachineVirtualCameraBase;
        if (activeVirtualCamera == null)
        {
            Debug.LogError("ActiveVirtualCamera là null!");
            transitioning = false;
            yield break;
        }

        CinemachineVirtualCamera currentCamera = activeVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        if (currentCamera == null)
        {
            Debug.LogError("Camera ảo hiện tại không phải là CinemachineVirtualCamera!");
            transitioning = false;
            yield break;
        }

        // Lưu trữ đối tượng được theo dõi của camera hiện tại
        previousFollowTarget = currentCamera.Follow;

        // Lấy vị trí và kích thước orthographic của camera hiện tại và camera mới
        Vector3 startCameraPosition = currentCamera.transform.position;
        float startOrthographicSize = currentCamera.m_Lens.OrthographicSize;
        Vector3 targetCameraPosition = targetCamera.transform.position;
        float targetOrthographicSize = targetCamera.m_Lens.OrthographicSize;

        // Thiết lập đối tượng được theo dõi cho camera mới
        currentCamera.Follow = targetCamera.Follow;

        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            // Tính toán vị trí và kích thước orthographic mới dựa trên lerp
            float t = elapsedTime / transitionDuration;
            currentCamera.transform.position = Vector3.Lerp(startCameraPosition, targetCameraPosition, t);
            currentCamera.m_Lens.OrthographicSize = Mathf.Lerp(startOrthographicSize, targetOrthographicSize, t);

            // Tăng thời gian đã trôi qua
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Đảm bảo camera đến vị trí và kích thước cuối cùng một cách chính xác
        currentCamera.transform.position = targetCameraPosition;
        currentCamera.m_Lens.OrthographicSize = targetOrthographicSize;

        // Đặt lại transitioning thành false khi hoàn thành chuyển đổi
        transitioning = false;

        // Bật lại di chuyển của nhân vật sau khi chuyển đổi hoàn tất
        if (playerMovement != null)
        {
            playerMovement.EnableMovement(true);
        }
    }
}
