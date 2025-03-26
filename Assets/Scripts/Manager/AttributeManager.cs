using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    public static AttributeManager Instance;

    
    // private void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;

    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
//          Dùng để lưu giá trị khi chuyển scence
//         if (transform.parent != null)
//         {
//             transform.SetParent(null); // Tách ra khỏi parent nếu có
//         }
//         DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public bool isRunningConversation { get; set; } = false;
    public bool isTrue { get; set; }
    public bool isRead { get; set; }
    // Add other attributes as needed
}
