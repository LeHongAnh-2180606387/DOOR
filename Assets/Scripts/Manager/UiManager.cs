using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject targetUI; // UI bạn muốn chuyển đến

    void OnEnable()
    {
        EventsManager.EventUiManager += SwitchToTargetUI;
    }

    void OnDisable()
    {
        EventsManager.EventUiManager -= SwitchToTargetUI;
    }

    void SwitchToTargetUI()
    {
        if (targetUI != null)
        {
            targetUI.SetActive(true);
            // Bạn có thể thêm các logic khác như ẩn UI hiện tại nếu cần
        }
    }
}
