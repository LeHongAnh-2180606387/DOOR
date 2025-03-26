using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraChangerTriggerZone : MonoBehaviour
{
    public CinemachineVirtualCamera cameraChanger;
    private bool hasTriggered = false;
    private bool playerInsideTrigger = false;
    private bool cameraTransitioning = false;
    private BoxCollider2D collisionTrigger;
    public bool actionTrigger;

    private void Start()
    {
        // Lấy Collider từ chính GameObject này
        collisionTrigger = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !cameraTransitioning && !hasTriggered)
        {
            hasTriggered = true;
            playerInsideTrigger = true;
            ActivateCamera();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = false;
            // Kiểm tra giá trị của actionTrigger
            if (actionTrigger == false)
            {
                // Thiết lập Collider là không trigger nếu actionTrigger là false
                collisionTrigger.isTrigger = false;
            }
        }
    }

    private void ActivateCamera()
    {
        // Kích hoạt camera với vị trí và kích thước mới
        StartCoroutine(DelayedActivateCamera());
    }

    private IEnumerator DelayedActivateCamera()
    {
        // Đợi một frame để đảm bảo mọi thứ đã được khởi tạo
        yield return null;

        // Kiểm tra Camera.main không null
        if (Camera.main == null)
        {
            Debug.LogError("Main camera not found!");
            yield break;
        }

        // Kiểm tra CinemachineBrain không null
        CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
        if (brain == null)
        {
            Debug.LogError("CinemachineBrain not found on the main camera!");
            yield break;
        }

        // Đợi cho đến khi ActiveVirtualCamera không còn null
        while (brain.ActiveVirtualCamera == null)
        {
            yield return null;
        }

        // Kích hoạt camera thông qua EventsManager
        EventsManager.CameraChanger(cameraChanger);
        StartCoroutine(WaitForCameraTransition());
    }

    private IEnumerator WaitForCameraTransition()
    {
        cameraTransitioning = true;
        // Chờ cho đến khi camera chuyển đổi hoàn thành
        yield return new WaitForSeconds(1.5f); // Thời gian chờ (có thể thay đổi)
        cameraTransitioning = false;
    }
}
