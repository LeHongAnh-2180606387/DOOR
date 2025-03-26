using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    public AudioSource audioSource; // Kéo và thả AudioSource vào trường này trong Inspector
    public AudioClip moveSound; // Âm thanh di chuyển

    public bool canMove = true;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        audioSource.clip = moveSound;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        if (canMove)
        {
            PlayerInput();
        }
    }

    private void FixedUpdate()
    {

        if (canMove)
        {
            AdjustPlayerFacingDirection();
            Move();
        }
    }

    private void PlayerInput()
    {
        if (!canMove) return; // Nếu không thể di chuyển, bỏ qua input

        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("MoveX", movement.x);
        myAnimator.SetFloat("MoveY", movement.y);

        if (!enabled) return; // Nếu script bị vô hiệu hóa, bỏ qua input

        // Xử lý input
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (Mathf.Abs(movement.x) > 0.2f || Mathf.Abs(movement.y) > 0.2f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void Move()
    {
        if (!canMove || !enabled) return; // Nếu không thể di chuyển hoặc script bị vô hiệu hóa, ngừng di chuyển
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.deltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        if (!canMove) return; // Nếu không thể di chuyển, bỏ qua điều chỉnh hướng
        if (Camera.main != null)
        {
            if (Keyboard.current.aKey.isPressed)
            {
                mySpriteRenderer.flipX = true;
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                mySpriteRenderer.flipX = false;
            }
        }
    }

    public void StopMovement()
    {
        // Đặt movement về 0 để dừng ngay lập tức
        movement = Vector2.zero;
        myAnimator.SetFloat("MoveX", 0);
        myAnimator.SetFloat("MoveY", 0);
        // Dừng âm thanh di chuyển nếu đang phát
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void EnableMovement(bool enable)
    {
        canMove = enable;
    }
}
