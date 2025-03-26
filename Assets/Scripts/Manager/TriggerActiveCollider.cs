using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActive : MonoBehaviour
{
    private BoxCollider2D collisionTrigger;
    public bool actionTrigger;
    // Start is called before the first frame update
    void Start()
    {
        collisionTrigger = GetComponent<BoxCollider2D>();
    }
        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Kiểm tra giá trị của actionTrigger
            if (actionTrigger == false)
            {
                // Thiết lập Collider là không trigger nếu actionTrigger là false
                collisionTrigger.isTrigger = false;
            }
            // Không cần gọi hàm DeactivateCamera() ở đây nếu bạn muốn giữ lại vị trí và kích thước mới của camera khi ra khỏi trigger
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
