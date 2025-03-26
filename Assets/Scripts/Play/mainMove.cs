using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMove : MonoBehaviour
{
    public float moveSpeed;
    private float leftAndRight;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isfacingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        leftAndRight = Input.GetAxis("Horizontal"); // Sửa chính tả và kiểu chữ
        //move
        rb.velocity = new Vector2(leftAndRight * moveSpeed, rb.velocity.y);
        //flip
        flip();
        anim.SetFloat("move", Mathf.Abs(leftAndRight));
        
    }

    void flip()
    {
        if (isfacingRight && leftAndRight < 0 || !isfacingRight && leftAndRight > 0)
        {
            isfacingRight = !isfacingRight;
            Vector3 mainSize = transform.localScale;
            mainSize.x = mainSize.x * -1;
            transform.localScale = mainSize;
        }
    }
}
