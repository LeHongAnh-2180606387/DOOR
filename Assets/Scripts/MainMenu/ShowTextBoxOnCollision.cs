using UnityEngine;
using UnityEngine.UI;


public class ShowTextBoxOnCollision : MonoBehaviour
{

    public GameObject textBoxPanel; // Tham chiếu đến TextBoxPanel
    public Text textBoxText; // Tham chiếu đến Text trong TextBoxPanel
    private bool isPlayerInside; // Biến kiểm tra xem nhân vật có nằm trong vùng kích hoạt hay không
    private GameObject player; // Biến lưu trữ nhân vật

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) // Kiểm tra nếu vật thể chạm vào có tag "Player"
        {
            isPlayerInside = true; // Đặt biến kiểm tra là true
            player = collision.gameObject; // Lưu trữ nhân vật
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) // Kiểm tra nếu vật thể chạm vào có tag "Player"
        {
            isPlayerInside = false; // Đặt biến kiểm tra là false
            textBoxPanel.SetActive(false); // Ẩn TextBoxPanel
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E)) // Kiểm tra nếu nhấn phím E khi nhân vật ở trong vùng kích hoạt
        {
            textBoxText.text = "Chao mung den voi Unity 2D!"; // Đặt nội dung cho TextBox
            textBoxPanel.SetActive(true); // Hiển thị TextBoxPanel
        }
    }
}
