using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 12f;

    [Header("Jump")]
    public float jumpForce = 6f;
    public float fallMultiplier = 2.5f;

    [Header("Dash")]
    public float dashForce = 12f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1f;

    public float DashCooldownRemaining => dashCooldownTimer;

    [Header("Abilities")]
    public bool hasDash = false;

    private CharacterController controller;
    private PlayerAttack playerAttack;
    public Animator animator;

    private Vector3 moveInput;
    private Vector3 smoothInput;
    private Vector3 velocity;

    private bool isGrounded;
    private bool isDashing;

    private float dashTimer;
    private float dashCooldownTimer;

    public bool IsMoving => moveInput != Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
        HandleAnimations();
    }

    // ---------------- INPUT ----------------
    void HandleInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 targetInput = new Vector3(h, 0f, v).normalized;

        smoothInput = Vector3.Lerp(
            smoothInput,
            targetInput,
            acceleration * Time.deltaTime
        );

        moveInput = smoothInput;

        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("IsJumping", false);
        }

        // JUMP
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isDashing)
        {
            velocity.y = jumpForce;
            animator.SetBool("IsJumping", true);
        }

        // DASH
        if (hasDash &&
            Input.GetKeyDown(KeyCode.LeftShift) &&
            dashCooldownTimer <= 0f)
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;

        // GRAVITY
        velocity.y += Physics.gravity.y * fallMultiplier * Time.deltaTime;
    }

    // ---------------- MOVEMENT ----------------
    void HandleMovement()
    {
        Vector3 horizontalMove;

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            horizontalMove = transform.forward * dashForce;

            if (dashTimer <= 0f)
                isDashing = false;
        }
        else
        {
            horizontalMove = moveInput * moveSpeed;
        }

        Vector3 finalMove = horizontalMove;
        finalMove.y = velocity.y;

        controller.Move(finalMove * Time.deltaTime);
    }

    // ---------------- ROTATION ----------------
    void HandleRotation()
    {
        if (moveInput != Vector3.zero && !isDashing && !playerAttack.isAiming)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                10f * Time.deltaTime
            );
        }

        if (playerAttack.isAiming && playerAttack.targetRotationAim != Quaternion.identity)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                playerAttack.targetRotationAim,
                12f * Time.deltaTime
            );
        }
    }

    // ---------------- ANIMATIONS ----------------
    void HandleAnimations()
    {
        animator.SetFloat("Speed", moveInput.magnitude);

        float dot = (moveInput.sqrMagnitude > 0.01f)
            ? Vector3.Dot(moveInput.normalized, transform.forward)
            : 1f;

        animator.SetFloat("DirectionDot", dot);
    }
}