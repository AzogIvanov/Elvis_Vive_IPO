using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 6f;
    public float fallMultiplier = 2.5f;
    public LayerMask groundLayer;

    [Header("Dash")]
    public float dashForce = 12f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1f;

    private Rigidbody rb;
    private Vector3 moveInput;
    private bool isGrounded;
    private bool isDashing;
    private float dashTimer;
    private float dashCooldownTimer;

    private PlayerAttack playerAttack;

    public Animator animator;

    public bool IsMoving => moveInput != Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAttack = GetComponent<PlayerAttack>();

        rb.angularDamping = 10f;

    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(h, 0f, v).normalized;

        // JUMP
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isDashing)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("IsJumping", true);
            Debug.Log("JUMP TRUE");
        }

        // DASH
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0f)
        {
            StartDash();
        }

        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        // NORMAL MOVEMENT ROTATION (WHEN NOT AIMING)
        if (moveInput != Vector3.zero && !isDashing && !playerAttack.isAiming)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 8f));
        }

        // ROTATION DURING AIMING
        // USING ROTATION FROM PLAYERATTACK
        if (playerAttack.isAiming && playerAttack.targetRotationAim != Quaternion.identity)
        {
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, playerAttack.targetRotationAim, Time.fixedDeltaTime * 10f)); // Más rápido para apuntado
        }

        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
                rb.linearVelocity = Vector3.zero;
            }
            return;
        }

        rb.velocity = new Vector3(moveInput.x * moveSpeed, rb.velocity.y, moveInput.z * moveSpeed);

        // ----- ANIMATION DIRECTION -----
        Vector3 moveDir = moveInput;
        Vector3 aimDir;

        if (playerAttack.isAiming && playerAttack.targetRotationAim != Quaternion.identity)
        {
            aimDir = playerAttack.targetRotationAim * Vector3.forward;
        }
        else
        {
            aimDir = transform.forward;
        }

        if (moveDir.sqrMagnitude > 0.01f)
        {
            moveDir.Normalize();
            aimDir.Normalize();

            float dot = Vector3.Dot(moveDir, aimDir);
            animator.SetFloat("DirectionDot", dot);
        }
        else
        {
            animator.SetFloat("DirectionDot", 1f);
        }


        animator.SetFloat("Speed", moveInput.magnitude);

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;

        rb.linearVelocity = Vector3.zero;

        Vector3 dashDirection = moveInput != Vector3.zero
            ? moveInput
            : transform.forward;

        rb.AddForce(dashDirection.normalized * dashForce, ForceMode.Impulse);
    }

    void OnCollisionStay(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) == 0)
            return;

        foreach (ContactPoint contact in collision.contacts)
        {
            
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                isGrounded = true;
                animator.SetBool("IsJumping", false);
                Debug.Log("JUMP FALSE");
                return;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }
}